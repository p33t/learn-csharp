namespace fundamental_c_sharp.PartialClass
{
    public partial class MyClass
    {
        public string MyMethod2()
        {
            // can see private fields in other files
            return _field;
        }
    }
}