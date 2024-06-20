using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class FundingAgreementApiTests : ApiTestsBase
    {
        public FundingAgreementApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_FundingAgreement_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var dateNineDaysInFuture = new DateTime().AddDays(9);
            var dateTenDaysInFuture = new DateTime().AddDays(10);
            var dateElevenDaysInFuture = new DateTime().AddDays(11);

            var request = new UpdateProjectByTaskRequest()
            {
                FundingAgreement = new FundingAgreementTask()
                {
                    TailoredTheFundingAgreement = true,
                    SharedFAWithTheTrust = true,
                    TrustHasSignedTheFA = YesNo.Yes,
                    DateTheTrustSignedFA = dateNineDaysInFuture,
                    ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf = dateTenDaysInFuture,
                    DateFAWasSigned = dateElevenDaysInFuture,
                    SavedFADocumentsInWorkplacesFolder = true
                }
            };

            var projectResponse =
                await _client.UpdateProjectTask(projectId, request, TaskName.FundingAgreement.ToString());

            projectResponse.FundingAgreement.TrustHasSignedTheFA.Should()
                .Be(request.FundingAgreement.TrustHasSignedTheFA);
            projectResponse.FundingAgreement.DateTheTrustSignedFA.Should()
                .Be(request.FundingAgreement.DateTheTrustSignedFA);
            projectResponse.FundingAgreement.TailoredTheFundingAgreement.Should()
                .Be(request.FundingAgreement.TailoredTheFundingAgreement);
            projectResponse.FundingAgreement.SharedFAWithTheTrust.Should()
                .Be(request.FundingAgreement.SharedFAWithTheTrust);
            projectResponse.FundingAgreement.ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf.Should()
                .Be(request.FundingAgreement.ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf);
            projectResponse.FundingAgreement.DateFAWasSigned.Should()
                .Be(request.FundingAgreement.DateFAWasSigned);
            projectResponse.FundingAgreement.SavedFADocumentsInWorkplacesFolder.Should()
                .Be(request.FundingAgreement.SavedFADocumentsInWorkplacesFolder);
        }

        [Fact]
        public async Task Patch_ExistingFundingArrangement_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var fundingAgreementTask = DatabaseModelBuilder.BuildFundingAgreementTask(project.Rid);
            context.Milestones.Add(fundingAgreementTask);

            await context.SaveChangesAsync();

            var dateNineDaysInFuture = new DateTime().AddDays(9);
            var dateTenDaysInFuture = new DateTime().AddDays(10);

            var request = new UpdateProjectByTaskRequest()
            {
                FundingAgreement = new FundingAgreementTask()
                {
                    TailoredTheFundingAgreement = true,
                    SharedFAWithTheTrust = false,
                    TrustHasSignedTheFA = YesNo.Yes,
                    DateTheTrustSignedFA = dateNineDaysInFuture,
                    ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf = dateTenDaysInFuture,
                    DateFAWasSigned = dateTenDaysInFuture,
                    SavedFADocumentsInWorkplacesFolder = false
                }
            };

            var projectResponse =
                await _client.UpdateProjectTask(projectId, request, TaskName.FundingAgreement.ToString());

            projectResponse.FundingAgreement.TrustHasSignedTheFA.Should()
                .Be(request.FundingAgreement.TrustHasSignedTheFA);
            projectResponse.FundingAgreement.DateTheTrustSignedFA.Should()
                .Be(request.FundingAgreement.DateTheTrustSignedFA);
            projectResponse.FundingAgreement.TailoredTheFundingAgreement.Should()
                .Be(request.FundingAgreement.TailoredTheFundingAgreement);
            projectResponse.FundingAgreement.SharedFAWithTheTrust.Should()
                .Be(request.FundingAgreement.SharedFAWithTheTrust);
            projectResponse.FundingAgreement.ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf.Should()
                .Be(request.FundingAgreement.ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf);
            projectResponse.FundingAgreement.DateFAWasSigned.Should()
                .Be(request.FundingAgreement.DateFAWasSigned);
            projectResponse.FundingAgreement.SavedFADocumentsInWorkplacesFolder.Should()
                .Be(request.FundingAgreement.SavedFADocumentsInWorkplacesFolder);
        }
    }
}
