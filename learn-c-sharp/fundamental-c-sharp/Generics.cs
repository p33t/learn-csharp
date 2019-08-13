using System;
using System.Collections.Generic;

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

    interface ICovarying<in T> where T : Exception
    {
    }

    class Covarying<T> : ICovarying<T> where T : Exception
    {
    }

    class SystemWrap : ICovarying<SystemException>
    {
    }

    class InvalidWrap : ICovarying<InvalidOperationException>
    {
    }
    
    public class Generics
    {
        public static Func<string, string> Hello = name => "hello " + name;

        public static string Hello2(string name) => wrap(Hello)(name);

        // This does NOT retain the name of the arg for hints in the IDE 
        public static Func<string, string> Hello3 = name => wrap(Hello)(name);
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

            Util.WriteLn("Expecting 'hello BRUCE': " + Hello2("bruce"));

            // Still doesn't work
//            var list = new List<ICovarying<Exception>> {new SystemWrap(), new InvalidWrap()};
        }
    }
}