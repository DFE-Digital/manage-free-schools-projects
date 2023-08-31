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
using System.Collections.Generic;

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
            var proj = _autoFixture.Create<CreateProjectRequest>();
            List<CreateProjectRequest> request = new List<CreateProjectRequest>()
            {
                proj
            };

            //Reduce these string lengths to avoid truncation errors
            foreach (CreateProjectRequest p in request)
            {
                p.ProjectId = DatabaseModelBuilder.CreateProjectId();
            }

            var result = await _client.PostAsync($"/api/v1/client/project/create", request.ConvertToJson());

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            using var context = _testFixture.GetContext();

            var createdProject = context.Kpi.First(p => p.ProjectStatusProjectId == request[0].ProjectId);

            createdProject.ProjectStatusProjectId.Should().Be(request[0].ProjectId);
            createdProject.ProjectStatusCurrentFreeSchoolName.Should().Be(request[0].SchoolName);
            createdProject.ProjectStatusFreeSchoolsApplicationNumber.Should().NotBeNullOrEmpty();
            createdProject.ProjectStatusFreeSchoolApplicationWave.Should().NotBeNullOrEmpty();

        }

        [Fact]
        public async Task When_CreateProjectBulk_Returns_NewProjectFields_201()
        {
            var proj1 = _autoFixture.Create<CreateProjectRequest>();
            var proj2 = _autoFixture.Create<CreateProjectRequest>();
            var proj3 = _autoFixture.Create<CreateProjectRequest>();

            List<CreateProjectRequest> request = new List<CreateProjectRequest>()
            {
                proj1,
                proj2,
                proj3
            };

            //Reduce these string lengths to avoid truncation errors
            foreach (CreateProjectRequest p in request)
            {
                p.ProjectId = DatabaseModelBuilder.CreateProjectId();
            }

            var result = await _client.PostAsync($"/api/v1/client/project/create", request.ConvertToJson());

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            using var context = _testFixture.GetContext();

            var createdProject1 = context.Kpi.First(p => p.ProjectStatusProjectId == request[0].ProjectId);
            var createdProject2 = context.Kpi.First(p => p.ProjectStatusProjectId == request[1].ProjectId);
            var createdProject3 = context.Kpi.First(p => p.ProjectStatusProjectId == request[2].ProjectId);

            createdProject1.ProjectStatusProjectId.Should().Be(request[0].ProjectId);
            createdProject2.ProjectStatusProjectId.Should().Be(request[1].ProjectId);
            createdProject3.ProjectStatusProjectId.Should().Be(request[2].ProjectId);

        }

        [Fact]
        public async Task When_CreateProject_Returns_Duplicate_422()
        {
            var proj1 = _autoFixture.Create<CreateProjectRequest>();

            List<CreateProjectRequest> request = new List<CreateProjectRequest>()
            {
                proj1,
            };

            //Reduce these string lengths to avoid truncation errors
            foreach (CreateProjectRequest p in request)
            {
                p.ProjectId = DatabaseModelBuilder.CreateProjectId();
            }


            var result = await _client.PostAsync($"/api/v1/client/project/create", request.ConvertToJson());

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            using var context = _testFixture.GetContext();

            var createdProject1 = context.Kpi.First(p => p.ProjectStatusProjectId == request[0].ProjectId);

            createdProject1.ProjectStatusProjectId.Should().Be(request[0].ProjectId);


            //Create another request
            var proj2 = _autoFixture.Create<CreateProjectRequest>();

            List<CreateProjectRequest> request2 = new List<CreateProjectRequest>()
            {
                proj2,
            };

            //Reduce these string lengths to avoid truncation errors
            foreach (CreateProjectRequest p in request2)
            {
                p.ProjectId = DatabaseModelBuilder.CreateProjectId();
            }

            //Set the school name to an existing school name
            request2[0].SchoolName = request[0].SchoolName;

            var result2 = await _client.PostAsync($"/api/v1/client/project/create", request2.ConvertToJson());

            result2.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}

