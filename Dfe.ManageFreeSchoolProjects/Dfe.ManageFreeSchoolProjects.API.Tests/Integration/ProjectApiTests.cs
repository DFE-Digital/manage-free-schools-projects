using ConcernsCaseWork.Service.Base;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
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
	public class ProjectApiTests : ApiTestsBase
	{
		public ProjectApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
		{
		}

        [Fact]
        public async Task When_CreateProject_Returns_NewProjectFields_201()
        {
            var request = _autoFixture.Create<CreateProjectRequest>();

            //Reduce these string lengths to avoid truncation errors
            request.ProjectId = request.ProjectId.Substring(0, 24);

            var result = await _client.PostAsync($"/api/v1/client/project/create", request.ConvertToJson());

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            using var context = _testFixture.GetContext();

            var createdProject = context.Kpi.First(p => p.ProjectStatusProjectId == request.ProjectId);

            createdProject.ProjectStatusProjectId.Should().Be(request.ProjectId);
            createdProject.ProjectStatusCurrentFreeSchoolName.Should().Be(request.SchoolName);
            createdProject.ProjectStatusFreeSchoolsApplicationNumber.Should().Be(request.ApplicationNumber);
            createdProject.ProjectStatusFreeSchoolApplicationWave.Should().Be(request.ApplicationWave);

        }
    }
}

