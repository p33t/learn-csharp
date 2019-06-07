using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{

    class MyClass<T> where T : Exception
    {
        public string GenericType
        {
            get
            {
                return typeof(T).Name;
            }
        }
    }
}
