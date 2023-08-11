using Azure.Core;
using ConcernsCaseWork.Service.Base;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Users;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.Data;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
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
        public async Task When_GetByUser_Returns_DashboardForSpecifiedUser_200()
        {
            var firstUser = await CreateUser();

            var secondUser = await CreateUser();

            using var context = _testFixture.GetContext();
            var projectOne = DatabaseModelBuilder.BuildProject();
            var projectTwo = DatabaseModelBuilder.BuildProject();
            var projectThree = DatabaseModelBuilder.BuildProject();

            context.Kpis.AddRange(projectOne, projectTwo, projectThree);

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

            var firstUserResult = await firstUserDashboardResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardByUserResponse>>();

            firstUserResult.Data.Should().HaveCount(2);
            firstUserResult.Data.Should().Contain(r => r.ProjectId == projectOne.Rid);
            firstUserResult.Data.Should().Contain(r => r.ProjectId == projectTwo.Rid);

            // Ensure project three belongs to the second user
            var secondUserDashboardResponse = await _client.GetAsync($"/api/v1/client/dashboard/byuser/{secondUser.Email}");
            secondUserDashboardResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var secondUserResult = await firstUserDashboardResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardByUserResponse>>();
        }

        private async Task<CreateUserRequest> CreateUser()
        {
            var result = _autoFixture.Create<CreateUserRequest>();
            await _client.PostAsync($"/api/v1/client/users", result.ConvertToJson());

            return result;
        }
    }
}
