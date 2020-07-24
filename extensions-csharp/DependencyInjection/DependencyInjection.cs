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
                .AddSingleton<FooImpl>()
                .AddSingleton<IHello, HelloWorld>()
                .AddSingleton<HelloFactory>()
                .BuildServiceProvider();

            var hello = injector.GetRequiredService<IHello>();
            Trace.Assert(hello.Hello() == "World");

            var yellow = injector.GetRequiredService<HelloFactory>()
                .Create(HelloFlavour.Yellow);
            Trace.Assert(yellow.Hello() == "Yellow");

            // NO... is not clever about implemented interfaces... which is prob good
            // var foo = injector.GetRequiredService<IFoo>();
            // Trace.Assert(foo is FooImpl);
        }
    }
}