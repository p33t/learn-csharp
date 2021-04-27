using System;
using System.Diagnostics;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace extensions_csharp.DynamicProxying
{
    public static class DynamicProxying
    {
        public class Incumbent
        {
            public string StuckWithThis()
            {
                return "blah";
            }
        }

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

        public class IncumbentSubclass : Incumbent, IHello
        {
            private IHello? _hello;

            public void Init(string world)
            {
                _hello = new HelloImpl(world);
            }

            public string Hello()
            {
                return _hello!.Hello();
            }
        }

        // Constructor arg and not a real IHello
        public class IncumbentSubclass2 : Incumbent, IHello
        {
            public IncumbentSubclass2(string stuckWithThis)
            {
            }

            public string Hello()
            {
                throw new NotImplementedException();
            }
        }

        public class MyInterceptor : IInterceptor
        {
            public void Intercept(IInvocation invocation)
            {
                Console.WriteLine($"Invoking {invocation.MethodInvocationTarget}");
                invocation.Proceed();
            }
        }

        public class MyMetaImpl : IInterceptor
        {
            private readonly HelloImpl _hello;

            public MyMetaImpl(string world)
            {
                _hello = new HelloImpl(world);
            }

            public void Intercept(IInvocation invocation)
            {
                // NPE
                // invocation.ReturnValue = invocation.MethodInvocationTarget.Invoke(_hello, invocation.Arguments);
                invocation.ReturnValue = invocation.Method.Invoke(_hello, invocation.Arguments);
            }
        }

        public static void Demo()
        {
            // Adding interception to an interface proxy.  Invoke() is hosted on a target class that is not inherited from
            var incSub = new IncumbentSubclass();
            incSub.Init("World!");
            var proxyGenerator = new ProxyGenerator();
            var altHello = proxyGenerator.CreateInterfaceProxyWithTarget<IHello>(incSub, new MyInterceptor());
            // var altHello = proxyGenerator.CreateClassProxy<IncumbentSubclass>(cls, );
            Debug.Assert(altHello.Hello() == "World!");
            // Debug.Assert(((Incumbent)altHello).StuckWithThis() == "blah");  NOPE
            Debug.Assert(((Incumbent) ProxyUtil.GetUnproxiedInstance(altHello)).StuckWithThis() == "blah");

            // ProxyUtil.CreateDelegateToMixin<>()  Does not appear useful... an interface facet to a given object with Invoke()

            // Resulting proxy impl of an interface inherits from a given parent class... but cannot seem to initialize ?!
            // Actually... this is bugged.  Need 'WithoutTarget'
            var options = new ProxyGenerationOptions
            {
                BaseTypeForInterfaceProxy = typeof(IncumbentSubclass)
            };
            altHello = proxyGenerator.CreateInterfaceProxyWithTarget<IHello>(incSub, options, new MyInterceptor());
            incSub = (IncumbentSubclass) altHello;
            incSub.Init("Bingo!");
            var helloResponse = altHello.Hello();
            // Debug.Assert(helloResponse == "Bingo!"); // why not?
            Debug.Assert(helloResponse == "World!"); // WHY?

            // completely skip target class !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            options = new ProxyGenerationOptions
            {
                BaseTypeForInterfaceProxy = typeof(Incumbent)
            };
            altHello = proxyGenerator.CreateInterfaceProxyWithoutTarget<IHello>(options, new MyMetaImpl("Yellow!"));
            Debug.Assert(((Incumbent) altHello).StuckWithThis() == "blah");
            Debug.Assert(altHello.Hello() == "Yellow!");

            // Hmmm.... this isn't going to work.  Seems like we need a no-arg constructor in proxy parent class.
            var incSub2 = new IncumbentSubclass2("this'll be interesting");
            proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IHello), incSub2, new MyMetaImpl("Dolly!"));
        }
    }
}
