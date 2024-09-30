using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PreFundingAgreementCheckpointMeeting;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project.Tasks
{
    public class PreFundingAgreementCheckpointMeetingTaskSummaryBuilderTests
    {
        [Theory]
        [InlineData("FS - Presumption", true)]
        [InlineData("Wave 2", false)]
        public void Build(string applicationWave, bool isHidden)
        {
            // Arrange
            var builder = new PreFundingAgreementCheckpointMeetingTaskSummaryBuilder();
            var parameters = new PreFundingAgreementCheckpointMeetingTaskSummaryBuilderParameters
            {
                ApplicationWave = applicationWave,
                TaskSummary = new TaskSummaryResponse
                {
                    Name = TaskName.PreFundingAgreementCheckpointMeeting.ToString(),
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
