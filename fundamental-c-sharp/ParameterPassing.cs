using System;
using System.Security.Cryptography;

namespace fundamental_c_sharp
{
    public class ParameterPassing
    {
        class TestClass
        {
            public string Str { get; set; } = string.Empty;
        }
        
        public static void Demo()
        {
            var tcRef = new TestClass {Str = "Ref"};
            var tcRefOrig = tcRef;
            
            var tc = new TestClass{Str = "Nothing"};
            TestClass output;
            MyMethod(tc, ref tcRef, out output);
            
            Check(tcRef != tcRefOrig, "'ref' did not assign back to input.  What's the point?");
            Check(tcRef.Str == "Ciao", "Should have the new value");
            Check(output.Str == "Goodbye", "Should have been assigned as 'out'");
            Check(tc.Str == "Grr", "Should be able to change non-ref / non-out objects");
        }

        private static void MyMethod(TestClass tcNone, ref TestClass tcRef, out TestClass tcOut)
        {
            // Doesn't like
//            tcOut.Str = "bruce";

            tcNone.Str = "Grr";

            tcRef.Str = "Hello";
            tcRef = new TestClass {Str = "Ciao"};

            tcOut = new TestClass{Str = "Goodbye"};
        }

        private static void Check(bool b, string msg)
        {
            if (!b) throw new Exception(msg);
        }
    }
}