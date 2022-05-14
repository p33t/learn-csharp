using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Newtonsoft.Model
{
    /// <summary>
    /// Configuration format version 1
    /// </summary>
    public class V1
    {
        [Required] public string Name { get; set; }
    }
}