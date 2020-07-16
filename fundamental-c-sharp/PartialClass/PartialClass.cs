using System;
using System.Diagnostics;
using System.Globalization;

namespace fundamental_c_sharp.PartialClass
{
    public static class PartialClass
    {

        public static void Demo()
        {
            Console.WriteLine("Partial Class ================");
            var exp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var cls = new MyClass(exp);
            Trace.Assert(cls.MyMethod() == exp);
        }
    }
}