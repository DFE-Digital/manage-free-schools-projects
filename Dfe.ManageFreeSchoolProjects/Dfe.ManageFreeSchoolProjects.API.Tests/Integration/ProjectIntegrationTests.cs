using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ProjectIntegrationTests
    {
        private readonly Fixture _autoFixture;
        private readonly HttpClient _client;
        private readonly ApiTestFixture _testFixture;

        public ProjectIntegrationTests(ApiTestFixture fixture)
        {
            _autoFixture = new Fixture();
            _testFixture = fixture;
            _client = fixture.Client;
        }

        [Fact]
        public async void When_Post_Project_Returns_200() 
        {
            var projectId = DatabaseModelBuilder.GenerateProjectId();

            var request = new CreateProjectRequest()
            {
                ProjectId = projectId,
                SchoolName = $"{projectId} school",
                ApplicationNumber = _autoFixture.Create<int>().ToString(),
                ApplicationWave = _autoFixture.Create<int>().ToString(),
                CreatedBy = DatabaseModelBuilder.GenerateCreatedBy()
            };

            var postResponse = await _client.PostAsync($"/api/project", request.ConvertToJson());
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var getResponse = await _client.GetAsync($"/api/project/id?projectid={projectId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var wrapper = await getResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectResponse>>();
            var result = wrapper.Data;

            result.Should().BeEquivalentTo(request);
        }
    }
}
