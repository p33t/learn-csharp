using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace extensions_csharp.Validation
{
    public static class Validation
    {
        public static void Demo()
        {
            Console.WriteLine("Validation ===================================");
            var model = new Appointment();
            var result = new List<ValidationResult>();
            Validator.TryValidateObject(model, new ValidationContext(model), result, true);
            // Doesn't seem to be necessary
            // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);    
            Debug.Assert(result.Count == 1);
            var error0MemberNames = result[0].MemberNames.ToArray();
            Debug.Assert(error0MemberNames.Length == 1);
            Debug.Assert(error0MemberNames[0] == "Description");
            Debug.Assert(result[0].ErrorMessage == "The Description field is required.");

            // Custom validation attribute
            model.FromIn = new DateTime(2021, 3, 28, 22, 40, 0);
            model.ToEx = model.FromIn.AddSeconds(-1);
            model.Description = "Random description";
            result.Clear();
            Validator.TryValidateObject(model, new ValidationContext(model), result, true);
            Debug.Assert(result.Count == 1);
            error0MemberNames = result[0].MemberNames.ToArray();
            Debug.Assert(error0MemberNames.Length == 2);
            
            // IValidatableObject
            model.ToEx = model.FromIn;
            model.Description = "Bad";
            result.Clear();
            Validator.TryValidateObject(model, new ValidationContext(model), result, true);
            Debug.Assert(result.Count == 1);
            error0MemberNames = result[0].MemberNames.ToArray();
            Debug.Assert(error0MemberNames.Length == 1);
            Debug.Assert(error0MemberNames[0] == "Description");
            Debug.Assert(result[0].ErrorMessage == "Cannot have 'Bad' description");
        }
    }
}