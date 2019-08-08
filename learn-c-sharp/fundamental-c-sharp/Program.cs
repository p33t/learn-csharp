using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fundamental_c_sharp
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        static void Main(string[] args)
        {
            Util.output("Hello C#");
            Generics.Demo();
            Time.Timestamps();
            
            ParameterPassing.Demo();
            Constructors.Demo();
        }
    }
}
