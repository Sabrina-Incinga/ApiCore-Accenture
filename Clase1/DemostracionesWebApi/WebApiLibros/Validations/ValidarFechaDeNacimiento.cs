using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiLibros.Validations
{
    public class ValidarFechaDeNacimiento: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            if(value is DateTime && (DateTime)value < new DateTime(1950, 01, 01))
            {
                return new ValidationResult("La fecha de nacimiento debe ser posterior al 01/01/1950");
            }
            return ValidationResult.Success;
        }
    }
}
