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
                .AddSingleton<HelloWorld>()
                .AddSingleton<HelloYellow>()
                .AddSingleton<IHello, HelloWorld>()
                .AddSingleton<HelloFactory>()
                .BuildServiceProvider();

            var hello = injector.GetService<IHello>();
            Trace.Assert(hello.Hello() == "World");

            var yellow = injector.GetService<HelloFactory>()
                .Create(HelloFlavour.Yellow);
            Trace.Assert(yellow.Hello() == "Yellow");

        }
    }
}