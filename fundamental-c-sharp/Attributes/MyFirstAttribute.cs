using System;

namespace fundamental_c_sharp.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyFirstAttribute : Attribute
    {
        public string StrVal { get; set; }
    }
}