using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Newtonsoft.Model
{
    public abstract class NameFilterDef
    {
        [Required]
        public virtual string Type => string.Empty;
    }
}