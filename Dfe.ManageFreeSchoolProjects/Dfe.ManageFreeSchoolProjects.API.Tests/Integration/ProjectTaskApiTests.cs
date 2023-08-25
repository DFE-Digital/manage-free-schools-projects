using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
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
        public async Task Patch_SchoolTask_Returns_201()
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
                        ProposedChairOfTrustees = "Lemon Group Ltd"
                    }
                }
            };

            var projectResponse = await UpdateProjectTask(projectId, request);

            projectResponse.SchoolType.Should().Be("Secondary");
            projectResponse.AgeRange.Should().Be("11-18");
            projectResponse.SchoolPhase.Should().Be("Opening");
            projectResponse.Nursery.Should().Be("Yes");
            projectResponse.SixthForm.Should().Be("Yes");
            projectResponse.CompanyName.Should().Be("School Builders Ltd");
            projectResponse.NumberOfCompanyMembers.Should().Be("100");
            projectResponse.ProposedChairOfTrustees.Should().Be("Lemon Group Ltd");
        }

        [Fact]
        public async Task Patch_ConstructionTask_Returns_201()
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
                    Construction = new ConstructionTaskRequest()
                    {
                        NameOfSite = "Lemon Site",
                        AddressOfSite = "Fruitpickers Lane",
                        PostcodeOfSite = "LF124YH",
                        BuildingType = "Brick",
                        TrustRef = "1234ABC",
                        TrustLeadSponsor = "Aviva",
                        TrustName = "Education First",
                        SiteMinArea = "10000",
                        TypeofWorksLocation = "Building site"
                    }
                }
            };

            var projectResponse = await UpdateProjectTask(projectId, request);

            projectResponse.NameOfSite.Should().Be("Lemon Site");
            projectResponse.AddressOfSite.Should().Be("Fruitpickers Lane");
            projectResponse.PostcodeOfSite.Should().Be("LF124YH");
            projectResponse.BuildingType.Should().Be("Brick");
            projectResponse.TrustRef.Should().Be("1234ABC");
            projectResponse.TrustLeadSponsor.Should().Be("Aviva");
            projectResponse.TrustName.Should().Be("Education First");
            projectResponse.SiteMinArea.Should().Be("10000");
            projectResponse.TypeofWorksLocation.Should().Be("Building site");
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

        private async Task<GetProjectResponse> UpdateProjectTask(string projectId, UpdateProjectTasksRequest request)
        {
            var updateTaskResponse = await _client.PatchAsync($"/api/v1/client/projects/{projectId}/tasks", request.ConvertToJson());
            updateTaskResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getProjectResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}");
            getProjectResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await getProjectResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetProjectResponse>>();

            return result.Data;
        }
    }
}
