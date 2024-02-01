using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace fundamental_c_sharp
{
    public static class DynamicObject
    {
        public static void Demo()
        {
            Console.WriteLine("DynamicObject ========================");

            var o1 = new {one=1, two=2};
            Console.WriteLine($"Type: {o1.GetType()}"); // <>f__AnonymousType0`2[System.Int32,System.Int32]

            Trace.Assert(o1.GetType().GetProperty("one") != null);
            Trace.Assert(o1.GetType().GetProperty("three") == null);
            
            
            // var json = JsonSerializer.Serialize(o1);
        }
    }
}
