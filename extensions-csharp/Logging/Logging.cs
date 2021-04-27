using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace extensions_csharp.Logging
{
    /// <summary>
    ///   Logging demonstrations
    /// </summary>
    /// <remarks>
    ///   ILogger injection doesn't work by default.  Need ILogger&lt;SomeType><br/>
    ///   Without a log provider, all log messages are ignored.<br/>
    ///   Need Microsoft.Extensions.Logging.Console to log to console.<br/>
    /// </remarks>
    public static class Logging
    {
        public class MyService
        {
            private readonly ILogger<MyService> _logger;

            public MyService(ILogger<MyService> logger)
            {
                _logger = logger;
            }

            public void MyMethod(string arg)
            {
                // NOTE: Don't use string interpolation.
                //       See https://blog.rsuter.com/logging-with-ilogger-recommendations-and-best-practices/
                _logger.LogInformation("MyMethod() called with arg {arg}", arg);
            }
        }
        
        public static void Demo()
        {
            Console.WriteLine("Logging (with DI)...");
            var services = new ServiceCollection();
            services.AddLogging(logging =>
            {
                // Need Microsoft.Extensions.Logging.Console for this
                logging.AddConsole();
                // Didn't need this but might be useful later
                // logging.AddConsole(opts => opts.LogToStandardErrorThreshold = LogLevel.Information);
            }); 
            services.AddSingleton<MyService>();

            
            using (var injector = services.BuildServiceProvider())
            {
                var myService = injector.GetService<MyService>();
                myService.MyMethod("Call 1");
                myService.MyMethod("Call 2");
            }
            Console.WriteLine("This should come after log messages are flushed");
        }
    }
}