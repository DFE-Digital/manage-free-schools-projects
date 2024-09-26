using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PrincipalDesignate;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project.Tasks;

public class PrincipalDesignateTaskSummaryBuilderTests
{
    [Theory]
    [InlineData("FS - Presumption", false)]
    [InlineData("otherWaveType", false)]
    public void Build(string waveType, bool isHidden)
    {
        // Arrange
        var builder = new PrincipalDesignateTaskSummaryBuilder();
        var parameters = new PrincipalDesignateTaskSummaryBuilderParameters
        {
            ApplicationWave = waveType,
            TaskSummary = new TaskSummaryResponse
            {
                Name = TaskName.PrincipalDesignate.ToString(),
                Status = ProjectTaskStatus.NotStarted,
            }
        };

        // Act
        var actual = builder.Build(parameters);

        // Assert
        actual.IsHidden.Should().Be(isHidden);
    }
}
