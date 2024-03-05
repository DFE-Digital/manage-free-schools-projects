using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Utils;
using System.Collections.Generic;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

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
            
            var tasks = TasksStub.BuildListOfTasks(project.Rid);
            context.Tasks.AddRange(tasks);
            
            await context.SaveChangesAsync();

            var taskListResponse = await _client.GetAsync($"/api/v1/client/projects/{project.ProjectStatusProjectId}/tasks/summary");
            taskListResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await taskListResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectByTaskSummaryResponse>>();

            var result = content.Data;

            result.School.Name.Should().Be("School");
            result.School.Status.Should().Be(ProjectTaskStatus.NotStarted);
            result.Dates.Name.Should().Be("Dates");
            result.Dates.Status.Should().Be(ProjectTaskStatus.NotStarted);
            result.Trust.Name.Should().Be("Trust");
            result.Trust.Status.Should().Be(ProjectTaskStatus.NotStarted);
            result.Constituency.Name.Should().Be("Constituency");
            result.Constituency.Status.Should().Be(ProjectTaskStatus.NotStarted);
            result.KickOffMeeting.Name.Should().Be("KickOffMeeting");
            result.KickOffMeeting.Status.Should().Be(ProjectTaskStatus.NotStarted);
            result.ModelFundingAgreement.Name.Should().Be("ModelFundingAgreement");
            result.ModelFundingAgreement.Status.Should().Be(ProjectTaskStatus.NotStarted);
            result.StatutoryConsultation.Name.Should().Be("StatutoryConsultation");
            result.StatutoryConsultation.Status.Should().Be(ProjectTaskStatus.NotStarted);
            result.ArticlesOfAssociation.Name.Should().Be("ArticlesOfAssociation");
            result.ArticlesOfAssociation.Status.Should().Be(ProjectTaskStatus.NotStarted);
            result.FinancePlan.Name.Should().Be("FinancePlan");
            result.FinancePlan.Status.Should().Be(ProjectTaskStatus.NotStarted);

            result.DraftGovernancePlan.Name.Should().Be("DraftGovernancePlan");
            result.DraftGovernancePlan.Status.Should().Be(ProjectTaskStatus.NotStarted);
        }
    }
}
