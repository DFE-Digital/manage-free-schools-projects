using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit;
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
        [InlineData("123", "Select an existing project status")]
        [InlineData("a", "Select an existing project status")]
        [InlineData("Cancelledd", "Select an existing project status")]
        public void StatusValidationFails(string date, string error)
        {
            var projectStatusValidation = new ProjectStatusValidationCommand();
            var dto = new BulkEditDto { ApplicationWave = "Any Wave" };
            var validationResult = projectStatusValidation.Execute(dto, date);

            validationResult.IsValid.Should().BeFalse();
            validationResult.ErrorMessage.Should().Be(error);
        }

        [Theory]
        [InlineData("Pre-opening")]
        [InlineData("Open")]
        [InlineData("Closed")]
        [InlineData("Cancelled")]
        [InlineData("Withdrawn in pre-opening")]
        [InlineData("Application Competition stage")]
        [InlineData("Application stage")]
        [InlineData("Open - not included in figures")]
        [InlineData("Pre-opening - not included in figures")]
        [InlineData("Rejected")]
        [InlineData("Withdrawn in application stage")]
        [InlineData("Cancelled in pre-opening")]
        public void StatusValidationPasses(string date)
        {
            var projectStatusValidation = new ProjectStatusValidationCommand();
            var dto = new BulkEditDto { ApplicationWave = "Any Wave" };
            var validationResult = projectStatusValidation.Execute(dto, date);

            validationResult.IsValid.Should().BeTrue();
            validationResult.ErrorMessage.Should().BeNull();

        }

        [Theory]
        [InlineData("Pre-opening", true)]
        [InlineData("Open", true)]
        [InlineData("Closed", true)]
        [InlineData("Cancelled", true)]
        [InlineData("Withdrawn in pre-opening", true)]
        [InlineData("Application competition stage", false)]
        [InlineData("Application stage", false)]
        [InlineData("Open - not included in figures", false)]
        [InlineData("Pre-opening - not included in figures", false)]
        [InlineData("Rejected", false)]
        [InlineData("Withdrawn in application stage", false)]
        [InlineData("Cancelled in pre-opening", true)]
        public void PresumptionFailsOnCertainStatus(string date, bool pass)
        {
            var projectStatusValidation = new ProjectStatusValidationCommand();
            var dto = new BulkEditDto { ApplicationWave = "FS - Presumption" };
            var validationResult = projectStatusValidation.Execute(dto, date);

            validationResult.IsValid.Should().Be(pass);

            if (pass)
            {
                validationResult.ErrorMessage.Should().BeNull();
            }
            else
            {
                validationResult.ErrorMessage.Should().Be("Select a presumption route project status");
            }
        }
    }
}
