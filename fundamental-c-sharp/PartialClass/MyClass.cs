namespace fundamental_c_sharp.PartialClass
{
    public partial class MyClass : IMyClass
    {
        private readonly string _field;
        
        public MyClass(string fieldValue)
        {
            _field = fieldValue;
        }

        public string MyMethod1()
        {
            return "some-string";
        }        
    }
}