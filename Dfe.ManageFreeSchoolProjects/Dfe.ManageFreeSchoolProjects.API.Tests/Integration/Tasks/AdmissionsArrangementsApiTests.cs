using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class AdmissionsArrangementsApiTests : ApiTestsBase
    {
        public AdmissionsArrangementsApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_NewAdmissionsArrangements_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                AdmissionsArrangements = new()
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.AdmissionsArrangements.ToString());

            // TODO: Add field assertions
        }

        [Fact]
        public async Task Patch_ExistingAdmissionsArragements_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                AdmissionsArrangements = new()
            };

            await _client.UpdateProjectTask(projectId, request, TaskName.AdmissionsArrangements.ToString());

            var updateRequest = new UpdateProjectByTaskRequest()
            {
                AdmissionsArrangements = new()
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.AdmissionsArrangements.ToString());

            //TODO: Add field assertions
        }

    }
}
