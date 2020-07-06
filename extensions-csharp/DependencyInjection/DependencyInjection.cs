using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace extensions_csharp.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void Demo()
        {
            Console.WriteLine("Dependency Injection...");
            var injector = new ServiceCollection()
                .AddSingleton<IHello, HelloWorld>()
                .BuildServiceProvider();

            var hello = injector.GetService<IHello>();
            Trace.Assert(hello.Hello() == "World");
        }
    }
}