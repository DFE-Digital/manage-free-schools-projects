using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class FundingAgreementSubmissionApiTests : ApiTestsBase
    {
        public FundingAgreementSubmissionApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_FundingAgreementSubmission_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                FundingAgreementSubmission = new FundingAgreementSubmissionTask()
                {
                    DraftedFundingAgreementSubmission = true,
                }
            };

            var projectResponse =
                await _client.UpdateProjectTask(projectId, request, TaskName.FundingAgreementSubmission.ToString());

            projectResponse.FundingAgreementSubmission.DraftedFundingAgreementSubmission.Should()
                .Be(request.FundingAgreementSubmission.DraftedFundingAgreementSubmission);
            projectResponse.FundingAgreementSubmission.RegionalDirectorSignedOffFundingAgreementSubmission.Should()
                .Be(request.FundingAgreementSubmission.RegionalDirectorSignedOffFundingAgreementSubmission);
            projectResponse.FundingAgreementSubmission.RegionalDirectorSignedOffFundingAgreementSubmission.Should()
                .Be(request.FundingAgreementSubmission.RegionalDirectorSignedOffFundingAgreementSubmission);
            projectResponse.FundingAgreementSubmission.SavedFundingAgreementSubmissionInWorkplacesFolder.Should()
                .Be(request.FundingAgreementSubmission.SavedFundingAgreementSubmissionInWorkplacesFolder);

        }

        [Fact]
        public async Task Patch_ExistingFundingArrangement_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var FundingAgreementSubmissionTask = DatabaseModelBuilder.BuildFundingAgreementSubmissionTask(project.Rid);
            context.Milestones.Add(FundingAgreementSubmissionTask);

            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                FundingAgreementSubmission = new FundingAgreementSubmissionTask()
                {
                    DraftedFundingAgreementSubmission = true,
                    RegionalDirectorSignedOffFundingAgreementSubmission = true,
                    SavedFundingAgreementSubmissionInWorkplacesFolder = true
                }
            };

            var projectResponse =
                await _client.UpdateProjectTask(projectId, request, TaskName.FundingAgreementSubmission.ToString());

            projectResponse.FundingAgreementSubmission.DraftedFundingAgreementSubmission.Should()
                .Be(request.FundingAgreementSubmission.DraftedFundingAgreementSubmission);
            projectResponse.FundingAgreementSubmission.RegionalDirectorSignedOffFundingAgreementSubmission.Should()
                .Be(request.FundingAgreementSubmission.RegionalDirectorSignedOffFundingAgreementSubmission);
            projectResponse.FundingAgreementSubmission.RegionalDirectorSignedOffFundingAgreementSubmission.Should()
                .Be(request.FundingAgreementSubmission.RegionalDirectorSignedOffFundingAgreementSubmission);
            projectResponse.FundingAgreementSubmission.SavedFundingAgreementSubmissionInWorkplacesFolder.Should()
                .Be(request.FundingAgreementSubmission.SavedFundingAgreementSubmissionInWorkplacesFolder);
        }
    }
}
