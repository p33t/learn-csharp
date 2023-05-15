using System.Diagnostics;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json") //, optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

// Basic JSON   =============================================
Trace.Assert("Value1" == configuration["Key1"]);

// load an entire section
var section1 = configuration.GetSection("Section1");
Trace.Assert("Value11" == section1["Key11"]);

// read direct into section
Trace.Assert("Value11" == configuration["Section1:Key11"]);


// Supplied by environment var ==============================
// This env var must be present (can supply in JB Rider run config)
// Section2__Key21=Value21
var section2 = configuration.GetSection("Section2");
Trace.Assert("Value21" == section2["Key21"]);


// Supplied via command line arg ============================
// Needs 2 command line args: --Key2 Value2
Trace.Assert("Value2" == configuration["Key2"]);

// needs 2 command line args: --Section2:Key22 Value22
Trace.Assert("Value22" == section2["Key22"]);

Console.WriteLine("Tests have succeeded.");
