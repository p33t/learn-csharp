using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace extensions_csharp.Validation
{
    public static class Validation
    {
        public static void Demo()
        {
            Console.WriteLine("Validation ===================================");
            var model = new Appointment();
            var validationContext = new ValidationContext(model);
            var result = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, result);
            // Doesn't seem to be necessary
            // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);    
            Debug.Assert(result.Count == 1);
            var error0MemberNames = result[0].MemberNames.ToArray();
            Debug.Assert(error0MemberNames.Length == 1);
            Debug.Assert(error0MemberNames[0] == "Description");
            Debug.Assert(result[0].ErrorMessage == "The Description field is required.");
        }
    }
}