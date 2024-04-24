using System;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class CommissionedExternalExpertApiTests : ApiTestsBase
    {
        public CommissionedExternalExpertApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_CommissionedExternalExpert_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                CommissionedExternalExpert = new CommissionedExternalExpertTask()
                {
                    CommissionedExternalExpertVisit = true,
                    ExternalExpertVisitDate = new DateTime().AddDays(10),
                    SavedExternalExpertSpecsToWorkplacesFolder = true
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.CommissionedExternalExpert.ToString());

            projectResponse.CommissionedExternalExpert.CommissionedExternalExpertVisit.Should()
                .Be(request.CommissionedExternalExpert.CommissionedExternalExpertVisit);
            projectResponse.CommissionedExternalExpert.ExternalExpertVisitDate.Should()
                .Be(request.CommissionedExternalExpert.ExternalExpertVisitDate);
            projectResponse.CommissionedExternalExpert.SavedExternalExpertSpecsToWorkplacesFolder.Should()
                .Be(request.CommissionedExternalExpert.SavedExternalExpertSpecsToWorkplacesFolder);
        }

        [Fact]
        public async Task Patch_ExistingCommissionedExternalExpert_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            
            var admissionsArrangementsTask = DatabaseModelBuilder.BuildCommissionedExternalExpertTask(project.Rid);
            context.Milestones.Add(admissionsArrangementsTask);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                CommissionedExternalExpert = new CommissionedExternalExpertTask()
                {
                    CommissionedExternalExpertVisit = true,
                    ExternalExpertVisitDate = new DateTime().AddDays(10),
                    SavedExternalExpertSpecsToWorkplacesFolder = true
                }
            };

            await _client.UpdateProjectTask(projectId, request, TaskName.CommissionedExternalExpert.ToString());

            var updateRequest = new UpdateProjectByTaskRequest()
            {
                AdmissionsArrangements = new()
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.CommissionedExternalExpert.ToString());

            projectResponse.CommissionedExternalExpert.CommissionedExternalExpertVisit.Should()
                .Be(request.CommissionedExternalExpert.CommissionedExternalExpertVisit);
            projectResponse.CommissionedExternalExpert.ExternalExpertVisitDate.Should()
                .Be(request.CommissionedExternalExpert.ExternalExpertVisitDate);
            projectResponse.CommissionedExternalExpert.SavedExternalExpertSpecsToWorkplacesFolder.Should()
                .Be(request.CommissionedExternalExpert.SavedExternalExpertSpecsToWorkplacesFolder);
        }

    }
}
