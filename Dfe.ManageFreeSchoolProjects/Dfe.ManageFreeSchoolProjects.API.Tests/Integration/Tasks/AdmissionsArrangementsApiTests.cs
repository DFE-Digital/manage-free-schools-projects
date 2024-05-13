using System;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class AdmissionsArrangementsApiTests : ApiTestsBase
    {
        public AdmissionsArrangementsApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_NewAdmissionsArrangements_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                AdmissionsArrangements = new AdmissionsArrangementsTask()
                {
                    ExpectedDateThatTrustWillConfirmArrangements = new DateTime().AddDays(11),
                    TrustConfirmedAdmissionsArrangementsPolicies = true,
                    TrustConfirmedAdmissionsArrangementsTemplate = true,
                    ActualDateThatTrustConfirmedArrangements = new DateTime().AddDays(10),
                    SavedToWorkplaces = true
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.AdmissionsArrangements.ToString());

            projectResponse.AdmissionsArrangements.ExpectedDateThatTrustWillConfirmArrangements.Should()
                .Be(request.AdmissionsArrangements.ExpectedDateThatTrustWillConfirmArrangements);
            projectResponse.AdmissionsArrangements.TrustConfirmedAdmissionsArrangementsPolicies.Should()
                .Be(request.AdmissionsArrangements.TrustConfirmedAdmissionsArrangementsPolicies);
            projectResponse.AdmissionsArrangements.TrustConfirmedAdmissionsArrangementsTemplate.Should()
                .Be(request.AdmissionsArrangements.TrustConfirmedAdmissionsArrangementsTemplate);
            projectResponse.AdmissionsArrangements.ActualDateThatTrustConfirmedArrangements.Should()
                .Be(request.AdmissionsArrangements.ActualDateThatTrustConfirmedArrangements);
            projectResponse.AdmissionsArrangements.SavedToWorkplaces.Should()
                .Be(request.AdmissionsArrangements.SavedToWorkplaces);
        }

        [Fact]
        public async Task Patch_ExistingAdmissionsArragements_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            
            var admissionsArrangementsTask = DatabaseModelBuilder.BuildAdmissionsArrangementsTask(project.Rid);
            context.Milestones.Add(admissionsArrangementsTask);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                AdmissionsArrangements = new AdmissionsArrangementsTask()
                {
                    ExpectedDateThatTrustWillConfirmArrangements = new DateTime().AddDays(8),
                    TrustConfirmedAdmissionsArrangementsPolicies = false,
                    TrustConfirmedAdmissionsArrangementsTemplate = false,
                    ActualDateThatTrustConfirmedArrangements = new DateTime().AddDays(9),
                    SavedToWorkplaces = false
                }
            };

            await _client.UpdateProjectTask(projectId, request, TaskName.AdmissionsArrangements.ToString());

            var updateRequest = new UpdateProjectByTaskRequest()
            {
                AdmissionsArrangements = new()
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.AdmissionsArrangements.ToString());
            
            projectResponse.AdmissionsArrangements.ExpectedDateThatTrustWillConfirmArrangements.Should()
                .Be(request.AdmissionsArrangements.ExpectedDateThatTrustWillConfirmArrangements);
            projectResponse.AdmissionsArrangements.TrustConfirmedAdmissionsArrangementsPolicies.Should()
                .Be(request.AdmissionsArrangements.TrustConfirmedAdmissionsArrangementsPolicies);
            projectResponse.AdmissionsArrangements.TrustConfirmedAdmissionsArrangementsTemplate.Should()
                .Be(request.AdmissionsArrangements.TrustConfirmedAdmissionsArrangementsTemplate);
            projectResponse.AdmissionsArrangements.ActualDateThatTrustConfirmedArrangements.Should()
                .Be(request.AdmissionsArrangements.ActualDateThatTrustConfirmedArrangements);
            projectResponse.AdmissionsArrangements.SavedToWorkplaces.Should()
                .Be(request.AdmissionsArrangements.SavedToWorkplaces);
        }

    }
}
