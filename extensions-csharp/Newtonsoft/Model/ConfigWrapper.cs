using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Newtonsoft.Model
{
    /// <summary>
    /// A wrapper class to get around validation and polymorphism problems at the root config level.
    /// </summary>
    public class ConfigWrapper
    {
        [Required]
        public TestConfig Config { get; set; }
    }
}