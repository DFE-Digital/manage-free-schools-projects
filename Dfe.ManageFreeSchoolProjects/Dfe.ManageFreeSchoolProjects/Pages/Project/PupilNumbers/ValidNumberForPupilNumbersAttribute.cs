using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.PupilNumbers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
    public class ValidNumberForPupilNumbersAttribute : ValidationAttribute
    {
        public ValidNumberForPupilNumbersAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            var valueAsString = value.ToString();

            bool success = int.TryParse(valueAsString, out int valueAsInt);

            if (!success)
                return new ValidationResult($"{validationContext.DisplayName} must be a number, like 30");

            if (valueAsInt < 0)
                return new ValidationResult(string.Format("{0} must be 0 or more", validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
