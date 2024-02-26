using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class DraftGovernancePlanTaskApiTests : ApiTestsBase
    {
        public DraftGovernancePlanTaskApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_New_DraftGovernancePlan_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            await context.SaveChangesAsync();

            var dateReceived = DateTime.Now.Date.AddDays(10);

            var request = new UpdateProjectByTaskRequest()
            {
                DraftGovernancePlan = new DraftGovernancePlanTask()
                {
                    SavedDocumentsInWorkplacesFolder = false,
                    PlanAndTemplateSharedWithEsfa = false,
                    PlanAndTemplateSharedWithExpert = false,
                    PlanAssessedUsingTemplate = false,
                    PlanFedBackToTrust = false,
                    PlanReceivedFromTrust = false,
                    DateReceived = dateReceived,
                    CommentsOnDecisionToApprove = "Comments on approval",
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.DraftGovernancePlan.ToString());

            AssertDraftGovernancePlan(projectResponse.DraftGovernancePlan, request.DraftGovernancePlan);
        }

        [Fact]
        public async Task Patch_Existing_DraftGovernancePlan_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            await context.SaveChangesAsync();

            var dateReceived = DateTime.Now.Date.AddDays(10);

            var createDraftGovernanceRequest = new UpdateProjectByTaskRequest()
            {
                DraftGovernancePlan = new DraftGovernancePlanTask()
                {
                    SavedDocumentsInWorkplacesFolder = false,
                    PlanAndTemplateSharedWithEsfa = false,
                    PlanAndTemplateSharedWithExpert = false,
                    PlanAssessedUsingTemplate = false,
                    PlanFedBackToTrust = false,
                    PlanReceivedFromTrust = false,
                    DateReceived = dateReceived,
                    CommentsOnDecisionToApprove = "Comments on approval",
                }
            };

            await _client.UpdateProjectTask(projectId, createDraftGovernanceRequest, TaskName.DraftGovernancePlan.ToString());

            var updateDraftGovernanceRequest = new UpdateProjectByTaskRequest()
            {
                DraftGovernancePlan = new DraftGovernancePlanTask()
                {
                    SavedDocumentsInWorkplacesFolder = true,
                    PlanAndTemplateSharedWithEsfa = true,
                    PlanAndTemplateSharedWithExpert = true,
                    PlanAssessedUsingTemplate = true,
                    PlanFedBackToTrust = true,
                    PlanReceivedFromTrust = true,
                    DateReceived = dateReceived.AddDays(5),
                    CommentsOnDecisionToApprove = "Updated comments on approval",
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, updateDraftGovernanceRequest, TaskName.DraftGovernancePlan.ToString());

            AssertDraftGovernancePlan(projectResponse.DraftGovernancePlan, updateDraftGovernanceRequest.DraftGovernancePlan);
        }

        private static void AssertDraftGovernancePlan(DraftGovernancePlanTask actual, DraftGovernancePlanTask expected)
        {
            actual.DateReceived.Should().Be(expected.DateReceived);
            actual.PlanReceivedFromTrust.Should().Be(expected.PlanReceivedFromTrust);
            actual.PlanFedBackToTrust.Should().Be(expected.PlanFedBackToTrust);
            actual.PlanAssessedUsingTemplate.Should().Be(expected.PlanAssessedUsingTemplate);
            actual.PlanAndTemplateSharedWithExpert.Should().Be(expected.PlanAndTemplateSharedWithExpert);
            actual.PlanAndTemplateSharedWithEsfa.Should().Be(expected.PlanAndTemplateSharedWithEsfa);
            actual.SavedDocumentsInWorkplacesFolder.Should().Be(expected.SavedDocumentsInWorkplacesFolder);
            actual.CommentsOnDecisionToApprove.Should().Be(expected.CommentsOnDecisionToApprove);
        }
    }
}
