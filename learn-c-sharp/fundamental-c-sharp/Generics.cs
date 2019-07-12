using System;

namespace fundamental_c_sharp
{

    class MyClass<T> where T : Exception
    {
        public string GenericType => typeof(T).Name;
    }
}
