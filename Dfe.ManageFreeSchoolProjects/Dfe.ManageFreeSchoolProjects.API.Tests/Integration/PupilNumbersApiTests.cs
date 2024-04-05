using Azure.Core;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using System.Net;
using System.Net.Http.Json;
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
        public async Task When_PupilNumbers_CapacityWhenFull_Returns_200()
        {
            var project = await CreateProject();
            var projectId = project.ProjectStatusProjectId;

            var updatePupilNumbersRequest = new UpdatePupilNumbersRequest()
            {
                CapacityWhenFull = new()
                {
                    Nursery = 10,
                    ReceptionToYear6 = 20,
                    Year7ToYear11 = 30,
                    Year12ToYear14 = 40,
                    SpecialistEducationNeeds = 50,
                    AlternativeProvision = 60
                }
            };
            var content = await UpdatePupilNumbers(projectId, updatePupilNumbersRequest);

            var actualPupilNumbers = content.Data;

            // CapacityWhenFull
            actualPupilNumbers.CapacityWhenFull.Nursery.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.Nursery);
            actualPupilNumbers.CapacityWhenFull.ReceptionToYear6.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.ReceptionToYear6);
            actualPupilNumbers.CapacityWhenFull.Year7ToYear11.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.Year7ToYear11);
            actualPupilNumbers.CapacityWhenFull.Year12ToYear14.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.Year12ToYear14);
            actualPupilNumbers.CapacityWhenFull.SpecialistEducationNeeds.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.SpecialistEducationNeeds);
            actualPupilNumbers.CapacityWhenFull.AlternativeProvision.Should().Be(updatePupilNumbersRequest.CapacityWhenFull.AlternativeProvision);

            actualPupilNumbers.CapacityWhenFull.Total.Should().Be(210);
        }

        [Fact]
        public async void When_PupilNumbers_Pre16PublishedAdmissionsNumber_Returns_200()
        {
            var project = await CreateProject();
            var projectId = project.ProjectStatusProjectId;

            var updatePupilNumbersRequest = new UpdatePupilNumbersRequest()
            {
                Pre16PublishedAdmissionNumber = new()
                {
                    ReceptionToYear6 = 10,
                    Year7 = 20,
                    Year10 = 30,
                    OtherPre16 = 40
                }
            };

            var content = await UpdatePupilNumbers(projectId, updatePupilNumbersRequest);

            var actualPupilNumbers = content.Data;

            actualPupilNumbers.Pre16PublishedAdmissionNumber.ReceptionToYear6.Should().Be(updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.ReceptionToYear6);
            actualPupilNumbers.Pre16PublishedAdmissionNumber.Year7.Should().Be(updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.Year7);
            actualPupilNumbers.Pre16PublishedAdmissionNumber.Year10.Should().Be(updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.Year10);
            actualPupilNumbers.Pre16PublishedAdmissionNumber.OtherPre16.Should().Be(updatePupilNumbersRequest.Pre16PublishedAdmissionNumber.OtherPre16);

            actualPupilNumbers.Pre16PublishedAdmissionNumber.Total.Should().Be(100);
        }

        [Fact]
        public async void When_PupilNumbers_Post16PublishedAdmissionsNumber_Returns_200()
        {
            var project = await CreateProject();
            var projectId = project.ProjectStatusProjectId;

            var updatePupilNumbersRequest = new UpdatePupilNumbersRequest()
            {
                Post16PublishedAdmissionNumber = new()
                {
                    Year12 = 10,
                    OtherPost16 = 20
                }
            };

            var content = await UpdatePupilNumbers(projectId, updatePupilNumbersRequest);

            var actualPupilNumbers = content.Data;

            actualPupilNumbers.Post16PublishedAdmissionNumber.Year12.Should().Be(updatePupilNumbersRequest.Post16PublishedAdmissionNumber.Year12);
            actualPupilNumbers.Post16PublishedAdmissionNumber.OtherPost16.Should().Be(updatePupilNumbersRequest.Post16PublishedAdmissionNumber.OtherPost16);

            actualPupilNumbers.Post16PublishedAdmissionNumber.Total.Should().Be(30);
        }

        [Fact]
        public async void When_PupilNumbers_Pre16CapacityBuildup_Returns_200()
        {
            var project = await CreateProject();
            var projectId = project.ProjectStatusProjectId;

            var updatePupilNumbersRequest = _autoFixture.Create<UpdatePupilNumbersRequest>();
            var content = await UpdatePupilNumbers(projectId, updatePupilNumbersRequest);

            var actualPupilNumbers = content.Data;

            actualPupilNumbers.Pre16CapacityBuildup.Nursery.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Nursery);
            actualPupilNumbers.Pre16CapacityBuildup.Reception.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Reception);
            actualPupilNumbers.Pre16CapacityBuildup.Year1.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year1);
            actualPupilNumbers.Pre16CapacityBuildup.Year2.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year2);
            actualPupilNumbers.Pre16CapacityBuildup.Year3.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year3);
            actualPupilNumbers.Pre16CapacityBuildup.Year4.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year4);
            actualPupilNumbers.Pre16CapacityBuildup.Year5.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year5);
            actualPupilNumbers.Pre16CapacityBuildup.Year6.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year6);
            actualPupilNumbers.Pre16CapacityBuildup.Year7.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year7);
            actualPupilNumbers.Pre16CapacityBuildup.Year8.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year8);
            actualPupilNumbers.Pre16CapacityBuildup.Year9.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year9);
            actualPupilNumbers.Pre16CapacityBuildup.Year10.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year10);
            actualPupilNumbers.Pre16CapacityBuildup.Year11.Should().BeEquivalentTo(updatePupilNumbersRequest.Pre16CapacityBuildup.Year11);

            actualPupilNumbers.Pre16CapacityBuildup.Total.CurrentCapacity.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.CurrentCapacity)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.FirstYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.FirstYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.SecondYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.SecondYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.ThirdYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.ThirdYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.FourthYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.FourthYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.FifthYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.FifthYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.SixthYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.SixthYear)));
            actualPupilNumbers.Pre16CapacityBuildup.Total.SeventhYear.Should().Be(CalculatePre16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.SeventhYear)));
        }

        [Fact]
        public async void When_PupilNumbers_Post16CapacityBuildup_Returns_200()
        {
            var project = await CreateProject();
            var projectId = project.ProjectStatusProjectId;

            var updatePupilNumbersRequest = _autoFixture.Create<UpdatePupilNumbersRequest>();
            var content = await UpdatePupilNumbers(projectId, updatePupilNumbersRequest);

            var actualPupilNumbers = content.Data;

            actualPupilNumbers.Post16CapacityBuildup.Year12.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year12);
            actualPupilNumbers.Post16CapacityBuildup.Year13.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year13);
            actualPupilNumbers.Post16CapacityBuildup.Year14.Should().BeEquivalentTo(updatePupilNumbersRequest.Post16CapacityBuildup.Year14);

            actualPupilNumbers.Post16CapacityBuildup.Total.CurrentCapacity.Should().Be(CalculatePost16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.CurrentCapacity)));
            actualPupilNumbers.Post16CapacityBuildup.Total.FirstYear.Should().Be(CalculatePost16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.FirstYear)));
            actualPupilNumbers.Post16CapacityBuildup.Total.SecondYear.Should().Be(CalculatePost16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.SecondYear)));
            actualPupilNumbers.Post16CapacityBuildup.Total.ThirdYear.Should().Be(CalculatePost16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.ThirdYear)));
            actualPupilNumbers.Post16CapacityBuildup.Total.FourthYear.Should().Be(CalculatePost16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.FourthYear)));
            actualPupilNumbers.Post16CapacityBuildup.Total.FifthYear.Should().Be(CalculatePost16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.FifthYear)));
            actualPupilNumbers.Post16CapacityBuildup.Total.SixthYear.Should().Be(CalculatePost16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.SixthYear)));
            actualPupilNumbers.Post16CapacityBuildup.Total.SeventhYear.Should().Be(CalculatePost16BuildupTotal(updatePupilNumbersRequest, nameof(CapacityBuildupEntry.SeventhYear)));
        }

        [Fact]
        public async void When_PupilNumbers_RecruitmentAndViability_Returns_200()
        {
            var project = await CreateProject();
            var projectId = project.ProjectStatusProjectId;

            var updatePupilNumbersRequest = new UpdatePupilNumbersRequest()
            {
                RecruitmentAndViability = new RecruitmentAndViability()
                {
                    ReceptionToYear6 = new RecruitmentAndViabilityEntry()
                    {
                        MinimumViableNumber = 10,
                        ApplicationsReceived = 5
                    },
                    Year7ToYear11 = new RecruitmentAndViabilityEntry()
                    {
                        MinimumViableNumber = 20,
                        ApplicationsReceived = 10
                    },
                    Year12ToYear14 = new RecruitmentAndViabilityEntry()
                    {
                        MinimumViableNumber = 30,
                        ApplicationsReceived = 15
                    }
                }
            };

            var content = await UpdatePupilNumbers(projectId, updatePupilNumbersRequest);

            var actualPupilNumbers = content.Data;

            actualPupilNumbers.RecruitmentAndViability.ReceptionToYear6.Should().BeEquivalentTo(updatePupilNumbersRequest.RecruitmentAndViability.ReceptionToYear6);
            actualPupilNumbers.RecruitmentAndViability.Year7ToYear11.Should().BeEquivalentTo(updatePupilNumbersRequest.RecruitmentAndViability.Year7ToYear11);
            actualPupilNumbers.RecruitmentAndViability.Year12ToYear14.Should().BeEquivalentTo(updatePupilNumbersRequest.RecruitmentAndViability.Year12ToYear14);

            actualPupilNumbers.RecruitmentAndViability.Total.MinimumViableNumber.Should().Be(60);
            actualPupilNumbers.RecruitmentAndViability.Total.ApplicationsReceived.Should().Be(30);
        }

        private async Task<Kpi> CreateProject()
        {
            var project = DatabaseModelBuilder.BuildProject();

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            return project;
        }

        private async Task<ApiSingleResponseV2<GetPupilNumbersResponse>> UpdatePupilNumbers(string projectId, UpdatePupilNumbersRequest updatePupilNumbersRequest)
        {
            var updatePupilNumbersResponse = await _client.PatchAsync($"/api/v1/client/projects/{projectId}/pupil-numbers", updatePupilNumbersRequest.ConvertToJson());
            updatePupilNumbersResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getPupilNumbersResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/pupil-numbers");
            getPupilNumbersResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await getPupilNumbersResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetPupilNumbersResponse>>();
            return content;
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
            var year7 = request.Pre16CapacityBuildup.Year7.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year7);
            var year8 = request.Pre16CapacityBuildup.Year8.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year8);
            var year9 = request.Pre16CapacityBuildup.Year9.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year9);
            var year10 = request.Pre16CapacityBuildup.Year10.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year10);
            var year11 = request.Pre16CapacityBuildup.Year11.GetType().GetProperty(property).GetValue(request.Pre16CapacityBuildup.Year11);

            return (int)nursery + 
                (int)reception + 
                (int)year1 + 
                (int)year2 + 
                (int)year3 + 
                (int)year4 + 
                (int)year5 + 
                (int)year6 +
                (int)year7 +
                (int)year8 +
                (int)year9 +
                (int)year10 +
                (int)year11;
        }

        private static int CalculatePost16BuildupTotal(UpdatePupilNumbersRequest request, string property)
        {
            // Helper to calculate the total of a property across all years
            // Not using this in production, but it reduces the amount of code we have to write for the test
            var year12 = request.Post16CapacityBuildup.Year12.GetType().GetProperty(property).GetValue(request.Post16CapacityBuildup.Year12);
            var year13 = request.Post16CapacityBuildup.Year13.GetType().GetProperty(property).GetValue(request.Post16CapacityBuildup.Year13);
            var year14 = request.Post16CapacityBuildup.Year14.GetType().GetProperty(property).GetValue(request.Post16CapacityBuildup.Year14);

            return (int)year12 +
                (int)year13 +
                (int)year14;
        }
    }
}
