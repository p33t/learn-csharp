using System;

// ReSharper disable once CheckNamespace
namespace fundamental_c_sharp.Generics
{

    class MyClass<T> where T : Exception
    {
        public string GenericType => typeof(T).Name;
    }

    interface IApply<in T>
    {
        void Apply(T arg);
    }

    class MultiApply : IApply<string>, IApply<int>
    {
        public void Apply(string arg)
        {
            // empty
        }

        public void Apply(int arg)
        {
            // empty
        }
    }
}
