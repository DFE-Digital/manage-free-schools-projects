using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreementHealthCheck;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project.Tasks
{
    public class FundingAgreementHealthCheckTaskSummaryBuilderTests
    {
        [Theory]
        [InlineData("FS - Presumption", false)]
        [InlineData("Wave 2", true)]
        public void Build(string applicationWave, bool isHidden)
        {
            // Arrange
            var builder = new FundingAgreementHealthCheckTaskSummaryBuilder();
            var parameters = new FundingAgreementHealthCheckTaskSummaryBuilderParameters
            {
                ApplicationWave = applicationWave,
                TaskSummary = new TaskSummaryResponse
                {
                    Name = TaskName.FundingAgreementHealthCheck.ToString(),
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
