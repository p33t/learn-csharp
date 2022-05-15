using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Newtonsoft.Model
{
    /// <summary>
    /// An implementation of NameFilterDef that requires a name to have a given prefix.
    /// </summary>
    public class Prefixed : NameFilterDef
    {
        [Required]
        [MinLength(2)] // min 1 char plus a separator
        public string Prefix { get; set; } = string.Empty;
    }
}