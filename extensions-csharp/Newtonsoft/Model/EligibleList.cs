using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Newtonsoft.Model
{
    public class EligibleList : NameFilterDef
    {
        [Required]
        public IList<string> Eligible { get; set; } = ImmutableList<string>.Empty;
    }
}