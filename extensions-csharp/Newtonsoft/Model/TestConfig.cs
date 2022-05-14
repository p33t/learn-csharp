using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Newtonsoft.Model
{
    /// <summary>
    /// Configuration format version 1
    /// </summary>
    public class TestConfig
    {
        [Required] 
        public string Name { get; set; }

        [Required] 
        public NameFilterDef NameFilterDef { get; set; }
    }
}