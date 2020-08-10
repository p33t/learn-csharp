using System;

namespace extensions_csharp.AutoMapping
{
    public class DeepAggregate
    {
        public Address Address { get; set; } = new Address();
        public string FamilyName { get; set; } = String.Empty;
    }
}