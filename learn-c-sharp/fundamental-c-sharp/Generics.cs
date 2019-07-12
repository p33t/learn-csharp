using System;

namespace fundamental_c_sharp.Generics
{

    class MyClass<T> where T : Exception
    {
        public string GenericType => typeof(T).Name;
    }

    interface IApply<in T>
    {
        void apply(T arg);
    }

    class MultiApply : IApply<string>, IApply<int>
    {
        public void apply(string arg)
        {
            // empty
        }

        public void apply(int arg)
        {
            // empty
        }
    }
}
