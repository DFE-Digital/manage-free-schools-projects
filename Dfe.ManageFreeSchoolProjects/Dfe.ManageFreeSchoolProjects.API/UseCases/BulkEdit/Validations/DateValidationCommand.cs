using System.Globalization;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class DateValidationCommand() : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(BulkEditDto data, string value)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-GB");
            var IsValid = DateTime.TryParse(value, culture, out var date);

            if (!IsValid)
            {
                return new()
                {
                    IsValid = false,
                    ErrorMessage = IsValid ? null : $"Date format is not valid"
                };
            }

            if(date.Year > 2050 || date.Year < 2000)
            {
                return new()
                {
                    IsValid = false,
                    ErrorMessage = "Year must be between 2000 and 2050"
                };
            }

            return new()
            {
                IsValid = true,
                ErrorMessage = null
            };
        }
    }
}