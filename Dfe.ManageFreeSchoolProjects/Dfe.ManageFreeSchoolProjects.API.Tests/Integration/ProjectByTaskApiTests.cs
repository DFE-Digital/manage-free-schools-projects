using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Utils;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ProjectTaskApiTests : ApiTestsBase
    {
        public ProjectTaskApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Get_ProjectByTask_NoDependentDataCreated_Returns_200()
        {
            // Ensures that if the child tables for the tasks are not populated, the api still works
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var tasks = TasksStub.BuildListOfTasks(project.Rid);
            context.Tasks.AddRange(tasks);

            await context.SaveChangesAsync();

            var getProjectByTaskResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/tasks");
            getProjectByTaskResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await getProjectByTaskResponse.Content
                .ReadFromJsonAsync<ApiSingleResponseV2<GetProjectByTaskResponse>>();

            result.Data.Construction.AddressOfSite.Should().BeNull();
            result.Data.Construction.BuildingType.Should().BeNull();
        }

        [Fact]
        public async Task Get_ProjectByTask_DoesNotExist_Returns_404()
        {
            var getProjectByTaskResponse = await _client.GetAsync($"/api/v1/client/projects/NotExist/tasks");
            getProjectByTaskResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        //TODO: fix unit test for new fields
        [Fact]
        public async Task Patch_SchoolTask_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var tasks = TasksStub.BuildListOfTasks(project.Rid);
            context.Tasks.AddRange(tasks);

            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                School = new SchoolTask()
                {
                    CurrentFreeSchoolName = "Test High School",
                    SchoolType = SchoolType.Mainstream,
                    AgeRange = "11-18",
                    SchoolPhase = SchoolPhase.Primary,
                    Nursery = "No",
                    SixthForm = "No",
                    FaithStatus = FaithStatus.None,
                    FaithType = FaithType.Other,
                    Gender = Gender.Mixed
                }
            };

            var projectResponse = await UpdateProjectTask(projectId, request);

            projectResponse.School.CurrentFreeSchoolName.Should().Be("Test High School");
            projectResponse.School.SchoolType.Should().Be(SchoolType.Mainstream);
            projectResponse.School.SchoolPhase.Should().Be(SchoolPhase.Primary);
            projectResponse.School.AgeRange.Should().Be("11-18");
            projectResponse.School.Nursery.Should().Be("No");
            projectResponse.School.SixthForm.Should().Be("No");
        }

        [Fact]
        public async Task Patch_ConstructionTask_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                Construction = new ConstructionTask()
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
            };

            var projectResponse = await UpdateProjectTask(projectId, request);

            projectResponse.Construction.NameOfSite.Should().Be("Lemon Site");
            projectResponse.Construction.AddressOfSite.Should().Be("Fruitpickers Lane");
            projectResponse.Construction.PostcodeOfSite.Should().Be("LF124YH");
            projectResponse.Construction.BuildingType.Should().Be("Brick");
            projectResponse.Construction.TrustRef.Should().Be("1234ABC");
            projectResponse.Construction.TrustLeadSponsor.Should().Be("Aviva");
            projectResponse.Construction.TrustName.Should().Be("Education First");
            projectResponse.Construction.SiteMinArea.Should().Be("10000");
            projectResponse.Construction.TypeofWorksLocation.Should().Be("Building site");
        }

        [Fact]
        public async Task Patch_DatesTask_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var DateTenDaysInFuture = new DateTime().AddDays(10);
            var DateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
                Dates = new DatesTask()
                {
                    DateOfEntryIntoPreopening = DateTenDaysInFuture,
                    ProvisionalOpeningDateAgreedWithTrust = DateNineDaysInFuture,
                    RealisticYearOfOpening = "2023 2024",
                }
            };

            var projectResponse = await UpdateProjectTask(projectId, request);

            projectResponse.Dates.DateOfEntryIntoPreopening.Should().Be(DateTenDaysInFuture);
            projectResponse.Dates.ProvisionalOpeningDateAgreedWithTrust.Should().Be(DateNineDaysInFuture);
            projectResponse.Dates.RealisticYearOfOpening.Should().Be("2023 2024");
        }

        [Fact]
        public async Task Patch_Task_NoProjectExists_Returns_404()
        {
            var request = new UpdateProjectByTaskRequest()
            {
            };

            var updateTaskResponse =
                await _client.PatchAsync($"/api/v1/client/projects/NotExist/tasks", request.ConvertToJson());
            updateTaskResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private async Task<GetProjectByTaskResponse> UpdateProjectTask(string projectId,
            UpdateProjectByTaskRequest request)
        {
            var updateTaskResponse =
                await _client.PatchAsync($"/api/v1/client/projects/{projectId}/tasks", request.ConvertToJson());
            updateTaskResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getProjectByTaskResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/tasks");
            getProjectByTaskResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await getProjectByTaskResponse.Content
                .ReadFromJsonAsync<ApiSingleResponseV2<GetProjectByTaskResponse>>();

            return result.Data;
        }
    }
}