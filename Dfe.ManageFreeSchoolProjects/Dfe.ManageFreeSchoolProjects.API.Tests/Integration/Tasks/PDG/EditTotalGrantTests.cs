using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Task = System.Threading.Tasks.Task;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks.PDG;

[Collection(ApiTestCollection.ApiTestCollectionName)]
public class EditTotalGrantTests(ApiTestFixture apiTestFixture) : ApiTestsBase(apiTestFixture)
{
    [Fact]
    public async Task Patch_NewTotalGrant_ReturnsResponseWithRevisedGrant()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        var poWithGrants = DatabaseModelBuilder.BuildGrants(project.Rid);

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);
        context.Po.Add(poWithGrants);
        await context.SaveChangesAsync();

        var request = new UpdateProjectByTaskRequest { PDGGrantTask = new PDGGrantTask { InitialGrant = 12500.0m, RevisedGrant = 25000.0m } };

        var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.PDG.ToString());

        projectResponse.PDGDashboard.InitialGrant.Should().Be(12500.00m);
        projectResponse.PDGDashboard.RevisedGrant.Should().Be(25000.00m);
    }

    [Fact]
    public async Task Patch_NewTotalGrant_WhenUpdatingNullSavesAsNull ()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        var poWithGrants = DatabaseModelBuilder.BuildGrants(project.Rid);

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);
        context.Po.Add(poWithGrants);
        await context.SaveChangesAsync();

        var request = new UpdateProjectByTaskRequest { PDGGrantTask = new PDGGrantTask { InitialGrant = null,  RevisedGrant = null } };

        var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.PDG.ToString());

        projectResponse.PDGDashboard.InitialGrant.Should().Be(null);
        projectResponse.PDGDashboard.RevisedGrant.Should().Be(null);
    }

}

