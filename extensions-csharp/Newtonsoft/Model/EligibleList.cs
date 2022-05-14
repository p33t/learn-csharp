using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Newtonsoft.Model
{
    public class EligibleList : NameFilterDef
    {
        public static string TypeString => "EligibleList";

        public override string Type => TypeString;

        [Required]
        public IList<string> Eligible { get; set; } = ImmutableList<string>.Empty;
    }
}