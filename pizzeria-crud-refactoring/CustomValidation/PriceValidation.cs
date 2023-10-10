using System;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_mvc.CustomValidation
{
    public class PriceValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            double inputValue;
            if (double.TryParse(value?.ToString(), out inputValue))
            {
                if (inputValue > 1 || inputValue < 30)
                {
                    return ValidationResult.Success;
                }
                else
                    return new ValidationResult("Inserisci un numero compreso tra 1 e 30");

            }
            else
                return new ValidationResult("Inserisci un numero");

        }
    }
}