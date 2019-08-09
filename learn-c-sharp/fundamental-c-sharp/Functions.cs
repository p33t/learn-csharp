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
            if(methInst.Target.GetType() == typeof(Wrapper)) Console.WriteLine("Yay!  Identified");
        }

        private static void Describe(Func<string, string> hello)
        {
            var typ = hello.GetType();
            Console.WriteLine("Type of function: " + typ);

            var targetType = hello.Target.GetType();
            Console.WriteLine("Target Type of function: " + targetType);
        }
    }
}