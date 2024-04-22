using System;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class PupilNumbersChecksApiTests : ApiTestsBase
    {
        public PupilNumbersChecksApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_PupilNumbersChecks_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                PupilNumbersChecks = new PupilNumbersChecksTask()
                {
                    SchoolReceivedEnoughApplications = true,
                    CapacityDataMatchesGiasRegistration = true,
                    CapacityDataMatchesFundingAgreement = true
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.PupilNumbersChecks.ToString());

            projectResponse.PupilNumbersChecks.SchoolReceivedEnoughApplications.Should()
                .Be(request.PupilNumbersChecks.SchoolReceivedEnoughApplications);
            projectResponse.PupilNumbersChecks.CapacityDataMatchesFundingAgreement.Should()
                .Be(request.PupilNumbersChecks.CapacityDataMatchesFundingAgreement);
            projectResponse.PupilNumbersChecks.CapacityDataMatchesGiasRegistration.Should()
                .Be(request.PupilNumbersChecks.CapacityDataMatchesGiasRegistration);
        }

        [Fact]
        public async Task Patch_Existing_PupilNumbersChecks_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            
            var pupilNumbersChecksTask = DatabaseModelBuilder.PupilNumbersChecksTask(project.Rid);
            context.Milestones.Add(pupilNumbersChecksTask);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                PupilNumbersChecks = new PupilNumbersChecksTask()
                {
                    SchoolReceivedEnoughApplications = true,
                    CapacityDataMatchesGiasRegistration = true,
                    CapacityDataMatchesFundingAgreement = true
                }
            };

            await _client.UpdateProjectTask(projectId, request, TaskName.PupilNumbersChecks.ToString());
            
            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.PupilNumbersChecks.ToString());

            projectResponse.PupilNumbersChecks.SchoolReceivedEnoughApplications.Should()
                .Be(request.PupilNumbersChecks.SchoolReceivedEnoughApplications);
            projectResponse.PupilNumbersChecks.CapacityDataMatchesFundingAgreement.Should()
                .Be(request.PupilNumbersChecks.CapacityDataMatchesFundingAgreement);
            projectResponse.PupilNumbersChecks.CapacityDataMatchesGiasRegistration.Should()
                .Be(request.PupilNumbersChecks.CapacityDataMatchesGiasRegistration);
        }

    }
}
