namespace extensions_csharp.SystemTextJson.Model
{
    // Doesn't help with newtonsoft schema generation
    // [KnownType(typeof(TestConfigV1))]
    public abstract class TestConfig
    {
        public abstract int FormatVersion { get; }
    }
}