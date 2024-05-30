using System.ComponentModel.DataAnnotations;

namespace CoffeeStoreApplication.Validations
{
    public class DateTimeAgeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                var now = DateTime.Now;
                var eighteenYearsAgo = now.AddYears(-18);
                if (dateTime > eighteenYearsAgo)
                {
                    return new ValidationResult("The date of birth must be 18 years or older.");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid date format.");
        }
    }
}
