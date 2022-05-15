using System;
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
        public static void Demo()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new TestConfigConverter());
            
            var v11 = new TestConfigV1
            {
                Name = "#1"
            };
            var v11Json = JsonSerializer.Serialize<TestConfig>(v11, options);
            Console.WriteLine("Resulting JSON:");
            Console.WriteLine(v11Json);
        }
    }
}