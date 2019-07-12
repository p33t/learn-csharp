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
            output("Hello C#");

            var generic = new Generics.MyClass<FormatException>();
            output($"Generic type: {generic.GenericType}");
            
            var multiApply = new Generics.MultiApply();
            multiApply.apply("bruce");
            multiApply.apply(-1);
        }

        static void output(string s)
        {
            System.Console.Out.WriteLine(s);
        }
    }
}
