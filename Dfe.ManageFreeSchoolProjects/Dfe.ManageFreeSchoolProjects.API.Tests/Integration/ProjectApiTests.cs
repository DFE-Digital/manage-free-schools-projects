using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
	public class ProjectApiTests : ApiTestsBase
	{
		public ProjectApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
		{
		}

        [Fact]
        public async Task When_CreateProject_Returns_NewProjectFields_201()
        {
            using var setupContext = _testFixture.GetContext();
            var trust = DatabaseModelBuilder.BuildTrust();

            setupContext.Trust.Add(trust);
            await setupContext.SaveChangesAsync();

            var projectDetails = _autoFixture.Create<ProjectDetails>();
            var request = new CreateProjectRequest();
            projectDetails.TRN = trust.TrustRef;
            request.Projects.Add(projectDetails);

            var projectId = DatabaseModelBuilder.CreateProjectId();
            request.Projects[0].ProjectId = projectId;

            var createProjectResponse = await _client.PostAsync($"/api/v1/client/projects/create", request.ConvertToJson());

            createProjectResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createProjectContent  = await createProjectResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<CreateProjectResponse>>();

            createProjectContent.Data.Projects.Should().HaveCount(1);
            createProjectContent.Data.Projects[0].ProjectId.Should().Be(projectId);

            var projectOverivewResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/overview");
            projectOverivewResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var projectOverivewContent = await projectOverivewResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectOverviewResponse>>();
            var projectOverview = projectOverivewContent.Data;
            

            projectOverview.SchoolDetails.TrustId.Should().Be(request.Projects[0].TRN);
            projectOverview.ProjectStatus.ProjectId.Should().Be(projectDetails.ProjectId);
            projectOverview.ProjectStatus.CurrentFreeSchoolName.Should().Be(projectDetails.SchoolName);
            projectOverview.SchoolDetails.Region.Should().Be(request.Projects[0].Region);
            projectOverview.SchoolDetails.LocalAuthority.Should().Be(request.Projects[0].LocalAuthority);
            projectOverview.ProjectStatus.FreeSchoolsApplicationNumber.Should().BeNullOrEmpty();
            projectOverview.ProjectStatus.ApplicationWave.Should().BeNullOrEmpty();
            projectOverview.SchoolDetails.SchoolType.Should().Be(request.Projects[0].SchoolType);

        }

        [Fact]
        public async Task When_CreateProjectBulk_Returns_NewProjectFields_201()
        {
            using var setupContext = _testFixture.GetContext();
            var trust1 = DatabaseModelBuilder.BuildTrust();
            var trust2 = DatabaseModelBuilder.BuildTrust();
            var trust3 = DatabaseModelBuilder.BuildTrust();

            setupContext.Trust.Add(trust1);
            setupContext.Trust.Add(trust2);
            setupContext.Trust.Add(trust3);
            await setupContext.SaveChangesAsync();

            var proj1 = _autoFixture.Create<ProjectDetails>();
            var proj2 = _autoFixture.Create<ProjectDetails>();
            var proj3 = _autoFixture.Create<ProjectDetails>();

            CreateProjectRequest request = new CreateProjectRequest();

            proj1.TRN = trust1.TrustRef;
            proj2.TRN = trust2.TrustRef;
            proj3.TRN = trust3.TrustRef;

            request.Projects.AddRange(new List<ProjectDetails>
            {
                proj1,
                proj2,
                proj3
            });

            foreach (ProjectDetails p in request.Projects)
            {
                p.ProjectId = DatabaseModelBuilder.CreateProjectId();
            }

            var result = await _client.PostAsync($"/api/v1/client/projects/create", request.ConvertToJson());

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            using var context = _testFixture.GetContext();

            var createdProject1 = context.Kpi.First(p => p.ProjectStatusProjectId == request.Projects[0].ProjectId);
            var createdProject2 = context.Kpi.First(p => p.ProjectStatusProjectId == request.Projects[1].ProjectId);
            var createdProject3 = context.Kpi.First(p => p.ProjectStatusProjectId == request.Projects[2].ProjectId);

            createdProject1.ProjectStatusProjectId.Should().Be(request.Projects[0].ProjectId);
            createdProject2.ProjectStatusProjectId.Should().Be(request.Projects[1].ProjectId);
            createdProject3.ProjectStatusProjectId.Should().Be(request.Projects[2].ProjectId);

        }

        [Fact]
        public async Task When_CreateProject_Returns_Duplicate_422()
        {
            using var setupContext = _testFixture.GetContext();
            var trust = DatabaseModelBuilder.BuildTrust();

            setupContext.Trust.Add(trust);
            await setupContext.SaveChangesAsync();

            var proj1 = _autoFixture.Create<ProjectDetails>();

            CreateProjectRequest request = new CreateProjectRequest();
            proj1.TRN = trust.TrustRef;
            request.Projects.Add(proj1);

            //Reduce these string lengths to avoid truncation errors
            request.Projects[0].ProjectId = DatabaseModelBuilder.CreateProjectId();

            var result = await _client.PostAsync($"/api/v1/client/projects/create", request.ConvertToJson());

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            using var context = _testFixture.GetContext();

            var createdProject1 = context.Kpi.First(p => p.ProjectStatusProjectId == request.Projects[0].ProjectId);

            createdProject1.ProjectStatusProjectId.Should().Be(request.Projects[0].ProjectId);

            //Create another request
            var proj2 = _autoFixture.Create<ProjectDetails>();

            CreateProjectRequest request2 = new CreateProjectRequest();
            proj2.TRN = trust.TrustRef;
            request2.Projects.Add(proj2);
            request2.Projects[0].ProjectId = request.Projects[0].ProjectId;

            var result2 = await _client.PostAsync($"/api/v1/client/projects/create", request2.ConvertToJson());

            result2.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}

