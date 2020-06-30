using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace extensions_csharp.Configuration
{
    public class InMemory
    {
        public static void Demo()
        {
            Console.WriteLine("Configuration.InMemory...");
            IConfiguration conf = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"my-key", "my-value"},
                })
                .Build();
            Trace.Assert(conf["my-key"] == "my-value");
        }
    }
}