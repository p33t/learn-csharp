using System.Diagnostics;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json") //, optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    // .AddCommandLine(args, new Dictionary<string, string>
    // {
    //     {"--log-level", "Serilog__MinimumLevel"}
    // })
    .Build();

// Basic JSON   =============================================
Trace.Assert("Value1" == configuration["Key1"]);

var section1 = configuration.GetSection("Section1");
Trace.Assert("Value11" == section1["Key11"]);


// Supplied by environment var ==============================
// This env var must be present (can supply in JB Rider run config)
// Section2__Key21=Value21
var section2 = configuration.GetSection("Section2");
Trace.Assert("Value21" == section2["Key21"]);


Console.WriteLine("Tests have succeeded.");
