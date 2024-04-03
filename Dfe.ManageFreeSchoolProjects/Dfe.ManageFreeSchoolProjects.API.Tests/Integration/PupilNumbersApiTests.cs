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
            actualPupilNumbers.Pre16PublishedAdmissionNumber.ReceptionToYear6.Should().Be(updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.ReceptionToYear6);
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

            // Pre 16 Capacity Buildup
            actualPupilNumbers.Pre16CapacityBuildup.Nursery.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Nursery);
            actualPupilNumbers.Pre16CapacityBuildup.Reception.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Reception);
            actualPupilNumbers.Pre16CapacityBuildup.Year1.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year1);
            actualPupilNumbers.Pre16CapacityBuildup.Year2.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year2);
            actualPupilNumbers.Pre16CapacityBuildup.Year3.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year3);
            actualPupilNumbers.Pre16CapacityBuildup.Year4.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year4);
            actualPupilNumbers.Pre16CapacityBuildup.Year5.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year5);
            actualPupilNumbers.Pre16CapacityBuildup.Year6.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year6);

            actualPupilNumbers.Pre16CapacityBuildup.Total.FirstYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.FirstYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.SecondYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.SecondYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.ThirdYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.ThirdYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.FourthYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.FourthYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.FifthYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.FifthYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.SixthYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.SixthYear)));

            // Post 16 capacity buildup
            actualPupilNumbers.Post16CapacityBuildup.Year7.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year7);
            actualPupilNumbers.Post16CapacityBuildup.Year8.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year8);
            actualPupilNumbers.Post16CapacityBuildup.Year9.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year9);
            actualPupilNumbers.Post16CapacityBuildup.Year10.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year10);
            actualPupilNumbers.Post16CapacityBuildup.Year11.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year11);
            actualPupilNumbers.Post16CapacityBuildup.Year12.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year12);
            actualPupilNumbers.Post16CapacityBuildup.Year13.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year13);
            actualPupilNumbers.Post16CapacityBuildup.Year14.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year14);

        }

        private static int CalculatePre16BuildupTotal(UpdatePupilNumbersRequest request, string property)
        {
            // Helper to calculate the total of a property across all years
            // Not using this in production, but it reduces the amount of code we have to write for the test
            var nursery = request.Pre16CapacityBuildup.Nursery.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Nursery);
            var reception = request.Pre16CapacityBuildup.Reception.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Reception);
            var year1 = request.Pre16CapacityBuildup.Year1.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year1);
            var year2 = request.Pre16CapacityBuildup.Year2.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year2);
            var year3 = request.Pre16CapacityBuildup.Year3.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year3);
            var year4 = request.Pre16CapacityBuildup.Year4.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year4);
            var year5 = request.Pre16CapacityBuildup.Year5.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year5);
            var year6 = request.Pre16CapacityBuildup.Year6.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year6);

            return (int)nursery + (int)reception + (int)year1 + (int)year2 + (int)year3 + (int)year4 + (int)year5 + (int)year6;
        }
    }
}
