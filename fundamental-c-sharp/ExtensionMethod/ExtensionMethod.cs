using System.Diagnostics;
using fundamental_c_sharp.ExtensionMethod.Fixture;

namespace fundamental_c_sharp.ExtensionMethod
{
    public static class ExtensionMethod
    {
        public static void Demo()
        {
            var bareClass = new BareClass {StrField = "hello"};
            // This requires 'using ...Fixture'
            // Or 'using static ...Fixture.Helper'
            var revStr = bareClass.RevStr();
            Trace.Assert(revStr == "olleh", $"Expected olleh but got {revStr}");
        }
    }
}