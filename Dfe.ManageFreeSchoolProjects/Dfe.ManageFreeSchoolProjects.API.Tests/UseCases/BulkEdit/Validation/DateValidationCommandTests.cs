using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases.BulkEdit.Validation
{
    public class DateValidationCommandTests
    {
        [Theory]
        [InlineData("g", "Date format is not valid")]
        [InlineData("12", "Date format is not valid")]
        [InlineData("12-13-2025", "Date format is not valid")]
        [InlineData("12/10/2051", "Year must be between 2000 and 2050")]
        [InlineData("12/10/1999", "Year must be between 2000 and 2050")]
        public void DateValidationFails(string date, string error)
        {
            var dateValidation = new DateValidationCommand();
            var validationResult = dateValidation.Execute(null, date);

            validationResult.IsValid.Should().BeFalse();
            validationResult.ErrorMessage.Should().Be(error);
            
        }

        [Theory]
        [InlineData("12-10-2025")]
        [InlineData("12/10/2050")]
        [InlineData("12/10/2000")]
        public void DateValidationPasses(string date)
        {
            var dateValidation = new DateValidationCommand();
            var validationResult = dateValidation.Execute(null, date);

            validationResult.IsValid.Should().BeTrue();
            validationResult.ErrorMessage.Should().BeNull();

        }
    }
}
