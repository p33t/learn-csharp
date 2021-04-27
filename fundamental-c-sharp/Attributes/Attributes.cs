using System;
using System.Diagnostics;
using System.Linq;

namespace fundamental_c_sharp.Attributes
{
    public static class Attributes
    {
        [MyFirst(StrVal = "hello")]
        private class MyClass
        {
        }
        public static void Demo()
        {
            Console.WriteLine("Attributes ============");
            var firstAttrib = typeof(MyClass).CustomAttributes.First();
            Trace.Assert(firstAttrib.AttributeType == typeof(MyFirstAttribute));
            // NOPE.. Trace.Assert(((MyFirstAttribute)firstAttrib.NamedArguments.).StrVal == "hello");
            var myFirst = (MyFirstAttribute?) Attribute.GetCustomAttribute(typeof(MyClass), typeof(MyFirstAttribute));
            Trace.Assert(myFirst != null);
            Trace.Assert(myFirst?.StrVal == "hello");
        }
    }
}