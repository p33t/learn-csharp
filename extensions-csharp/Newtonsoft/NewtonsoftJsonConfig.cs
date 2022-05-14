using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;
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
            var serializerSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                // enables saving of $type fields where necessary
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new SimplifiedSerializationBinder(typeof(NameFilterDef), new DefaultSerializationBinder())
            };

            var v11 = new TestConfig
            {
                Name = "#1",
                NameFilterDef = new EligibleList
                {
                    Eligible = new List<string>
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
            Trace.Assert(v11Alt.NameFilterDef is EligibleList);

            Trace.Assert(((EligibleList) v11.NameFilterDef).Eligible.Count
                         == ((EligibleList) v11Alt.NameFilterDef).Eligible.Count);
        }
    }
}