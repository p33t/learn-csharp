using System;
using System.Linq;

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
        public static Func<string, string> hello = name => "hello " + name;

        public static Func<string, string> hello2 = name => wrap(hello)(name);

        private static Func<string, T> wrap<T>(Func<string, T> orig)
        {
            return name => orig(name.ToUpper());
        } 
        
        public static void Demo()
        {

            var generic = new MyClass<FormatException>();
            Util.WriteLn($"Generic type: {generic.GenericType}");
            
            var multiApply = new MultiApply();
            multiApply.Apply("bruce");
            multiApply.Apply(-1);           
            
            Util.WriteLn("Expecting 'hello BRUCE': " + hello2("bruce"));
        }
    }
}
