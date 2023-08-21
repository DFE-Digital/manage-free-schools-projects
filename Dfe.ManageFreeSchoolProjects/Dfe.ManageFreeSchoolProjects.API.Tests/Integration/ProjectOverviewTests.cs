using ConcernsCaseWork.Service.Base;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ProjectOverviewTests : ApiTestsBase
    {
        public ProjectOverviewTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task When_Get_AllFieldsSet_Returns_200()
        {
            using var context = _testFixture.GetContext();
            var project = DatabaseModelBuilder.BuildProject();

            context.Kpis.Add(project);
            await context.SaveChangesAsync();

            var overviewResponse = await _client.GetAsync($"/api/v1/client/project/overview/{project.Rid}");
            overviewResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await overviewResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardByUserResponse>>();

            // Project status
            overview
        }

        public async Task When_Get_MandatoryFieldsSet_Returns_200()
        {
            throw new NotImplementedException("Not implemented");
        }

        public async Task When_Get_ProjectNotFound_Returns_400()
        {
            throw new NotImplementedException("Not implemented");
        }
    }
}
