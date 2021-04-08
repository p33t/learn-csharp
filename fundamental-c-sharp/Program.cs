using System;
using System.Threading.Tasks;

namespace fundamental_c_sharp
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        static async Task Main(string[] args)
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
            Reflection.PropertyInvocation();
            ExtensionMethod.ExtensionMethod.Demo();
            // Too slow... TaskAsync.Demo().Wait();
            Enums.Demo();
            NullableX.Demo();
            ValueTuples.Demo();
            PartialClass.PartialClass.Demo();
            Attributes.Attributes.Demo();
            DateTimes.Demo();
            DateTimeOffsets.Demo();
            Casting.Demo();
            Equality.Demo();
            Expressions.Demo();
            Switches.Demo();
            await TaskAsync.Demo();
            TaskWait.Demo();
            await TaskExceptions.Demo();
            ClassInitialization.Demo();

            Console.WriteLine("Done.");
        }
    }
}
