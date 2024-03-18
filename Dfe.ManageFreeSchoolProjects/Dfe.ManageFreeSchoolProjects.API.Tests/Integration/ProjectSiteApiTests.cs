using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Sites;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ProjectSiteApiTests : ApiTestsBase
    {
        public ProjectSiteApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task When_SitesDoNotExist_Returns_200()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var updateSiteRequest = _autoFixture.Create<UpdateProjectSitesRequest>();

            var updateSiteResponse = await _client.PatchAsync($"/api/v1/client/projects/{projectId}/sites", updateSiteRequest.ConvertToJson());
            updateSiteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getProjectSitesResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/sites");
            getProjectSitesResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await getProjectSitesResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetProjectSitesResponse>>();

            var actualPermenantSite = content.Data.PermenantSite;
            var actualTemporarySite = content.Data.TemporarySite;

            AssertProjectSite(content.Data.PermenantSite, updateSiteRequest.PermenantSite);
            AssertProjectSite(content.Data.TemporarySite, updateSiteRequest.TemporarySite);
        }

        [Fact]
        public async Task When_SitesExist_Returns_200()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var setSitesRequest = _autoFixture.Create<UpdateProjectSitesRequest>();

            var setSiteResponse = await _client.PatchAsync($"/api/v1/client/projects/{projectId}/sites", setSitesRequest.ConvertToJson());
            setSiteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updateSiteRequest = _autoFixture.Create<UpdateProjectSitesRequest>();

            var updateSiteResponse = await _client.PatchAsync($"/api/v1/client/projects/{projectId}/sites", updateSiteRequest.ConvertToJson());
            updateSiteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getProjectSitesResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/sites");
            getProjectSitesResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await getProjectSitesResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetProjectSitesResponse>>();

            var actualPermenantSite = content.Data.PermenantSite;
            var actualTemporarySite = content.Data.TemporarySite;

            AssertProjectSite(content.Data.PermenantSite, updateSiteRequest.PermenantSite);
            AssertProjectSite(content.Data.TemporarySite, updateSiteRequest.TemporarySite);
        }

        [Fact]
        public async Task When_Get_ProjectDoesNotExist_Returns_404()
        {
            var projectId = Guid.NewGuid().ToString();

            var getProjectSitesResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/sites");
            getProjectSitesResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task When_Patch_ProjectDoesNotExist_Returns_404()
        {
            var projectId = Guid.NewGuid().ToString();
            var updateSiteRequest = _autoFixture.Create<UpdateProjectSitesRequest>();

            var updateSiteResponse = await _client.PatchAsync($"/api/v1/client/projects/{projectId}/sites", updateSiteRequest.ConvertToJson());
            updateSiteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private static void AssertProjectSite(ProjectSite actual, ProjectSite expected)
        {
            actual.Address.AddressLine1.Should().Be(expected.Address.AddressLine1);
            actual.Address.AddressLine2.Should().Be(expected.Address.AddressLine2);
            actual.Address.Postcode.Should().Be(expected.Address.Postcode);
        } 
    }
}
