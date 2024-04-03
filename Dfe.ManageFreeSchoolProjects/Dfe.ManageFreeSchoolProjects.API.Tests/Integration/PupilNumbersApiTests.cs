using Azure.Core;
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

            // CapacityWhenFull
            actualPupilNumbers.CapacityWhenFull.Nursery.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.Nursery);
            actualPupilNumbers.CapacityWhenFull.ReceptionToYear6.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.ReceptionToYear6);
            actualPupilNumbers.CapacityWhenFull.Year7ToYear11.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.Year7ToYear11);
            actualPupilNumbers.CapacityWhenFull.Year12ToYear14.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.Year12ToYear14);
            actualPupilNumbers.CapacityWhenFull.SpecialistEducationNeeds.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.SpecialistEducationNeeds);
            actualPupilNumbers.CapacityWhenFull.AlternativeProvision.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.AlternativeProvision);

            var capacityTotals = updatePupilNumbersRequest.CapacityWhenFull.Nursery +
                         updatePupilNumbersRequest.CapacityWhenFull.ReceptionToYear6 +
                         updatePupilNumbersRequest.CapacityWhenFull.Year7ToYear11 +
                         updatePupilNumbersRequest.CapacityWhenFull.Year12ToYear14;

            actualPupilNumbers.CapacityWhenFull.Total.Should().Be(capacityTotals);

            // Pre 16 PAN
            //actualPupilNumbers.Pre16PublishedAdmissionNumber.Nursery.Should().Be(updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.Nursery);
            //actualPupilNumbers.Pre16PublishedAdmissionNumber.ReceptionToYear6.Should().Be(updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.ReceptionToYear6);
            actualPupilNumbers.Pre16PublishedAdmissionNumber.Year7.Should().Be(updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.Year7);
            actualPupilNumbers.Pre16PublishedAdmissionNumber.Year10.Should().Be(updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.Year10);
            actualPupilNumbers.Pre16PublishedAdmissionNumber.OtherPre16.Should().Be(updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.OtherPre16);

            var pre16PanTotals = updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.Year7 +
                updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.Year10 +
                updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.OtherPre16;

            actualPupilNumbers.Pre16PublishedAdmissionNumber.Total.Should().Be(pre16PanTotals);

            //// Post 16 PAN
            actualPupilNumbers.Post16PublishedAdmissionNumber.Year12.Should().Be(updatePupilNumbersRequest.Post16PublishedAdmissionNumber.Year12);
            actualPupilNumbers.Post16PublishedAdmissionNumber.OtherPost16.Should().Be(updatePupilNumbersRequest.Post16PublishedAdmissionNumber.OtherPost16);

            var post16PanTotals = updatePupilNumbersRequest.Post16PublishedAdmissionNumber.Year12 + updatePupilNumbersRequest.Post16PublishedAdmissionNumber.OtherPost16;

            actualPupilNumbers.Post16PublishedAdmissionNumber.Total.Should().Be(post16PanTotals);
        }
    }
}
