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
            
            var v11 = new V1
            {
                Name = "#1"
            };

            var json = JsonConvert.SerializeObject(v11, serializerSettings);

            var v11Alt = JsonConvert.DeserializeObject<V1>(json, serializerSettings);
            
            Trace.Assert(v11.Name == v11Alt!.Name);
        }
    }
}