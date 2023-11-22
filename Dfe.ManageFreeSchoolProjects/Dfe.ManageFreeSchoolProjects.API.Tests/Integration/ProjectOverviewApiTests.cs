using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ProjectOverviewApiTests : ApiTestsBase
    {
        public ProjectOverviewApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task When_Get_AllFieldsSet_Returns_200()
        {
            using var context = _testFixture.GetContext();

            var trust = DatabaseModelBuilder.BuildTrust();
            context.Trust.Add(trust);
            await context.SaveChangesAsync();
            
            var project = DatabaseModelBuilder.BuildProject();
            project.SchoolDetailsSchoolTypeMainstreamApEtc = "FS - AP";

            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var createProjectRiskRequest = _autoFixture.Create<CreateProjectRiskRequest>();

            var createProjectRiskResponse = await _client.PostAsync($"/api/v1/client/projects/{project.ProjectStatusProjectId}/risk", createProjectRiskRequest.ConvertToJson());
            createProjectRiskResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var overviewResponse = await _client.GetAsync($"/api/v1/client/projects/{project.ProjectStatusProjectId}/overview");
            overviewResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await overviewResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectOverviewResponse>>();

            // Project status
            var projectStatus = result.Data.ProjectStatus;
            projectStatus.CurrentFreeSchoolName.Should().Be(project.ProjectStatusCurrentFreeSchoolName);
            projectStatus.ProjectStatus.Should().Be(project.ProjectStatusProjectStatus);
            projectStatus.FreeSchoolsApplicationNumber.Should().Be(project.ProjectStatusFreeSchoolsApplicationNumber);
            projectStatus.ProjectId.Should().Be(project.ProjectStatusProjectId);
            projectStatus.Urn.Should().Be(project.ProjectStatusUrnWhenGivenOne);
            projectStatus.ApplicationWave.Should().Be(project.ProjectStatusFreeSchoolApplicationWave);
            projectStatus.RealisticYearOfOpening.Should().Be(project.ProjectStatusRealisticYearOfOpening);
            projectStatus.DateOfEntryIntoPreopening.Should().Be(project.ProjectStatusDateOfEntryIntoPreOpening.Value.Date);
            projectStatus.ProvisionalOpeningDateAgreedWithTrust.Should().Be(project.ProjectStatusProvisionalOpeningDateAgreedWithTrust.Value.Date);
            projectStatus.ActualOpeningDate.Should().Be(project.ProjectStatusActualOpeningDate.Value.Date.ToString());
            projectStatus.OpeningAcademicYear.Should().Be(project.ProjectStatusTrustsPreferredYearOfOpening);

            // School details
            var schoolDetails = result.Data.SchoolDetails;
            schoolDetails.LocalAuthority.Should().Be(project.LocalAuthority);
            schoolDetails.Region.Should().Be(project.SchoolDetailsGeographicalRegion);
            schoolDetails.Constituency.Should().Be(project.SchoolDetailsConstituency);
            schoolDetails.ConstituencyMp.Should().Be(project.SchoolDetailsConstituencyMp);
            schoolDetails.NumberOfEntryForms.Should().Be(project.SchoolDetailsNumberOfFormsOfEntry);
            schoolDetails.SchoolType.Should().Be(SchoolType.AlternativeProvision);
            schoolDetails.SchoolPhase.Should().Be(project.SchoolDetailsSchoolPhasePrimarySecondary);
            schoolDetails.AgeRange.Should().Be(project.SchoolDetailsAgeRange);
            schoolDetails.Gender.Should().Be(project.SchoolDetailsGender);
            schoolDetails.Nursery.Should().Be(project.SchoolDetailsNursery);
            schoolDetails.SixthForm.Should().Be(project.SchoolDetailsSixthForm);
            schoolDetails.IndependentConverter.Should().Be(project.SchoolDetailsIndependentConverter);
            schoolDetails.SpecialistResourceProvision.Should().Be(project.SchoolDetailsSpecialistResourceProvision);
            schoolDetails.FaithStatus.Should().Be(project.SchoolDetailsFaithStatus);
            schoolDetails.FaithType.Should().Be(project.SchoolDetailsFaithType);
            schoolDetails.TrustId.Should().Be(project.TrustId);
            schoolDetails.TrustName.Should().Be(project.SchoolDetailsTrustName);
            schoolDetails.TrustType.Should().Be(project.SchoolDetailsTrustType);

            // Risk
            result.Data.Risk.Date.Value.Date.Should().Be(DateTime.Now.Date);
            result.Data.Risk.RiskRating.Should().Be(createProjectRiskRequest.Overall.RiskRating);
            result.Data.Risk.Summary.Should().Be(createProjectRiskRequest.Overall.Summary);
        }

        [Fact]
        public async Task When_Get_MandatoryFieldsSet_Returns_200()
        {
            using var context = _testFixture.GetContext();
            var project = DatabaseModelBuilder.BuildProjectMandatoryFieldsOnly();
            project.ProjectStatusProjectId = DatabaseModelBuilder.CreateProjectId();

            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var overviewResponse = await _client.GetAsync($"/api/v1/client/projects/{project.ProjectStatusProjectId}/overview");
            overviewResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await overviewResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectOverviewResponse>>();

            // Project status
            var projectStatus = result.Data.ProjectStatus;
            projectStatus.CurrentFreeSchoolName.Should().BeNull();
            projectStatus.ProjectStatus.Should().BeNull();
            projectStatus.DateOfEntryIntoPreopening.Should().BeNull();
            projectStatus.ProvisionalOpeningDateAgreedWithTrust.Should().BeNull();
            projectStatus.ActualOpeningDate.Should().BeNull();

            // School details
            var schoolDetails = result.Data.SchoolDetails;
            schoolDetails.LocalAuthority.Should().BeNull();
            schoolDetails.Region.Should().BeNull();

            // Risk
            result.Data.Risk.RiskRating.Should().BeNull();
            result.Data.Risk.Summary.Should().BeNull();
            result.Data.Risk.Date.Should().BeNull();
        }

        [Fact]
        public async Task When_Get_ProjectNotFound_Returns_404()
        {
            var overviewResponse = await _client.GetAsync($"/api/v1/client/project/overview/NotExist");
            overviewResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
