using Azure.Core;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.API.UseCases.ProjectTask;
using Dfe.ManageFreeSchoolProjects.Data.Entities;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ProjectTaskApiTests : ApiTestsBase
    {
        public ProjectTaskApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_DatesTask_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var request = new UpdateProjectTasksRequest()
            {
                Tasks = new ProjectTaskRequest()
                {
                    School = new SchoolTaskRequest()
                    {
                        SchoolType = "Secondary",
                        AgeRange = "11-18",
                        SchoolPhase = "Opening",
                        Nursery = "Yes",
                        SixthForm = "Yes",
                        CompanyName = "School Builders Ltd",
                        NumberOfCompanyMembers = "100",
                        ProposedChairOfTrustees = "Lemon Group LTD"
                    }
                }
            };

            await UpdateProjectTask(projectId, request);
        }

        [Fact]
        public async Task Patch_Task_NoProjectExists_Returns_404()
        {
            var request = new UpdateProjectTasksRequest()
            {
            };

            var updateTaskResponse = await _client.PatchAsync($"/api/v1/client/projects/NotExist/tasks", request.ConvertToJson());
            updateTaskResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private async Task UpdateProjectTask(string projectId, UpdateProjectTasksRequest request)
        {
            var updateTaskResponse = await _client.PatchAsync($"/api/v1/client/projects/{projectId}/tasks", request.ConvertToJson());
            updateTaskResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
