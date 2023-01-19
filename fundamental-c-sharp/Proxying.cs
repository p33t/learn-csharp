using System;
using System.Diagnostics;
using System.Reflection;

namespace fundamental_c_sharp
{
    public class MyProxy : DispatchProxy
    {
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine($"Invoked {targetMethod.Name}");
            return targetMethod.Name;
        }
    }

    public interface IMyInterface
    {
        public string Getter1 { get; }
    }
    
    public static class Proxying
    {

        public static void Demo()
        {
            Console.WriteLine("Proxying --------------------");
            var myInterface = DispatchProxy.Create<IMyInterface, MyProxy>();
            var result = myInterface.Getter1;
            Console.WriteLine(result);
            Debug.Assert(result == "get_Getter1");
        }
    }
}