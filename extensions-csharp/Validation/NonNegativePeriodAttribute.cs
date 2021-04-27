using System.ComponentModel.DataAnnotations;

namespace extensions_csharp.Validation
{
    public class NonNegativePeriodAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // This throws NotImplementedException
            // var result = base.IsValid(value, validationContext);
            //
            // if (result != ValidationResult.Success)
            //     return result;

            var appointment = validationContext.ObjectInstance as Appointment;
            if (appointment == null)
            {
                var msg = $"{nameof(NonNegativePeriodAttribute)} only operates on {nameof(Appointment)} models";
                return new ValidationResult(msg);
            }

            if (appointment.FromIn > appointment.ToEx)
            {
                var msg = "From cannot be after To";
                return new ValidationResult(msg, new[] {nameof(Appointment.FromIn), nameof(appointment.ToEx)});
            }
            
            return ValidationResult.Success;
        }
    }
}