using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Newtonsoft.Model
{
    public class Prefixed : NameFilterDef
    {
        [Required]
        public string Prefix { get; set; }
    }
}