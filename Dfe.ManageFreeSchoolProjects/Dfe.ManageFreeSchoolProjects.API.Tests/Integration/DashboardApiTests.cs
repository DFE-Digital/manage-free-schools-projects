﻿using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Users;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using System.Collections.Generic;
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
        public async Task When_Get_Returns_DashboardFields_200()
        {
            var user = await CreateUser();

            using var context = _testFixture.GetContext();
            var project = DatabaseModelBuilder.BuildProject();

            var dbUser = context.Users.First(u => u.Email == user.Email);
            dbUser.Projects.Add(project);

            await context.SaveChangesAsync();

            var userDashboardResponse = await _client.GetAsync($"/api/v1/client/dashboard?userId={user.Email}");
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
        public async Task When_Get_WithUser_Returns_DashboardForSpecifiedUser_200()
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
            var firstUserDashboardResponse = await _client.GetAsync($"/api/v1/client/dashboard?userId={firstUser.Email}");
            firstUserDashboardResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var firstUserResult = await firstUserDashboardResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            firstUserResult.Data.Should().HaveCount(2);
            firstUserResult.Data.Should().Contain(r => r.ProjectId == projectOne.ProjectStatusProjectId);
            firstUserResult.Data.Should().Contain(r => r.ProjectId == projectTwo.ProjectStatusProjectId);

            // Ensure project three belongs to the second user
            var secondUserDashboardResponse = await _client.GetAsync($"/api/v1/client/dashboard?userId={secondUser.Email}");
            secondUserDashboardResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var secondUserResult = await secondUserDashboardResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            secondUserResult.Data.Should().HaveCount(1);
            secondUserResult.Data.Should().Contain(r => r.ProjectId == projectThree.ProjectStatusProjectId);
        }

        [Fact]
        public async Task When_Get_WithRegion_Returns_DashboardForSpecifiedRegion_200()
        {
            using var context = _testFixture.GetContext();
            var projectOne = DatabaseModelBuilder.BuildProject();
            var projectTwo = DatabaseModelBuilder.BuildProject();
            projectTwo.SchoolDetailsGeographicalRegion = projectOne.SchoolDetailsGeographicalRegion;
            var firstRegion = projectOne.SchoolDetailsGeographicalRegion;

            var projectThree = DatabaseModelBuilder.BuildProject();
            var projectFour = DatabaseModelBuilder.BuildProject();

            context.Kpi.AddRange(projectOne, projectTwo, projectThree, projectFour);

            await context.SaveChangesAsync();


            var firstRegionResponse = await _client.GetAsync($"/api/v1/client/dashboard?regions={firstRegion}");
            firstRegionResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var firstRegionProjects = await firstRegionResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            firstRegionProjects.Data.Should().HaveCount(2);
            firstRegionProjects.Data.Should().Contain(r => r.ProjectId == projectOne.ProjectStatusProjectId);
            firstRegionProjects.Data.Should().Contain(r => r.ProjectId == projectTwo.ProjectStatusProjectId);

            var secondRegion = projectThree.SchoolDetailsGeographicalRegion;
            var secondRegionResponse = await _client.GetAsync($"/api/v1/client/dashboard?regions={firstRegion},{secondRegion}");
            secondRegionResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var secondRegionProjects = await secondRegionResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            secondRegionProjects.Data.Should().HaveCount(3);
            secondRegionProjects.Data.Should().Contain(r => r.ProjectId == projectOne.ProjectStatusProjectId);
            secondRegionProjects.Data.Should().Contain(r => r.ProjectId == projectTwo.ProjectStatusProjectId);
            secondRegionProjects.Data.Should().Contain(r => r.ProjectId == projectThree.ProjectStatusProjectId);
        }

        [Fact]
        public async Task When_Get_WithLocalAuthority_Returns_DashboardForSpecifiedLocalAuthority_200()
        {
            using var context = _testFixture.GetContext();
            var projectOne = DatabaseModelBuilder.BuildProject();
            var projectTwo = DatabaseModelBuilder.BuildProject();
            projectTwo.LocalAuthority = projectOne.LocalAuthority;
            var firstLocalAuthority = projectOne.LocalAuthority;

            var projectThree = DatabaseModelBuilder.BuildProject();
            var projectFour = DatabaseModelBuilder.BuildProject();

            context.Kpi.AddRange(projectOne, projectTwo, projectThree, projectFour);

            await context.SaveChangesAsync();


            var firstLocalAuthorityResponse = await _client.GetAsync($"/api/v1/client/dashboard?localAuthorities={firstLocalAuthority}");
            firstLocalAuthorityResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var firstLocalAuthorityProjects = await firstLocalAuthorityResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            firstLocalAuthorityProjects.Data.Should().HaveCount(2);
            firstLocalAuthorityProjects.Data.Should().Contain(r => r.ProjectId == projectOne.ProjectStatusProjectId);
            firstLocalAuthorityProjects.Data.Should().Contain(r => r.ProjectId == projectTwo.ProjectStatusProjectId);

            var secondLocalAuthority = projectThree.LocalAuthority;
            var secondLocalAuthorityResponse = await _client.GetAsync($"/api/v1/client/dashboard?localAuthorities={firstLocalAuthority},{secondLocalAuthority}");
            secondLocalAuthorityResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var secondLocalAuthorityProjects = await secondLocalAuthorityResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            secondLocalAuthorityProjects.Data.Should().HaveCount(3);
            secondLocalAuthorityProjects.Data.Should().Contain(r => r.ProjectId == projectOne.ProjectStatusProjectId);
            secondLocalAuthorityProjects.Data.Should().Contain(r => r.ProjectId == projectTwo.ProjectStatusProjectId);
            secondLocalAuthorityProjects.Data.Should().Contain(r => r.ProjectId == projectThree.ProjectStatusProjectId);
        }

        [Fact]
        public async Task When_Get_WithProjectTitle_Returns_DashboardForSpecifiedProjectTitle_200()
        {
            using var context = _testFixture.GetContext();
            var projectOne = DatabaseModelBuilder.BuildProject();
            var projectTwo = DatabaseModelBuilder.BuildProject();
            projectTwo.ProjectStatusCurrentFreeSchoolName = projectOne.ProjectStatusCurrentFreeSchoolName;

            var projectThree = DatabaseModelBuilder.BuildProject();

            context.Kpi.AddRange(projectOne, projectTwo, projectThree);

            await context.SaveChangesAsync();

            var firstProjectTitle = projectOne.ProjectStatusCurrentFreeSchoolName;
            var firstProjectTitleResponse = await _client.GetAsync($"/api/v1/client/dashboard?project={firstProjectTitle}");
            firstProjectTitleResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var firstTitleProjects = await firstProjectTitleResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            firstTitleProjects.Data.Should().HaveCount(2);
            firstTitleProjects.Data.Should().Contain(r => r.ProjectId == projectOne.ProjectStatusProjectId);
            firstTitleProjects.Data.Should().Contain(r => r.ProjectId == projectTwo.ProjectStatusProjectId);

            var secondProjectTitle = projectThree.ProjectStatusCurrentFreeSchoolName.Substring(0, 12);
            var secondProjectTitleResponse = await _client.GetAsync($"/api/v1/client/dashboard?project={secondProjectTitle}");
            secondProjectTitleResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var secondTitleProjects = await secondProjectTitleResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            secondTitleProjects.Data.Should().HaveCount(1);
            secondTitleProjects.Data.Should().Contain(r => r.ProjectId == projectThree.ProjectStatusProjectId);
        }

        [Fact]
        public async Task When_Get_WithProjectId_Returns_DashboardForSpecifiedProjectId_200()
        {
            using var context = _testFixture.GetContext();
            var projectOne = DatabaseModelBuilder.BuildProject();
            var projectTwo = DatabaseModelBuilder.BuildProject();

            context.Kpi.AddRange(projectOne, projectTwo);
            await context.SaveChangesAsync();

            var firstProjectId = projectOne.ProjectStatusCurrentFreeSchoolName;
            var firstProjectTitleResponse = await _client.GetAsync($"/api/v1/client/dashboard?project={projectOne.ProjectStatusProjectId}");
            firstProjectTitleResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var firstIdProjects = await firstProjectTitleResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            firstIdProjects.Data.Should().HaveCount(1);
            firstIdProjects.Data.Should().Contain(r => r.ProjectId == projectOne.ProjectStatusProjectId);

            var secondProjectIdResponse = await _client.GetAsync($"/api/v1/client/dashboard?project={projectTwo.ProjectStatusProjectId}");
            secondProjectIdResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var secondIdProjects = await secondProjectIdResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            secondIdProjects.Data.Should().HaveCount(1);
            secondIdProjects.Data.Should().Contain(r => r.ProjectId == projectTwo.ProjectStatusProjectId);
        }

        [Fact]
        public async Task When_Get_UserDoesNotExist_Returns_EmptyDashboard_200()
        {
            var firstUserDashboardResponse = await _client.GetAsync($"/api/v1/client/dashboard?userId=NotExist");
            firstUserDashboardResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await firstUserDashboardResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            result.Data.Should().BeEmpty();
        }

        // Since the data isn't ordered we can't check specific projects are returned
        // For now just checking the count
        // TODO: Check specific projects
        [Fact]
        public async Task When_Get_Pagination_Returns_Dashboard_200()
        {
            using var context = _testFixture.GetContext();
            var projectOne = DatabaseModelBuilder.BuildProject();
            var projectTwo = DatabaseModelBuilder.BuildProject();
            var projectThree = DatabaseModelBuilder.BuildProject();
            var projectFour = DatabaseModelBuilder.BuildProject();
            var projectFive = DatabaseModelBuilder.BuildProject();
            var projectSix = DatabaseModelBuilder.BuildProject();

            var projects = new List<Kpi>()
            {
                projectOne,
                projectTwo,
                projectThree,
                projectFour,
                projectFive,
                projectSix
            };

            var user = await CreateUser();
            var dbUser = context.Users.First(u => u.Email == user.Email);
            dbUser.Projects.AddRange(projects);

            context.Kpi.AddRange(projectOne, projectTwo, projectThree, projectFour, projectFive, projectSix);

            await context.SaveChangesAsync();

            var pageOneResponse = await _client.GetAsync($"/api/v1/client/dashboard?userId={user.Email}&page=1&count=2");
            pageOneResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var pageOneResult = await pageOneResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            pageOneResult.Data.Should().HaveCount(2);
            pageOneResult.Paging.RecordCount.Should().Be(6);
            pageOneResult.Paging.Page.Should().Be(1);
            pageOneResult.Paging.HasNext.Should().BeTrue();
            pageOneResult.Paging.HasPrevious.Should().BeFalse();
            pageOneResult.Paging.TotalPages.Should().Be(3);

            var pageTwoResponse = await _client.GetAsync($"/api/v1/client/dashboard?userId={user.Email}&page=3&count=2");
            pageTwoResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var pageTwoResult = await pageTwoResponse.Content.ReadFromJsonAsync<ApiListWrapper<GetDashboardResponse>>();

            pageTwoResult.Data.Should().HaveCount(2);
            pageTwoResult.Paging.Page.Should().Be(3);
            pageTwoResult.Paging.HasNext.Should().BeFalse();
            pageTwoResult.Paging.HasPrevious.Should().BeTrue();
        }

        private async Task<CreateUserRequest> CreateUser()
        {
            var result = _autoFixture.Create<CreateUserRequest>();
            await _client.PostAsync($"/api/v1/client/users", result.ConvertToJson());

            return result;
        }
    }
}
