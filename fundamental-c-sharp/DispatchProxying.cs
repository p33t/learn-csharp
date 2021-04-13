using System;
using System.Diagnostics;
using System.Reflection;

namespace fundamental_c_sharp
{
    public static class DispatchProxying
    {
        public interface IHello
        {
            string Hello();
        }

        public class HelloImpl : IHello
        {
            private readonly string _world;

            public HelloImpl(string world)
            {
                _world = world;
            }

            public string Hello()
            {
                return _world;
            }
        }

        public class HelloService : DispatchProxy
        {
            private IHello? _hello;

            public void Init(string world)
            {
                _hello = new HelloImpl(world);
            }

            protected override object Invoke(MethodInfo targetMethod, object[] args)
            {
                Console.WriteLine($"Invoking {targetMethod.Name}()");
                if (targetMethod.Name == nameof(IHello.Hello) && targetMethod.DeclaringType == typeof(IHello))
                    return _hello!.Hello();

                throw new ArgumentException($"Unknown method {targetMethod.Name}", nameof(targetMethod));
            }
        }

        public static void Demo()
        {
            Console.WriteLine("Dispatch Proxying ============================");

            var hello = DispatchProxy.Create<IHello, HelloService>();
            // ReSharper disable once SuspiciousTypeConversion.Global
            ((HelloService)hello).Init("World!");
            Debug.Assert(hello.Hello() == "World!");
        }
    }
}
