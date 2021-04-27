using System;
using System.Threading.Tasks;

namespace extensions_csharp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Configuration.Configuration.Demo();
            DependencyInjection.DependencyInjection.Demo();
            Logging.Logging.Demo();
            AutoMapping.AutoMapping.Demo();
            Validation.Validation.Demo();
            DynamicProxying.DynamicProxying.Demo();
            await Authz.Authz.Demo();
            Console.WriteLine("DONE");
        }
    }
}