using System;
using System.Diagnostics;

namespace fundamental_c_sharp
{
    public static class NullableX
    {
        class MyClass
        {
            
        }
        public static void Demo()
        {
            Console.WriteLine("Nullable ...");
            MyClass? opt = null;
            bool? optBool = null;
            
            Trace.Assert(opt is null);
            Trace.Assert(optBool is null);
            
            opt = new MyClass();
            optBool = false;
            
            Trace.Assert(opt is MyClass);
            Trace.Assert(optBool is bool);
        }
    }
}