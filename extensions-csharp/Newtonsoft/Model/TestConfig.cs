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
        public Component1Config Component1 { get; set; }
    }
}