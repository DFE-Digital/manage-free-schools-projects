using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class AuditTests : ApiTestsBase
    {
        public AuditTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Audit_User()
        {
            var project = await CreateProject();
            await UpdateMilestones(project.ProjectId);

            var context = _testFixture.GetContext();
            var kpi = await context.Kpi.FirstAsync(x => x.ProjectStatusProjectId == project.ProjectId);
            var user = await context.Users.FirstAsync(x => x.Email == "API.TestFixture@test.gov.uk");
            var milestones = await context.Milestones.FirstAsync(x => x.Rid == kpi.Rid);
            var po = await context.Po.FirstAsync(x => x.Rid == kpi.Rid);
            var rag = await context.Rag.FirstAsync(x => x.Rid == kpi.Rid);
            var riskAppraisalMeeting = await context.RiskAppraisalMeetingTask.FirstAsync(x => x.RID == kpi.Rid);
            var tasks = await context.Tasks.Where(x => x.Rid == kpi.Rid).ToListAsync();

            kpi.UpdatedByUserId.Should().Be(user.Id);
            // po.UpdatedByUserId.Should().Be(user.Id);
            // milestones.UpdatedByUserId.Should().Be(user.Id);
            // rag.UpdatedByUserId.Should().Be(user.Id);
            // riskAppraisalMeeting.UpdatedByUserId.Should().Be(user.Id);
            // tasks.UpdatedByUserId.Should().Be(user.Id);

        }

        private async Task<ProjectResponseDetails> CreateProject()
        {
            using var context = _testFixture.GetContext();
            var trust = DatabaseModelBuilder.BuildTrust();

            context.Trust.Add(trust);
            await context.SaveChangesAsync();

            var projectDetails = _autoFixture.Create<ProjectDetails>();

            var request = new CreateProjectRequest();
            projectDetails.TRN = trust.TrustRef;
            request.Projects.Add(projectDetails);

            var projectId = DatabaseModelBuilder.CreateProjectId();
            request.Projects[0].ProjectId = projectId;

            var createProjectResponse = await _client.PostAsync($"/api/v1/client/projects/create", request.ConvertToJson());
            createProjectResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createProjectContent = await createProjectResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<CreateProjectResponse>>();

            return createProjectContent.Data.Projects[0];
        }

        private async Task UpdateMilestones(string projectId)
        {
            var request = new UpdateProjectByTaskRequest()
            {
                DraftGovernancePlan = _autoFixture.Create<DraftGovernancePlanTask>()
            };

            await _client.UpdateProjectTask(projectId, request, TaskName.School.ToString());
        }
    }
}
