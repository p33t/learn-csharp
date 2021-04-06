using System;
using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Validation
{
    public class Appointment
    {
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [NonNegativePeriod]
        public DateTime FromIn { get; set; }
        
        public DateTime ToEx { get; set; }
    }
}