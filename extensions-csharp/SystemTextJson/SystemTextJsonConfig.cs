using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using extensions_csharp.SystemTextJson.Model;

namespace extensions_csharp.SystemTextJson
{
    public class SystemTextJsonConfig
    {
        /// <summary>
        /// Configuration serialize/deserialize with System.Text.Json (vs Newtonsoft)
        /// </summary>
        /// Need to roll-your-own polymorphism.
        ///   See https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-core-3-1#an-alternative-way-to-do-polymorphic-deserialization
        ///   Works pretty well!
        /// No validation at this point. Can prob use conventional annotations-based validation.
        ///   Potentially write an attribute that will recurse down into objects and perform validation (not done by default)
        public static void Demo()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new TestConfigConverter());
            options.Converters.Add(new NameFilterDefConverter());

            string SerializeConfig(TestConfig config) => JsonSerializer.Serialize(config, options);
            TestConfig DeserializeConfig(string json) => JsonSerializer.Deserialize<TestConfig>(json, options);
            
            // Eligible List ======================================================================
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

            var json11 = SerializeConfig(v11);
            // Console.WriteLine("Resulting JSON:");
            // Console.WriteLine(json11);
            var v11Alt = DeserializeConfig(json11) as TestConfigV1;
            Trace.Assert(v11Alt != null);
            Trace.Assert(v11Alt!.Name.Equals(v11.Name));
            Trace.Assert(v11Alt.NameFilterDef is EligibleList);
            Trace.Assert(((EligibleList) v11.NameFilterDef).Eligible.Count
                         == ((EligibleList) v11Alt.NameFilterDef).Eligible.Count);
            // This does not contain all the strange polymorphism side affects for IList that Newtonsoft has
            var json11Alt = SerializeConfig(v11Alt);
            Trace.Assert(json11Alt.Equals(json11));
            
            // Prefixed ======================================================================
            var v12 = new TestConfigV1
            {
                Name = "#2",
                NameFilterDef = new Prefixed
                {
                    Prefix = "Bruce "
                }
            };

            var json12 = SerializeConfig(v12);
            var v12Alt = DeserializeConfig(json12) as TestConfigV1;
            Trace.Assert(v12Alt!.Name == v12.Name);
            Trace.Assert(v12Alt.NameFilterDef is Prefixed);
            Trace.Assert((v12Alt.NameFilterDef as Prefixed)!.Prefix == "Bruce ");
        }
    }
}