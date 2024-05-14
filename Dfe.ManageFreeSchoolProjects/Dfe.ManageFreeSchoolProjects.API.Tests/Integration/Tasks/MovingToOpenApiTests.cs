using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class MoveToOpeningApiTests: ApiTestsBase
    {
        public MoveToOpeningApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_NewMoveToOpening_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var dateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
                MovingToOpen = new MovingToOpenTask()
                {
                  ProjectBriefToEducationEstates = true,
                  ProjectBriefToNewDeliveryOfficer = true,
                  SentEmailsToSchoolsPrinciple = true,
                  SentEmailsToRelevantContacts = true,
                  SavedToWorkplacesFolderAnnexB = true,
                  SavedToWorkplacesFolderAnnexE = true,
                  SavedToWorkplacesFolderProjectBrief = true,
                  ActualOpeningDate = dateNineDaysInFuture
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.MovingToOpen.ToString());

            projectResponse.MovingToOpen.ProjectBriefToEducationEstates.Should()
                .Be(request.MovingToOpen.ProjectBriefToEducationEstates);
            projectResponse.MovingToOpen.ProjectBriefToNewDeliveryOfficer.Should()
                .Be(request.MovingToOpen.ProjectBriefToNewDeliveryOfficer);
            projectResponse.MovingToOpen.SentEmailsToSchoolsPrinciple.Should()
                .Be(request.MovingToOpen.SentEmailsToSchoolsPrinciple);
            projectResponse.MovingToOpen.SentEmailsToRelevantContacts.Should()
                .Be(request.MovingToOpen.SentEmailsToRelevantContacts);
            projectResponse.MovingToOpen.SavedToWorkplacesFolderAnnexB.Should()
                .Be(request.MovingToOpen.SavedToWorkplacesFolderAnnexB);
            projectResponse.MovingToOpen.SavedToWorkplacesFolderAnnexE.Should()
                .Be(request.MovingToOpen.SavedToWorkplacesFolderAnnexE);
            projectResponse.MovingToOpen.SavedToWorkplacesFolderProjectBrief.Should()
                .Be(request.MovingToOpen.SavedToWorkplacesFolderProjectBrief);
            projectResponse.MovingToOpen.ActualOpeningDate.Should()
                .Be(request.MovingToOpen.ActualOpeningDate);

        }

        [Fact]
        public async Task Patch_ExistingMovingToOpen_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var movingToOpenTask = DatabaseModelBuilder.MovingToOpenTask(project.Rid);
            context.Milestones.Add(movingToOpenTask);

            await context.SaveChangesAsync();

            var dateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
                MovingToOpen = new MovingToOpenTask()
                {
                    ProjectBriefToEducationEstates = true,
                    ProjectBriefToNewDeliveryOfficer = true,
                    SentEmailsToSchoolsPrinciple = true,
                    SentEmailsToRelevantContacts = true,
                    SavedToWorkplacesFolderAnnexB = true,
                    SavedToWorkplacesFolderAnnexE = true,
                    SavedToWorkplacesFolderProjectBrief = true,
                    ActualOpeningDate = dateNineDaysInFuture
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.MovingToOpen.ToString());

            projectResponse.MovingToOpen.ProjectBriefToEducationEstates.Should()
                .Be(request.MovingToOpen.ProjectBriefToEducationEstates);
            projectResponse.MovingToOpen.ProjectBriefToNewDeliveryOfficer.Should()
                .Be(request.MovingToOpen.ProjectBriefToNewDeliveryOfficer);
            projectResponse.MovingToOpen.SentEmailsToSchoolsPrinciple.Should()
                .Be(request.MovingToOpen.SentEmailsToSchoolsPrinciple);
            projectResponse.MovingToOpen.SentEmailsToRelevantContacts.Should()
                .Be(request.MovingToOpen.SentEmailsToRelevantContacts);
            projectResponse.MovingToOpen.SavedToWorkplacesFolderAnnexB.Should()
                .Be(request.MovingToOpen.SavedToWorkplacesFolderAnnexB);
            projectResponse.MovingToOpen.SavedToWorkplacesFolderAnnexE.Should()
                .Be(request.MovingToOpen.SavedToWorkplacesFolderAnnexE);
            projectResponse.MovingToOpen.SavedToWorkplacesFolderProjectBrief.Should()
                .Be(request.MovingToOpen.SavedToWorkplacesFolderProjectBrief);
            projectResponse.MovingToOpen.ActualOpeningDate.Should()
                .Be(request.MovingToOpen.ActualOpeningDate);
        }
    }
}
