using System;
using System.Diagnostics;

namespace fundamental_c_sharp
{
    public static class Casting
    {
        interface MyInterface
        {
        }

        class MyClass : MyInterface
        {
        }

        class AnotherClass
        {
        }

        public static void Demo()
        {
            Console.WriteLine("Casting ==============");
            object myClass = new MyClass();
            object anotherClass = new AnotherClass();
            
            // ReSharper disable once SafeCastIsUsedAsTypeCheck
            Trace.Assert(myClass as MyInterface != null);
            Trace.Assert(myClass is MyInterface);
            
            // ReSharper disable once SafeCastIsUsedAsTypeCheck
            Trace.Assert(anotherClass as MyInterface == null);  //<<<<< doesn't throw
            Trace.Assert(!(anotherClass is MyInterface));

            var x = (MyInterface) myClass;

            try
            {
                x = (MyInterface) anotherClass;
                Trace.Fail("Should not reach here");
            }
            catch (InvalidCastException)
            {
                // expected
            }
            
            Trace.Assert(x == myClass);
        }
    }
}