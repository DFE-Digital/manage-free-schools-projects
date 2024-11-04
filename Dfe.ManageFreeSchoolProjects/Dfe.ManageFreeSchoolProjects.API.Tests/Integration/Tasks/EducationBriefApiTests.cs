using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks;

  [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class EducationBriefTaskApiTests : ApiTestsBase
    {
        public EducationBriefTaskApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_NewEducationBrief_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();


            var request = new UpdateProjectByTaskRequest()
            {
                EducationBrief = new EducationBriefTask()
                {
                    TrustConfirmedPlansAndPoliciesInPlace = true,
                    DateTrustProvidedEducationBrief = null,
                    CommissionedEEToReviewSafeguardingPolicy = true,
                    CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy = null,
                    DateEEReviewedEducationBrief = DateTime.Now.Date.AddDays(-5),
                    SavedCopiesOfPlansAndPoliciesInWorkplaces = null,
                    SavedEESpecificationAndAdviceInWorkplaces = true,
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.EducationBrief.ToString());

            projectResponse.EducationBrief.TrustConfirmedPlansAndPoliciesInPlace.Should().Be(request.EducationBrief.TrustConfirmedPlansAndPoliciesInPlace);
            projectResponse.EducationBrief.DateTrustProvidedEducationBrief.Should().Be(request.EducationBrief.DateTrustProvidedEducationBrief);
            projectResponse.EducationBrief.CommissionedEEToReviewSafeguardingPolicy.Should().Be(request.EducationBrief.CommissionedEEToReviewSafeguardingPolicy);
            projectResponse.EducationBrief.CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy.Should().Be(request.EducationBrief.CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy);
            projectResponse.EducationBrief.DateEEReviewedEducationBrief.Should().Be(request.EducationBrief.DateEEReviewedEducationBrief);
            projectResponse.EducationBrief.SavedCopiesOfPlansAndPoliciesInWorkplaces.Should().Be(request.EducationBrief.SavedCopiesOfPlansAndPoliciesInWorkplaces);
            projectResponse.EducationBrief.SavedEESpecificationAndAdviceInWorkplaces.Should().Be(request.EducationBrief.SavedEESpecificationAndAdviceInWorkplaces);
    }

        [Fact]
        public async Task Patch_ExistingEducationBrief_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var educationBriefTask = DatabaseModelBuilder.BuildEducationBriefTask(project.Rid);
            context.Milestones.Add(educationBriefTask);

            await context.SaveChangesAsync();
            
            var request = new UpdateProjectByTaskRequest()
            {
                EducationBrief = new EducationBriefTask()
                {
                    TrustConfirmedPlansAndPoliciesInPlace = true,
                    DateTrustProvidedEducationBrief = DateTime.Now.Date.AddDays(-5),
                    CommissionedEEToReviewSafeguardingPolicy = true,
                    CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy = true,
                    DateEEReviewedEducationBrief = DateTime.Now.Date.AddDays(-4),
                    SavedCopiesOfPlansAndPoliciesInWorkplaces =true,
                    SavedEESpecificationAndAdviceInWorkplaces = true,
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.EducationBrief.ToString());

            projectResponse.EducationBrief.TrustConfirmedPlansAndPoliciesInPlace.Should().Be(request.EducationBrief.TrustConfirmedPlansAndPoliciesInPlace);
            projectResponse.EducationBrief.DateTrustProvidedEducationBrief.Should().Be(request.EducationBrief.DateTrustProvidedEducationBrief);
            projectResponse.EducationBrief.CommissionedEEToReviewSafeguardingPolicy.Should().Be(request.EducationBrief.CommissionedEEToReviewSafeguardingPolicy);
            projectResponse.EducationBrief.CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy.Should().Be(request.EducationBrief.CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy);
            projectResponse.EducationBrief.DateEEReviewedEducationBrief.Should().Be(request.EducationBrief.DateEEReviewedEducationBrief);
            projectResponse.EducationBrief.SavedCopiesOfPlansAndPoliciesInWorkplaces.Should().Be(request.EducationBrief.SavedCopiesOfPlansAndPoliciesInWorkplaces);
            projectResponse.EducationBrief.SavedEESpecificationAndAdviceInWorkplaces.Should().Be(request.EducationBrief.SavedEESpecificationAndAdviceInWorkplaces);
    }
}