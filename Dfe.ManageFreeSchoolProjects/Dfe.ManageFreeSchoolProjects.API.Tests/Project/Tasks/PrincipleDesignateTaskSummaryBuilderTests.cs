using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PrincipleDesignate;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project.Tasks;

public class PrincipleDesignateTaskSummaryBuilderTests
{
    [Theory]
    [InlineData("FS - Presumption", true)]
    [InlineData("otherWaveType", false)]
    public void Build(string waveType, bool isHidden)
    {
        // Arrange
        var builder = new PrincipleDesignateTaskSummaryBuilder();
        var parameters = new PrincipleDesignateTaskSummaryBuilderParameters
        {
            ApplicationWave = waveType,
            TaskSummary = new TaskSummaryResponse
            {
                Name = TaskName.PrincipleDesignate.ToString(),
                Status = ProjectTaskStatus.NotStarted,
            }
        };

        // Act
        var actual = builder.Build(parameters);

        // Assert
        actual.IsHidden.Should().Be(isHidden);
    }
}
