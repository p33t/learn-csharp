using System;
using System.Diagnostics;
using System.Globalization;

namespace fundamental_c_sharp.PartialClass
{
    public static class PartialClass
    {

        public static void Demo()
        {
            // NOTE: Generally stay away from partial classes as
            //       they appear to break encapsulation and are jarring to read
            Console.WriteLine("Partial Class ================");
            var exp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var cls = new MyClass(exp);
            Trace.Assert(cls.MyMethod1() == "some-string");
            Trace.Assert(cls.MyMethod2() == exp);
        }
    }
}