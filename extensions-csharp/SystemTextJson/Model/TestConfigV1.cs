using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.SystemTextJson.Model
{
    /// <summary>
    /// Configuration format version 1
    /// </summary>
    public class TestConfigV1 : TestConfig
    {
        public override int FormatVersion => 1;
        
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;
        //
        // [Required] 
        // public NameFilterDef NameFilterDef { get; set; }
    }
}