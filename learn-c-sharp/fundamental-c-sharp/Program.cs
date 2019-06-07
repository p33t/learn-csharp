using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fundamental_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.output("Hello C#");

            var generic = new Generics.MyClass<FormatException>();
            Program.output($"Generic type: {generic.GenericType}");
        }

        static void output(string s)
        {
            System.Console.Out.WriteLine(s);
        }
    }
}
