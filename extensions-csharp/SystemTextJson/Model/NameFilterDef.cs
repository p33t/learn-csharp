namespace extensions_csharp.SystemTextJson.Model
{
    // Doesn't help with schema generation by Newtonsoft Schema
    // [KnownType(typeof(EligibleList))]
    // [KnownType(typeof(Prefixed))]
    // Also doesn't help with schema generation by Newtonsoft Schema
    // [JsonSubtypes.KnownSubType(typeof(EligibleList), "EligibleList")]
    // [JsonSubtypes.KnownSubType(typeof(Prefixed), "Prefixed")]
    public abstract class NameFilterDef
    {
        public abstract string Subtype { get; }
    }
}