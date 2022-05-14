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
            var json11 = JsonConvert.SerializeObject(v11, serializerSettings);
            Console.WriteLine("Resulting JSON:\n " + json11);
            var v11Alt = JsonConvert.DeserializeObject<TestConfig>(json11, serializerSettings);
            Trace.Assert(v11.Name == v11Alt!.Name);
            Trace.Assert(v11Alt.NameFilterDef is EligibleList);
            Trace.Assert(((EligibleList) v11.NameFilterDef).Eligible.Count
                         == ((EligibleList) v11Alt.NameFilterDef).Eligible.Count);
            
            // 'Prefixed' name filter config
            var v12 = new TestConfig
            {
                Name = "#2",
                NameFilterDef = new Prefixed
                {
                    Prefix = "Bruce "
                }
            };
            var json12 = JsonConvert.SerializeObject(v12, serializerSettings);
            var v12Alt = JsonConvert.DeserializeObject<TestConfig>(json12, serializerSettings);
            Trace.Assert(v12.Name == v12Alt!.Name);
            Trace.Assert(v12Alt.NameFilterDef is Prefixed);
            Trace.Assert(((Prefixed) v12.NameFilterDef).Prefix
                         == ((Prefixed) v12Alt.NameFilterDef).Prefix);
            
        }
    }
}