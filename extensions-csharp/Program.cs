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
            Console.WriteLine("DONE");
        }
    }
}