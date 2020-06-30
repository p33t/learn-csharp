using System;
using System.Linq;
using fundamental_c_sharp.ExtensionMethod;

namespace fundamental_c_sharp.ExtensionMethod.Fixture
{
    public static class Helper
    {
        public static string RevStr(this BareClass self)
        {
            var arr = self.StrField.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}