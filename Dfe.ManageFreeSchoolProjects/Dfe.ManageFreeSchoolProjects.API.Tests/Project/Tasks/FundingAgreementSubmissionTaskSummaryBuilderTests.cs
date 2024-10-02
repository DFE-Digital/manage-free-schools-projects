using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreementSubmission;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project.Tasks
{
    public class FundingAgreementSubmissionTaskSummaryBuilderTests
    {
        [Theory]
        [InlineData("FS - Presumption", true)]
        [InlineData("Wave 2", false)]
        public void Build(string applicationWave, bool isHidden)
        {
            // Arrange
            var builder = new FundingAgreementSubmissionTaskSummaryBuilder();
            var parameters = new FundingAgreementSubmissionTaskSummaryBuilderParameters
            {
                ApplicationWave = applicationWave,
                TaskSummary = new TaskSummaryResponse
                {
                    Name = TaskName.FundingAgreementSubmission.ToString(),
                    Status = ProjectTaskStatus.NotStarted,
                }
            };

            // Act
            var actual = builder.Build(parameters);

            // Assert
            actual.IsHidden.Should().Be(isHidden);
        }
    }
}
