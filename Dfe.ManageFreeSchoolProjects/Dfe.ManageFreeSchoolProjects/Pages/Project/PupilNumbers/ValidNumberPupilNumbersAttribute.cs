using System;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.PupilNumbers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
    public class ValidNumberPupilNumbersAttribute : ValidationAttribute
    {
        public ValidNumberPupilNumbersAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            var valueAsString = value.ToString();

            bool success = int.TryParse(valueAsString, out int valueAsInt);

            if (!success)
                return new ValidationResult($"{validationContext.DisplayName} must be a number");

            if (valueAsInt < 0)
                return new ValidationResult(string.Format("{0} must be greater than 0", validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
