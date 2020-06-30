using System;
using System.Diagnostics;

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
            Console.WriteLine($"Expecting 'Hello Bruce':" + Run(Hello, "Bruce"));
        }
    }
}