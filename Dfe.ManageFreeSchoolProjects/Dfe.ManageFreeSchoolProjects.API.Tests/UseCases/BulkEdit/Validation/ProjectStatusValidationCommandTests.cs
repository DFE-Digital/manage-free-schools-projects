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
        public void StatusValidationFails(string status, string error)
        {
            var projectStatusValidation = new ProjectStatusValidationCommand();
            var dto = new BulkEditDto { ApplicationWave = "Any Wave" };
            var validationResult = projectStatusValidation.Execute(new() { Data = dto, Value = status });

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
        public void StatusValidationPasses(string status)
        {
            var projectStatusValidation = new ProjectStatusValidationCommand();
            var dto = new BulkEditDto { ApplicationWave = "Any Wave" };
            var validationResult = projectStatusValidation.Execute(new() { Data = dto, Value = status });

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
        public void PresumptionFailsOnCertainStatus(string status, bool pass)
        {
            var projectStatusValidation = new ProjectStatusValidationCommand();
            var dto = new BulkEditDto { ApplicationWave = "FS - Presumption" };
            var validationResult = projectStatusValidation.Execute(new() { Data = dto, Value = status });

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


        [Theory]
        [InlineData("Pre-opening", true)]
        [InlineData("Open", true)]
        [InlineData("Closed", true)]
        [InlineData("Cancelled", true)]
        [InlineData("Withdrawn in pre-opening", true)]
        [InlineData("Application competition stage", true)]
        [InlineData("Application stage", true)]
        [InlineData("Open - not included in figures", true)]
        [InlineData("Pre-opening - not included in figures", true)]
        [InlineData("Rejected", true)]
        [InlineData("Withdrawn in application stage", true)]
        [InlineData("Cancelled in pre-opening", true)]
        [InlineData("Cancelled in pre-openingg", false)]
        [InlineData("", false)]
        public void IgnoresProjectIfNotFoundCertainStatus(string status, bool pass)
        {
            var projectStatusValidation = new ProjectStatusValidationCommand();
            var validationResult = projectStatusValidation.Execute(new() { Data = null, Value = status });

            validationResult.IsValid.Should().Be(pass);

            if (pass)
            {
                validationResult.ErrorMessage.Should().BeNull();
            }
            else
            {
                validationResult.ErrorMessage.Should().NotBeNullOrEmpty();
            }
        }
    }
}
