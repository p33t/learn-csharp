using System.Diagnostics;

namespace fundamental_c_sharp
{
    public static class Equality
    {
        public static void Demo()
        {
            var s1 = "1";
            var s2 = "2";
            var s1B = new string(s1);
            var s2B = s2;
            Trace.Assert(s1 == s1B); // == has been overridden in string
            Trace.Assert(s1 != s2);
            Trace.Assert(ReferenceEquals(s2, s2B)); // Can always compare references
        }
    }
}