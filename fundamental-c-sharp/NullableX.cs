using System;
using System.Diagnostics;

namespace fundamental_c_sharp
{
    public static class NullableX
    {
        class MyClass
        {
            public bool SomeFlag { get; } = false;

            public bool SomeMethod() => false;

            public int? SomeInt { get; } = 33;
        }
        public static void Demo()
        {
            Console.WriteLine("Nullable ...");
            MyClass? opt = null;
            bool? optBool = null;
            int? optInt = null;
            
            Trace.Assert(opt is null);
            Trace.Assert(optBool is null);
            
            Trace.Assert(opt?.SomeFlag != true);
            Trace.Assert(opt?.SomeFlag != false);
            Trace.Assert(opt?.SomeFlag is null);
            Trace.Assert(opt?.SomeFlag is true == false);
            Trace.Assert(opt?.SomeFlag is false == false);
            Trace.Assert(opt?.SomeMethod() is true == false);
            Trace.Assert(opt?.SomeMethod() is false == false);

            Trace.Assert(optInt == null);
            
            opt = new MyClass();
            optBool = false;
            optInt = 99;
            
            Trace.Assert(opt is MyClass);
            Trace.Assert(optBool is bool);
            Trace.Assert(optInt is int);

            Trace.Assert(opt?.SomeFlag != true);
            Trace.Assert(opt?.SomeFlag == false);
            Trace.Assert(opt?.SomeFlag is null == false);
            Trace.Assert(opt?.SomeFlag is true == false);
            Trace.Assert(opt?.SomeFlag is false);
            Trace.Assert(opt?.SomeMethod() is true == false);
            Trace.Assert(opt?.SomeMethod() is false);

            Trace.Assert(optInt! == 99);
            Trace.Assert(opt!.SomeInt! == 33);
        }
    }
}