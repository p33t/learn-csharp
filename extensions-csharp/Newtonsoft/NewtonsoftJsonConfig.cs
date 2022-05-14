using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
using extensions_csharp.Newtonsoft.Model;
using Newtonsoft.Json;

namespace extensions_csharp.Newtonsoft
{
    public class NewtonsoftJsonConfig
    {
        /// <summary>
        /// Writing & Loading a structured JSON configuration file.
        /// </summary>
        public static void Demo()
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            
            var v11 = new TestConfig
            {
                Name = "#1",
                Component1 = new Component1Config
                {
                    Elements = new List<string>
                    {
                        "elem1",
                        "elem2"
                    }
                }
            };

            var json = JsonConvert.SerializeObject(v11, serializerSettings);
            Console.WriteLine("Resulting JSON:\n " + json);

            var v11Alt = JsonConvert.DeserializeObject<TestConfig>(json, serializerSettings);
            
            Trace.Assert(v11.Name == v11Alt!.Name);
            Trace.Assert(v11.Component1.Elements.Count == v11Alt.Component1.Elements.Count);
        }
    }
}