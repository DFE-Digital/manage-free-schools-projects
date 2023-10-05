using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class GetProjectTaskSummaryApiTests : ApiTestsBase
    {
        public GetProjectTaskSummaryApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task GetProjectTaskList_Returns_200()
        {
            using var context = _testFixture.GetContext();
            var project = DatabaseModelBuilder.BuildProject();

            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var taskListResponse = await _client.GetAsync($"/api/v1/client/projects/{project.ProjectStatusProjectId}/tasks/summary");
            taskListResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await taskListResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectByTaskSummaryResponse>>();

            var result = content.Data;

            result.School.Name.Should().Be("School");
            result.School.Status.Should().Be(ProjectTaskStatus.NotStarted);
            result.Construction.Name.Should().Be("Construction");
            result.Construction.Status.Should().Be(ProjectTaskStatus.InProgress);
            result.Dates.Name.Should().Be("Dates");
            result.Dates.Status.Should().Be(ProjectTaskStatus.NotStarted);
        }
    }
}
