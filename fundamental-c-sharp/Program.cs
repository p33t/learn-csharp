namespace fundamental_c_sharp
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        static void Main(string[] args)
        {
            Util.WriteLn("Hello C#");
            Generics.Demo();
            Time.Timestamps();
            ParameterPassing.Demo();
            Constructors.Demo();
            Polymorphism.Demo();
            Functions.Demo();
            Delegates.Demo();
            Reflection.DispatchProxy();
            ExtensionMethod.ExtensionMethod.Demo();
            TaskAsync.Demo().Wait();
            Enums.Demo();
            NullableX.Demo();
            ValueTuples.Demo();
            PartialClass.PartialClass.Demo();
            Attributes.Attributes.Demo();
        }
    }
}
