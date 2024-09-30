using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DueDiligenceChecks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project.Tasks
{
    public class DueDiligenceChecksTaskSummaryBuilderTests
    {
        [Theory]
        [InlineData("FS - Presumption", true)]
        [InlineData("Wave 2", false)]
        public void Build(string applicationWave, bool isHidden)
        {
            // Arrange
            var builder = new DueDiligenceChecksTaskSummaryBuilder();
            var parameters = new DueDiligenceChecksTaskSummaryBuilderParameters
            {
                ApplicationWave = applicationWave,
                TaskSummary = new TaskSummaryResponse
                {
                    Name = TaskName.DueDiligenceChecks.ToString(),
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
