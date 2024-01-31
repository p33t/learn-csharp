using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace extensions_csharp.Validation
{
    public class Appointment : IValidatableObject
    {
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [NonNegativePeriod]
        public DateTime FromIn { get; set; }
        
        [RegularExpression("^Xxx$", ErrorMessage = "Must be 'Xxx'")]
        public string MustBeXxx { get; set; } = "Xxx";
        
        public DateTime ToEx { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var appointment = validationContext.ObjectInstance as Appointment;
            if (appointment!.Description == "Bad")
                return new[]
                {
                    new ValidationResult("Cannot have 'Bad' description", new[] {nameof(Appointment.Description)})
                };
            return ImmutableArray<ValidationResult>.Empty;
        }
    }
}