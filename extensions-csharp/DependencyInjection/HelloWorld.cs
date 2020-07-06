namespace extensions_csharp.DependencyInjection
{
    public class HelloWorld : IHello
    {
        public string Hello()
        {
            return "World";
        }
    }
}