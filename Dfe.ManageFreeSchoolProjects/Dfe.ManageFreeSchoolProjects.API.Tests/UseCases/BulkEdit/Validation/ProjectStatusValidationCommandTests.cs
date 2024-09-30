using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases.BulkEdit.Validation
{
    public class ProjectStatusValidationCommandTests
    {
        [Theory]
        [InlineData("123", "Project Status is not in possible list of statuses")]
        [InlineData("a", "Project Status is not in possible list of statuses")]
        [InlineData("Cancelledd", "Project Status is not in possible list of statuses")]
        public void StatusValidationFails(string date, string error)
        {
            var projectStatusValidation = new ProjectStatusValidationCommand();
            var validationResult = projectStatusValidation.Execute(null, date);

            validationResult.IsValid.Should().BeFalse();
            validationResult.ErrorMessage.Should().Be(error);
        }

        [Theory]
        [InlineData("Pre-opening")]
        [InlineData("Open")]
        [InlineData("Closed")]
        [InlineData("Cancelled")]
        [InlineData("Withdrawn in pre-opening")]
        [InlineData("Application competition stage")]
        [InlineData("Application stage")]
        [InlineData("Open - not included in figures")]
        [InlineData("Pre-opening - not included in figures")]
        [InlineData("Rejected")]
        [InlineData("Withdrawn in application stage")]
        [InlineData("Cancelled in pre-opening")]
        public void StatusValidationPasses(string date)
        {
            var projectStatusValidation = new ProjectStatusValidationCommand();
            var validationResult = projectStatusValidation.Execute(null, date);

            validationResult.IsValid.Should().BeTrue();
            validationResult.ErrorMessage.Should().BeNull();

        }
    }
}
