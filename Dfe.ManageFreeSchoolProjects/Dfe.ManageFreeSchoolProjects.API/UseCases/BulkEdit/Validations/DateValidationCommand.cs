using System.Globalization;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class DateValidationCommand : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(ValidationCommandParameters<BulkEditDto> parameters)
        {
            var actualOpeningDateString = parameters.Value;

            var dateParts = actualOpeningDateString.Split('/');
            if (dateParts.Length != 3)
                return new ValidationResult
                    { IsValid = false, ErrorMessage = "Enter a valid date. For example, 27/03/2021" };

            var dateObject = new { Day = dateParts[0], Month = dateParts[1], Year = dateParts[2] };
            
            var missingParts = new List<string>();

            if (string.IsNullOrEmpty(dateObject.Day)) missingParts.Add("day");
            if (string.IsNullOrEmpty(dateObject.Month)) missingParts.Add("month");
            if (string.IsNullOrEmpty(dateObject.Year)) missingParts.Add("year");

            if (missingParts.Count == 3)
                return new ValidationResult
                    { IsValid = false, ErrorMessage = "Date must include a day, month, and year" };

            if (missingParts.Count > 0)
                return new ValidationResult
                    { IsValid = false, ErrorMessage = $"Date must include a {string.Join(" and ", missingParts)}" };

            if (!int.TryParse(dateObject.Day, out var day) || day < 1 || day > 31)
                return new ValidationResult
                    { IsValid = false, ErrorMessage = "Day must be a valid number between 1 and 31" };

            if (!int.TryParse(dateObject.Month, out var month) || month < 1 || month > 12)
                return new ValidationResult
                    { IsValid = false, ErrorMessage = "Month must be a valid number between 1 and 12" };

            if (!int.TryParse(dateObject.Year, out var year) || year < 2000 || year > 2050)
                return new ValidationResult { IsValid = false, ErrorMessage = "Year must be between 2000 and 2050" };

            var daysInCurrentMonth = DateTime.DaysInMonth(year, month);
            if (day > daysInCurrentMonth)
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = $"Day must be between 1 and {daysInCurrentMonth} for the given month."
                };

            return new ValidationResult { IsValid = true };
        }
    }
}