using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using extensions_csharp.Newtonsoft.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace extensions_csharp.Newtonsoft
{
    public class NewtonsoftJsonConfig
    {
        /// <summary>
        /// Writing & Loading a structured JSON configuration file.
        /// </summary>
        public static void Demo()
        {
            Tuple<string, Type>[] binds = 
            {
                new("Prefixed", typeof(Prefixed)),
                new("EligibleList", typeof(EligibleList)),
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

            // 'EligibleList' name filter config
            var v11 = new TestConfig
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
            var json11 = JsonConvert.SerializeObject(v11, serializerSettings);
            Console.WriteLine("Resulting JSON:\n " + json11);
            var v11Alt = JsonConvert.DeserializeObject<TestConfig>(json11, serializerSettings);
            Trace.Assert(v11.Name == v11Alt!.Name);
            Trace.Assert(v11Alt.NameFilterDef is EligibleList);
            Trace.Assert(((EligibleList) v11.NameFilterDef).Eligible.Count
                         == ((EligibleList) v11Alt.NameFilterDef).Eligible.Count);
            // This has "$type" & "$values" fields in the resulting EligibleList.Eligible value
            // var json11Alt = JsonConvert.SerializeObject(v11Alt, serializerSettings);
            // Console.WriteLine("Alternate JSON:\n " + json11Alt);
            // Trace.Assert(json11Alt.Equals(json11));
            
            // 'Prefixed' name filter config
            var v12 = new TestConfig
            {
                Name = "#2",
                NameFilterDef = new Prefixed
                {
                    Prefix = "Bruce "
                }
            };
            AssertValid(v12);
            var json12 = JsonConvert.SerializeObject(v12, serializerSettings);
            var v12Alt = JsonConvert.DeserializeObject<TestConfig>(json12, serializerSettings);
            Trace.Assert(v12.Name == v12Alt!.Name);
            Trace.Assert(v12Alt.NameFilterDef is Prefixed);
            Trace.Assert(((Prefixed) v12.NameFilterDef).Prefix
                         == ((Prefixed) v12Alt.NameFilterDef).Prefix);
            
            // Data validation... is really a separate thing.
            var v13 = JsonConvert.DeserializeObject<TestConfig>("{}", serializerSettings);
            Trace.Assert(v13 != null);
            AssertInvalid(v13!);
            var v14 = new TestConfig
            {
                Name = "#4",
                NameFilterDef = new Prefixed
                {
                    Prefix = "x" // too short
                }
            };
            // Doesn't work....is only shallow validation
            // AssertInvalid(v14);
        }

        private static void AssertValid(TestConfig config)
        {
            var errors = Validate(config);
            Trace.Assert(!errors.Any(), 
                $"Got {errors.Count} errors: {string.Join('\n', errors)}");
        }

        private static void AssertInvalid(TestConfig config)
        {
            Trace.Assert(Validate(config).Count > 0, "No errors");
        }
        
        private static List<string> Validate(TestConfig config)
        {
            var result = new List<ValidationResult>();
            Validator.TryValidateObject(config, new ValidationContext(config), result, true);
            return result.Select(r => r.ErrorMessage).ToList();
        }
    }
}