using System;

namespace fundamental_c_sharp
{
    public class Functions
    {
        class Wrapper
        {
            public String meth(String s) => $"Ciao {s}";
        }

        public static void Demo()
        {
            Func<string, string> hello = (arg) => $"Hello {arg}";
            Describe(hello);
            Func<string, string> methInst = new Wrapper().meth;
            Describe(methInst);

            // Trying to identify a wrapper function/class
            if (methInst.Target is Wrapper) Console.WriteLine("Yay!  Identified");
            
            // multi-line
            Func<string, string> multi = (arg) =>
            {
                // ReSharper disable once ConvertToConstant.Local
                var s = "Hello ";
                return s + arg;
            };
            Describe(multi);
        }

        private static void Describe(Func<string, string> hello)
        {
            var typ = hello.GetType();
            Console.WriteLine("Type of function: " + typ);

            var targetType = hello.Target?.GetType();
            Console.WriteLine("Target Type of function: " + targetType);
        }
    }
}