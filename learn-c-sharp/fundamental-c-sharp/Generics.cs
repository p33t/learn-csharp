using System;

// ReSharper disable once CheckNamespace
namespace fundamental_c_sharp
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

    public class Generics
    {
        public static void Demo()
        {

            var generic = new MyClass<FormatException>();
            Util.output($"Generic type: {generic.GenericType}");
            
            var multiApply = new MultiApply();
            multiApply.Apply("bruce");
            multiApply.Apply(-1);            
        }
    }
}
