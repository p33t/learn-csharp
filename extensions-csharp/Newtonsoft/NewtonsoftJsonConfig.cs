using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using extensions_csharp.Newtonsoft.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;

namespace extensions_csharp.Newtonsoft
{
    public class NewtonsoftJsonConfig
    {
        /// <summary>
        /// Writing & Loading a structured JSON configuration file.
        /// </summary>
        /// Notes:
        ///   Am able to read and write nested polymorphic structures with custom $type values
        ///     The '$type' field is not easily changed.
        ///     This include a top-level config format versioning feature
        ///   Stopped short of writing Converters and low-level [de]serialization
        ///   Schema generation (and resulting validation) do not work with polymorphism as it is done here.
        public static void Demo()
        {
            Tuple<string, Type>[] binds =
            {
                new("Prefixed", typeof(Prefixed)),
                new("EligibleList", typeof(EligibleList)),
                new("V1", typeof(TestConfigV1))
            };

            var serializerSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                // enables saving of $type fields where necessary
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new SimplifiedSerializationBinder(
                    serializedType => binds.FirstOrDefault(t2 => t2.Item2 == serializedType)?.Item1,
                    typeString => binds.FirstOrDefault(t2 => t2.Item1 == typeString)?.Item2,
                    new DefaultSerializationBinder()
                )
            };

            var serializer = JsonSerializer.Create(serializerSettings);
            var schema = new JSchemaGenerator().Generate(typeof(ConfigWrapper));
            // Console.WriteLine("ConfigWrapper schema... ");
            // schema.WriteTo(new JsonTextWriter(Console.Out));
            // Console.WriteLine();
            // var schemaV1 = new JSchemaGenerator().Generate(typeof(TestConfigV1));
            // Console.WriteLine("TestConfigV1 schema... ");
            // schemaV1.WriteTo(new JsonTextWriter(Console.Out));
            // Console.WriteLine();
            
            // Returns a JSON string from serializing the given TestConfig
            string SerializeTestConfig(TestConfig config) =>
                JsonConvert.SerializeObject(config, typeof(TestConfig), serializerSettings);

            
            TestConfig DeserializeTestConfig(string json, bool validated = false)
            {
                var wrapperJson = $"{{Config:{json}}}";
                JsonReader reader = new JsonTextReader(new StringReader(wrapperJson));
                if (validated)
                {
                    reader = new JSchemaValidatingReader(reader)
                    {
                        Schema = schema
                    };
                }

                var wrapper = serializer.Deserialize<ConfigWrapper>(reader);
                return wrapper!.Config;
            }

            // 'EligibleList' name filter config ===================================================================
            var v11 = new TestConfigV1
            {
                Name = "#1",
                NameFilterDef = new EligibleList
                {
                    Eligible = new List<string>
                    {
                        "Bruce Lee",
                        "Bruce Wayne"
                    }
                }
            };
            AssertValid(v11);
            var json11 = SerializeTestConfig(v11);
            Console.WriteLine("Resulting JSON:\n " + json11);
            var v11Alt = DeserializeTestConfig(json11) as TestConfigV1;
            Trace.Assert(v11Alt != null);
            Trace.Assert(v11.Name == v11Alt!.Name);
            Trace.Assert(v11Alt.NameFilterDef is EligibleList);
            Trace.Assert(((EligibleList) v11.NameFilterDef).Eligible.Count
                         == ((EligibleList) v11Alt.NameFilterDef).Eligible.Count);
            // This has "$type" & "$values" fields in the resulting EligibleList.Eligible value
            // var json11Alt = SerializeTestConfig(v11Alt, serializerSettings);
            // Console.WriteLine("Alternate JSON:\n " + json11Alt);
            // Trace.Assert(json11Alt.Equals(json11));

            // 'Prefixed' name filter config =====================================================================
            var v12 = new TestConfigV1
            {
                Name = "#2",
                NameFilterDef = new Prefixed
                {
                    Prefix = "Bruce "
                }
            };
            AssertValid(v12);
            var json12 = SerializeTestConfig(v12);
            var v12Alt = DeserializeTestConfig(json12) as TestConfigV1;
            Trace.Assert(v12.Name == v12Alt!.Name);
            Trace.Assert(v12Alt.NameFilterDef is Prefixed);
            Trace.Assert(((Prefixed) v12.NameFilterDef).Prefix
                         == ((Prefixed) v12Alt.NameFilterDef).Prefix);


            // Data validation of C# object... is really a separate thing.==========================================
            var v13 = DeserializeTestConfig("{\"$type\": \"V1\"}") as TestConfigV1;
            Trace.Assert(v13 != null);
            AssertInvalid(v13!);
            var v14 = new TestConfigV1
            {
                Name = "#4",
                NameFilterDef = new Prefixed
                {
                    Prefix = "x" // too short
                }
            };
            // Doesn't work....is only shallow validation
            //AssertInvalid(v14);
            var json14 = SerializeTestConfig(v14);

            
            // validation during parse ============================================================================
            var v11Valid = DeserializeTestConfig(json11, true) as TestConfigV1;
            Trace.Assert(v11Valid != null);
            Trace.Assert(v11Valid!.Name.Equals(v11.Name));
            var v12Valid = DeserializeTestConfig(json12, true) as TestConfigV1;
            Trace.Assert(v12Valid != null);
            Trace.Assert(v12Valid!.Name.Equals(v12.Name));
            Trace.Assert(v12Valid.NameFilterDef is Prefixed);

            string AssertInvalidJson(string json)
            {
                try
                {
                    DeserializeTestConfig(json, true);
                    throw new Exception($"Should not have successfully validated {json}");
                }
                catch (JSchemaValidationException ex)
                {
                    return ex.Message;
                }
            }

            var errorMessage = AssertInvalidJson("{\"$type\": \"V1\"}");
            Trace.Assert(errorMessage.Contains(nameof(TestConfigV1.NameFilterDef)));

            errorMessage = AssertInvalidJson(json14);
            Trace.Assert(errorMessage.Contains("NameFilterDef.Prefix"));
        }

        private static void AssertValid(TestConfigV1 config)
        {
            var errors = Validate(config);
            Trace.Assert(!errors.Any(),
                $"Got {errors.Count} errors: {string.Join('\n', errors)}");
        }

        private static void AssertInvalid(TestConfigV1 config)
        {
            Trace.Assert(Validate(config).Count > 0, "No errors");
        }

        private static List<string> Validate(TestConfigV1 config)
        {
            var result = new List<ValidationResult>();
            Validator.TryValidateObject(config, new ValidationContext(config), result, true);
            return result.Select(r => r.ErrorMessage).ToList();
        }
    }
}