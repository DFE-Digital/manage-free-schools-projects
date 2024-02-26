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

            var forecastDate = DateTime.Now.Date.AddDays(5);
            var actualDate = DateTime.Now.Date.AddDays(10);

            var request = new UpdateProjectByTaskRequest()
            {
                DraftGovernancePlan = new DraftGovernancePlanTask()
                {
                    ForecastDate = forecastDate,
                    ActualDate = actualDate,
                    CommentsOnDecisionToApprove = "Comments on approval",
                    SharepointLink = "https://sharepoint.com"
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

            var forecastDate = DateTime.Now.Date.AddDays(5);
            var actualDate = DateTime.Now.Date.AddDays(10);

            var createDraftGovernanceRequest = new UpdateProjectByTaskRequest()
            {
                DraftGovernancePlan = new DraftGovernancePlanTask()
                {
                    ForecastDate = forecastDate,
                    ActualDate = actualDate,
                    CommentsOnDecisionToApprove = "Comments on approval",
                    SharepointLink = "https://sharepoint.com"
                }
            };

            await _client.UpdateProjectTask(projectId, createDraftGovernanceRequest, TaskName.DraftGovernancePlan.ToString());

            var updateDraftGovernanceRequest = new UpdateProjectByTaskRequest()
            {
                DraftGovernancePlan = new DraftGovernancePlanTask()
                {
                    ForecastDate = forecastDate.AddDays(5),
                    ActualDate = actualDate.AddDays(5),
                    CommentsOnDecisionToApprove = "Updated comments on approval",
                    SharepointLink = "https://sharepoint.com/updated"
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, updateDraftGovernanceRequest, TaskName.DraftGovernancePlan.ToString());

            AssertDraftGovernancePlan(projectResponse.DraftGovernancePlan, updateDraftGovernanceRequest.DraftGovernancePlan);
        }

        private static void AssertDraftGovernancePlan(DraftGovernancePlanTask actual, DraftGovernancePlanTask expected)
        {
            actual.ForecastDate.Should().Be(expected.ForecastDate);
            actual.ActualDate.Should().Be(expected.ActualDate);
            actual.CommentsOnDecisionToApprove.Should().Be(expected.CommentsOnDecisionToApprove);
            actual.SharepointLink.Should().Be(expected.SharepointLink);
        }
    }
}
