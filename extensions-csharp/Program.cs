using System;

namespace extensions_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration.Configuration.Demo();
            DependencyInjection.DependencyInjection.Demo();
            Logging.Logging.Demo();
            AutoMapping.AutoMapping.Demo();
            Validation.Validation.Demo();
            DynamicProxying.DynamicProxying.Demo();
            Console.WriteLine("DONE");
        }
    }
}