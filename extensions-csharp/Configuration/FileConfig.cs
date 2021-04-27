using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace extensions_csharp.Configuration
{
    public static class FileConfig
    {
        public static void Demo()
        {
            Console.WriteLine("Configuration.File (JSON)..."); 
            IConfiguration conf = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var mySection = conf.GetSection("MySection");
            Trace.Assert(mySection["MyKey2"] == "MyValue2");
        }
    }
}