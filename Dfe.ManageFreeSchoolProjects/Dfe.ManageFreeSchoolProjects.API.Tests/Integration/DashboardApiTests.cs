using ConcernsCaseWork.Service.Base;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Users;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using FluentAssertions;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
	[Collection(ApiTestCollection.ApiTestCollectionName)]
    public class DashboardApiTests : ApiTestsBase
    {
        public DashboardApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task When_GetByUser_Returns_DashboardFields_200()
        {
            var user = await CreateUser();

            using var context = _testFixture.GetContext();
            var project = DatabaseModelBuilder.BuildProject();

            var dbUser = context.Users.First(u => u.Email == user.Email);
            dbUser.Projects.Add(project);

            await context.SaveChangesAsync();

            var userDashboardResponse = await _client.GetAsync($"/api/v1/client/dashboard/byuser/{user.Email}");
            userDashboardResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await userDashboardResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            result.Data.Should().HaveCount(1);

            var dashboard = result.Data.First();

            dashboard.ProjectId.Should().Be(project.ProjectStatusProjectId);
            dashboard.ProjectTitle.Should().Be(project.ProjectStatusCurrentFreeSchoolName);
            dashboard.TrustName.Should().Be(project.TrustName);
            dashboard.LocalAuthority.Should().Be(project.LocalAuthority);
            dashboard.RealisticOpeningDate.Should().NotBeNullOrEmpty();
            dashboard.Region.Should().Be(project.SchoolDetailsGeographicalRegion);
            dashboard.Status.Should().Be("1");
        }

        [Fact]
        public async Task When_GetByUser_Returns_DashboardForSpecifiedUser_200()
        {
            var firstUser = await CreateUser();

            var secondUser = await CreateUser();

            using var context = _testFixture.GetContext();
            var projectOne = DatabaseModelBuilder.BuildProject();
            var projectTwo = DatabaseModelBuilder.BuildProject();
            var projectThree = DatabaseModelBuilder.BuildProject();

            context.Kpi.AddRange(projectOne, projectTwo, projectThree);

            await context.SaveChangesAsync();

            var dbFirstUser = context.Users.First(u => u.Email == firstUser.Email);
            dbFirstUser.Projects.Add(projectOne);
            dbFirstUser.Projects.Add(projectTwo);

            var dbSecondUser = context.Users.First(u => u.Email == secondUser.Email);
            dbSecondUser.Projects.Add(projectThree);

            await context.SaveChangesAsync();

            // Ensure project one and two belong to the first user
            var firstUserDashboardResponse = await _client.GetAsync($"/api/v1/client/dashboard/byuser/{firstUser.Email}");
            firstUserDashboardResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var firstUserResult = await firstUserDashboardResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            firstUserResult.Data.Should().HaveCount(2);
            firstUserResult.Data.Should().Contain(r => r.ProjectId == projectOne.ProjectStatusProjectId);
            firstUserResult.Data.Should().Contain(r => r.ProjectId == projectTwo.ProjectStatusProjectId);

            // Ensure project three belongs to the second user
            var secondUserDashboardResponse = await _client.GetAsync($"/api/v1/client/dashboard/byuser/{secondUser.Email}");
            secondUserDashboardResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var secondUserResult = await secondUserDashboardResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            secondUserResult.Data.Should().HaveCount(1);
            secondUserResult.Data.Should().Contain(r => r.ProjectId == projectThree.ProjectStatusProjectId);
        }

        [Fact]
        public async Task When_GetByUser_UserDoesNotExist_Returns_EmptyDashboard_200()
        {
            var firstUserDashboardResponse = await _client.GetAsync($"/api/v1/client/dashboard/byuser/NotExist");
            firstUserDashboardResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await firstUserDashboardResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            result.Data.Should().BeEmpty();
        }

        private async Task<CreateUserRequest> CreateUser()
        {
            var result = _autoFixture.Create<CreateUserRequest>();
            await _client.PostAsync($"/api/v1/client/users", result.ConvertToJson());

            return result;
        }

        [Fact]
        public async Task When_GetAll_Returns_Dashboard_200()
        {

            using var context = _testFixture.GetContext();
            var projectOne = DatabaseModelBuilder.BuildProject();
            var projectTwo = DatabaseModelBuilder.BuildProject();
            var projectThree = DatabaseModelBuilder.BuildProject();

            context.Kpis.AddRange(projectOne, projectTwo, projectThree);

            await context.SaveChangesAsync();

            // Ensure project one and two belong to the first user
            var dashboardResponse = await _client.GetAsync($"/api/v1/client/dashboard/all");
            dashboardResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await dashboardResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            result.Data.Should().Contain(r => r.ProjectId == projectOne.ProjectStatusProjectId);
            result.Data.Should().Contain(r => r.ProjectId == projectTwo.ProjectStatusProjectId);
            result.Data.Should().Contain(r => r.ProjectId == projectThree.ProjectStatusProjectId);
        }
    }
}
