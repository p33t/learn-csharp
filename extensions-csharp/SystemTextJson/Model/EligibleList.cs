using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.SystemTextJson.Model
{
    /// <summary>
    /// Indicates an implementation of NameFilterDef that defines eligible names explicitly.
    /// </summary>
    public class EligibleList : NameFilterDef
    {
        public override string Subtype => "EligibleList";
        
        [Required]
        public IList<string> Eligible { get; set; } = ImmutableList<string>.Empty;
    }
}