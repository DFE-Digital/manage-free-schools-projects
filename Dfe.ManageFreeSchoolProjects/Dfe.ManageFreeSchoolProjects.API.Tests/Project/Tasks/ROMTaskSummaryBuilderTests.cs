using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ReadinessToOpenMeeting;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project.Tasks
{
    public class ReadinessToOpenMeetingTaskSummaryBuilderTests
    {
        [Theory]
        [InlineData("FS - Presumption", true)]
        [InlineData("Wave 2", false)]
        public void Build(string applicationWave, bool isHidden)
        {
            // Arrange
            var builder = new ROMTaskSummaryBuilder();
            var parameters = new ROMTaskSummaryBuilderParameters
            {
                ApplicationWave = applicationWave,
                TaskSummary = new TaskSummaryResponse
                {
                    Name = TaskName.ReadinessToOpenMeeting.ToString(),
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
