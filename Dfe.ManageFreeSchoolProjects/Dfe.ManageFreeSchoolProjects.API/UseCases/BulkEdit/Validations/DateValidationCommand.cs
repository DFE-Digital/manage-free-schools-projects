using System.Globalization;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class DateValidationCommand() : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(ValidationCommandParameters<BulkEditDto> parameters)
        {
            // var culture = CultureInfo.CreateSpecificCulture("en-GB");
            // var IsValid = DateTime.TryParse(parameters.Value, culture, out var date);
            //
            // if (!IsValid)
            // {
            //     return new()
            //     {
            //         IsValid = false,
            //         ErrorMessage = "Enter a valid date. For example, 27/03/2021"
            //     };
            // }
            //
            // if(date.Year > 2050 || date.Year < 2000)
            // {
            //     return new()
            //     {
            //         IsValid = false,
            //         ErrorMessage = "Year must be between 2000 and 2050"
            //     };
            // }
            //
            // return new()
            // {
            //     IsValid = true,
            //     ErrorMessage = null
            // };

            return Validate(parameters.Value);
        }

        private static ValidationResult Validate(string actualOpeningDateString)
        {
            //verify what format they'll input date
            var validDate = DateTime.TryParse(actualOpeningDateString, CultureInfo.CreateSpecificCulture("en-GB"), DateTimeStyles.AssumeLocal, out var actualOpeningDate);
            if (!validDate)
                return new () { IsValid = false, ErrorMessage = "Enter a valid date. For example, 27/03/2021" };
            
            var missingParts = new List<string>();

            if (actualOpeningDate.Day == 0) 
                missingParts.Add("day");
            if (actualOpeningDate.Month == 0)
                missingParts.Add("month");
            if (actualOpeningDate.Year == 0) 
                missingParts.Add("year");

            if (missingParts.Count == 3)
                return new() { IsValid = false, ErrorMessage = "Date must include a day, month and year." };

            if (missingParts.Count > 0)
                return new() { IsValid = false, ErrorMessage = $"Date must include a {string.Join(" and ", missingParts)}" };

            var daysInCurrentMonth = DateTime.DaysInMonth(actualOpeningDate.Year, actualOpeningDate.Month);
            if (actualOpeningDate.Day < 1 || actualOpeningDate.Day > daysInCurrentMonth)
                return new() { IsValid = false, ErrorMessage = $"Day must be between 1 and {daysInCurrentMonth}" };
            
            if (actualOpeningDate.Month is < 1 or > 12)
                return new() { IsValid = false, ErrorMessage = $"Month must be between 1 and 12" };

            return new() { IsValid = true };
        }
    }
}