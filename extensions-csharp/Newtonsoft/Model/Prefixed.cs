using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Newtonsoft.Model
{
    public class Prefixed : NameFilterDef
    {
        [Required]
        [MinLength(2)] // min 1 char plus a separator
        public string Prefix { get; set; } = string.Empty;
    }
}