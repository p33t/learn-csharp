using System;
using System.Diagnostics;
using System.Linq;

namespace fundamental_c_sharp
{
    public delegate string MyFun(string ip);
    public class Delegates
    {
        public static string Hello(string world)
        {
            return "Hello " + world;
        }

        public static string Run(MyFun fun, string arg)
        {
            return fun(arg);
        }
        
        public static void Demo()
        {
            Console.WriteLine("Delegates --------------------");
            var output = Run(Hello, "Bruce");
            // Console.WriteLine($"Expecting 'Hello Bruce':" + output + ".");
            Debug.Assert(string.Equals("Hello Bruce", output));

            output = Run(s => string.Join(null, s.Reverse()), "Yellow");
            Debug.Assert(string.Equals("wolleY", output, StringComparison.Ordinal));
        }
    }
}