using Azure.Core;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;
using Dfe.ManageFreeSchoolProjects.API.UseCases.ProjectOverview;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ProjectStatusApiTests : ApiTestsBase
    {
        public ProjectStatusApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task When_Post_ProjectDoesNotExist_Returns_404()
        {
            var projectId = Guid.NewGuid().ToString();
            var updateStatusRequest = _autoFixture.Create<UpdateProjectStatusRequest>();

            var updateStatusResponse = await _client.PostAsync($"/api/v1/client/updateprojectstatus?projectId={projectId}", updateStatusRequest.ConvertToJson());
            updateStatusResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task When_Post_StatusValid_Returns_200()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;
            var updateStatusRequest = new UpdateProjectStatusRequest()
            {
                ProjectStatus = ProjectStatus.Open,
                ProjectCancelledReason = ProjectCancelledReason.NotSet,
                ProjectWithdrawnReason = ProjectWithdrawnReason.NotSet,
                CancelledDate = null,
                ClosedDate = null,
                WithdrawnDate = null,
                ProjectCancelledDueToNationalReviewOfPipelineProjects = null,
                ProjectWithdrawnDueToNationalReviewOfPipelineProjects = null,
                CommentaryForCancellation = null,
                CommentaryForWithdrawal = null

            };


            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var updateStatusResponse = await _client.PostAsync($"/api/v1/client/updateprojectstatus?projectId={projectId}", updateStatusRequest.ConvertToJson());
            updateStatusResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var overviewResponse = await _client.GetAsync($"/api/v1/client/projects/{project.ProjectStatusProjectId}/overview");
            overviewResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await overviewResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectOverviewResponse>>();

            // Project status
            var projectStatus = result.Data.ProjectStatus;
            projectStatus.ProjectStatus.Should().Be(ProjectStatus.Open);
            projectStatus.ProjectCancelledReason.Should().Be(ProjectCancelledReason.NotSet);
            projectStatus.ProjectWithdrawnReason.Should().Be(ProjectWithdrawnReason.NotSet);
            projectStatus.ProjectCancelledDate.Should().BeNull();
            projectStatus.ProjectClosedDate.Should().BeNull();
            projectStatus.ProjectWithdrawnDate.Should().BeNull();
            projectStatus.ProjectCancelledDueToNationalReviewOfPipelineProjects.Should().BeNull();
            projectStatus.ProjectWithdrawnDueToNationalReviewOfPipelineProjects.Should().BeNull();
            projectStatus.CommentaryForCancellation.Should().BeNull();
            projectStatus.CommentaryForWithdrawal.Should().BeNull();


        }

        [Fact]
        public async Task When_Post_StatusCancelledValid_Returns_200()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            var cancelledDate = DateTime.UtcNow.Date;
            var commentaryForCancellation = "Cancelled due to issues with planning";
            var updateStatusRequest = new UpdateProjectStatusRequest()
            {
                ProjectStatus = ProjectStatus.Cancelled,
                ProjectCancelledReason = ProjectCancelledReason.Planning,
                ProjectWithdrawnReason = ProjectWithdrawnReason.NotSet,
                CancelledDate = cancelledDate,
                ClosedDate = null,
                WithdrawnDate = null,
                ProjectCancelledDueToNationalReviewOfPipelineProjects = YesNo.No,
                ProjectWithdrawnDueToNationalReviewOfPipelineProjects = null,
                CommentaryForCancellation = commentaryForCancellation,
                CommentaryForWithdrawal = null

            };


            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var updateStatusResponse = await _client.PostAsync($"/api/v1/client/updateprojectstatus?projectId={projectId}", updateStatusRequest.ConvertToJson());
            updateStatusResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var overviewResponse = await _client.GetAsync($"/api/v1/client/projects/{project.ProjectStatusProjectId}/overview");
            overviewResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await overviewResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectOverviewResponse>>();

            // Project status
            var projectStatus = result.Data.ProjectStatus;
            projectStatus.ProjectStatus.Should().Be(ProjectStatus.Cancelled);
            projectStatus.ProjectCancelledReason.Should().Be(ProjectCancelledReason.Planning);
            projectStatus.ProjectWithdrawnReason.Should().Be(ProjectWithdrawnReason.NotSet);
            projectStatus.ProjectCancelledDate.Should().Be(cancelledDate);
            projectStatus.ProjectClosedDate.Should().BeNull();
            projectStatus.ProjectWithdrawnDate.Should().BeNull();
            projectStatus.ProjectCancelledDueToNationalReviewOfPipelineProjects.Should().Be(YesNo.No);
            projectStatus.ProjectWithdrawnDueToNationalReviewOfPipelineProjects.Should().BeNull();
            projectStatus.CommentaryForCancellation.Should().Be(commentaryForCancellation);
            projectStatus.CommentaryForWithdrawal.Should().BeNull();


        }

    }
}
