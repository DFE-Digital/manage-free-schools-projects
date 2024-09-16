using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class GovernancePlanTaskApiTests : ApiTestsBase
    {
        public GovernancePlanTaskApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_New_GovernancePlan_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            await context.SaveChangesAsync();

            var dateReceived = DateTime.Now.Date.AddDays(10);

            var request = new UpdateProjectByTaskRequest()
            {
                GovernancePlan = new GovernancePlanTask()
                {
                    SavedDocumentsInWorkplacesFolder = false,
                    PlanAndAssessmentSharedWithEsfa = false,
                    PlanAndAssessmentSharedWithLocalAuthority = false,
                    PlanAndAssessmentSharedWithExpert = false,
                    PlanAssessedUsingTemplate = false,
                    PlanFedBackToTrust = false,
                    PlanReceivedFromTrust = false,
                    DatePlanReceived = dateReceived,
                    FinalGovernancePlanAgreed = false,
                    Comments = "Comments on approval",

                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.GovernancePlan.ToString());

            AssertGovernancePlan(projectResponse.GovernancePlan, request.GovernancePlan);
        }

        [Fact]
        public async Task Patch_Existing_GovernancePlan_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            await context.SaveChangesAsync();

            var dateReceived = DateTime.Now.Date.AddDays(10);

            var datePlanAgreed = DateTime.Now.Date.AddDays(5);

            var createGovernanceRequest = new UpdateProjectByTaskRequest()
            {
                GovernancePlan = new GovernancePlanTask()
                {
                    SavedDocumentsInWorkplacesFolder = false,
                    PlanAndAssessmentSharedWithEsfa = false,
                    PlanAndAssessmentSharedWithLocalAuthority = false,
                    PlanAndAssessmentSharedWithExpert = false,
                    PlanAssessedUsingTemplate = false,
                    PlanFedBackToTrust = false,
                    PlanReceivedFromTrust = false,
                    DatePlanReceived = dateReceived,
                    FinalGovernancePlanAgreed = false,
                    DatePlanAgreed = datePlanAgreed,
                    Comments = "Comments on approval",
                }
            };

            await _client.UpdateProjectTask(projectId, createGovernanceRequest, TaskName.GovernancePlan.ToString());

            var updateGovernanceRequest = new UpdateProjectByTaskRequest()
            {
                GovernancePlan = new GovernancePlanTask()
                {
                    SavedDocumentsInWorkplacesFolder = true,
                    PlanAndAssessmentSharedWithEsfa = true,
                    PlanAndAssessmentSharedWithLocalAuthority = true,
                    PlanAndAssessmentSharedWithExpert = true,
                    PlanAssessedUsingTemplate = true,
                    PlanFedBackToTrust = true,
                    PlanReceivedFromTrust = true,
                    DatePlanReceived = dateReceived.AddDays(5),
                    FinalGovernancePlanAgreed = true,
                    DatePlanAgreed = datePlanAgreed.AddDays(2),
                    Comments = "Updated comments on approval",
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, updateGovernanceRequest, TaskName.GovernancePlan.ToString());

            AssertGovernancePlan(projectResponse.GovernancePlan, updateGovernanceRequest.GovernancePlan);
        }

        private static void AssertGovernancePlan(GovernancePlanTask actual, GovernancePlanTask expected)
        {
            actual.DatePlanReceived.Should().Be(expected.DatePlanReceived);
            actual.PlanReceivedFromTrust.Should().Be(expected.PlanReceivedFromTrust);
            actual.PlanFedBackToTrust.Should().Be(expected.PlanFedBackToTrust);
            actual.PlanAssessedUsingTemplate.Should().Be(expected.PlanAssessedUsingTemplate);
            actual.PlanAndAssessmentSharedWithExpert.Should().Be(expected.PlanAndAssessmentSharedWithExpert);
            actual.PlanAndAssessmentSharedWithLocalAuthority.Should().Be(expected.PlanAndAssessmentSharedWithLocalAuthority);
            actual.PlanAndAssessmentSharedWithEsfa.Should().Be(expected.PlanAndAssessmentSharedWithEsfa);
            actual.SavedDocumentsInWorkplacesFolder.Should().Be(expected.SavedDocumentsInWorkplacesFolder);
            actual.FinalGovernancePlanAgreed.Should().Be(expected.FinalGovernancePlanAgreed);
            actual.DatePlanAgreed.Should().Be(expected?.DatePlanAgreed);
            actual.Comments.Should().Be(expected.Comments);
        }
    }
}
