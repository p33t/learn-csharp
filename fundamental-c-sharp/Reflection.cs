using System;
using System.Diagnostics;
using System.Reflection;

namespace fundamental_c_sharp
{
    public class Reflection
    {
        interface MyInterface
        {
            public string hello();

            public DateTime MyDateTime { get; set; }
        }
        class MyClass : MyInterface
        {
            public string hello()
            {
                return "world";
            }

            public DateTime MyDateTime { get; set; }
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

        public static void PropertyInvocation()
        {
            Console.WriteLine("Reflection - Property Invocation =================");
            var myCls = new MyClass();
            PropertyInfo myProp = typeof(MyClass).GetProperty(nameof(MyClass.MyDateTime)) ?? throw new Exception("Impossible");
            var value = myProp.GetValue(myCls);
            if (!(value is DateTime dt))
                throw new Exception("Should be a DateTime");
            Trace.Assert(dt == new DateTime());
        }

        public static void DispatchProxy()
        {
            Console.WriteLine("Reflection - Dispatch Proxy =================");
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