using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Validation
{
    public class Appointment : IValidatableObject
    {
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [NonNegativePeriod]
        public DateTime FromIn { get; set; }
        
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