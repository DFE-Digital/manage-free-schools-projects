using System.Globalization;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class DateValidationCommand() : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(ValidationCommandParameters<BulkEditDto> parameters)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-GB");
            var IsValid = DateTime.TryParse(parameters.Value, culture, out var date);

            if (!IsValid)
            {
                return new()
                {
                    IsValid = false,
                    ErrorMessage = "Enter a valid date. For example, 27/03/2021"
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

        private static ValidationResult Validate(string actualOpeningDateString)
        {
            //verify what format they'll input date
            var validDate = DateTime.TryParseExact(actualOpeningDateString, "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out var actualOpeningDate);
            if (!validDate)
                return new ValidationResult { IsValid = false, ErrorMessage = "Enter a date in the correct format" };
            
            
            
            
            List<string> missingParts = new List<string>();

            if (string.IsNullOrWhiteSpace(dayInput)) missingParts.Add("day");
            if (string.IsNullOrWhiteSpace(monthInput)) missingParts.Add("month");
            if (string.IsNullOrWhiteSpace(yearInput)) missingParts.Add("year");

        }
    }
}