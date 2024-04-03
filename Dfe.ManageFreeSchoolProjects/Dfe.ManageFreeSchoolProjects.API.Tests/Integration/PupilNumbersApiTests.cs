using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Net;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class PupilNumbersApiTests : ApiTestsBase
    {
        public PupilNumbersApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task When_PupilNumbersAreAllSet_Returns_200()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var updatePupilNumbersRequest = _autoFixture.Create<UpdatePupilNumbersRequest>();
            var updatePupilNumbersResponse = await _client.PatchAsync($"/api/v1/client/projects/{projectId}/pupil-numbers", updatePupilNumbersRequest.ConvertToJson());
            updatePupilNumbersResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getPupilNumbersResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/pupil-numbers");
            getPupilNumbersResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await getPupilNumbersResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetPupilNumbersResponse>>();

            var actualPupilNumbers = content.Data;

            // Do some assertions
            actualPupilNumbers.CapacityWhenFull.Should().BeEquivalentTo(updatePupilNumbersRequest.CapacityWhenFull);
        }
    }
}
