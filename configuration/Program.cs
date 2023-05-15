using System.Diagnostics;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json") //, optional: true, reloadOnChange: true)
    // .AddEnvironmentVariables()
    // .AddCommandLine(args, new Dictionary<string, string>
    // {
    //     {"--log-level", "Serilog__MinimumLevel"}
    // })
    .Build();

// Basic JSON   =============================================
Trace.Assert("Value1" == configuration["Key1"]);

var section1 = configuration.GetSection("Section1");
Trace.Assert("Value11" == section1["Key11"]);




Console.WriteLine("Tests have succeeded.");
