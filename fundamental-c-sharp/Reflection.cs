using System;
using System.Reflection;

namespace fundamental_c_sharp
{
    public class Reflection
    {
        interface MyInterface
        {
            public string hello();
        }
        class MyClass : MyInterface
        {
            public string hello()
            {
                return "world";
            }
        }

        class LoggingProxy<T> : DispatchProxy
        {
            private object? _delegate;

            public static T Create(T decorated)
            {
                object? proxy = Create<T, LoggingProxy<T>>();
                if (proxy != null)
                {
                    ((LoggingProxy<T>) proxy).InitDelegate(decorated);
                    return (T) proxy;
                }
                throw new Exception("Could not create DispatchProxy instance");
            }

            protected override object? Invoke(MethodInfo targetMethod, object[] args)
            {
                Util.WriteLn($"Before {targetMethod}");
                var result = targetMethod.Invoke(_delegate, args);
                Util.WriteLn($"After {targetMethod}");
                return result;
            }

            public void InitDelegate(T @delegate)
            {
                this._delegate = @delegate;
            }
        }

        public static void DispatchProxy()
        {
            var proxy = LoggingProxy<MyInterface>.Create( new MyClass());
            proxy.hello();
        }

        //
        // private static void OutputTimestamp()
        // {
        //     var timestamp = DateTime.UtcNow.Ticks;
        //     var localTicks = DateTime.Now.Ticks;
        //     var localTime = new DateTime(timestamp, DateTimeKind.Utc).ToLocalTime();
        //     Console.Out.WriteLine("Timestamp = {0}.  Local ticks = {1}.  Local time = {2}.", timestamp, localTicks, localTime);
        // }
    }
}