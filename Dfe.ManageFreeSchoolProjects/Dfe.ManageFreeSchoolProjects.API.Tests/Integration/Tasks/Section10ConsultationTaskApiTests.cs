using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class Section10ConsultationTaskApiTests : ApiTestsBase
    {
        public Section10ConsultationTaskApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_NewSection10Consultation_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var dateTwentyDaysInFuture = new DateTime().AddDays(20);

            var request = new UpdateProjectByTaskRequest()
            {
                Section10Consultation = new Section10ConsultationTask()
                {
                    ExpectedDateForReceivingFindingsFromTrust = dateTwentyDaysInFuture,
                    ReceivedConsultationFindingsFromTrust = null,
                    DateReceived = null,
                    ConsultationFulfilsTrustSection10StatutoryDuty = null,
                    Comments = "",
                    SavedFindingsInWorkplacesFolder = null
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.Section10Consultation.ToString());

            projectResponse.Section10Consultation.ExpectedDateForReceivingFindingsFromTrust.Should().Be(request.Section10Consultation.ExpectedDateForReceivingFindingsFromTrust);
            projectResponse.Section10Consultation.ReceivedConsultationFindingsFromTrust.Should().BeNull();
            projectResponse.Section10Consultation.DateReceived.Should().BeNull();
            projectResponse.Section10Consultation.ConsultationFulfilsTrustSection10StatutoryDuty.Should().BeNull();
            projectResponse.Section10Consultation.Comments.Should().Be(request.Section10Consultation.Comments);
            projectResponse.Section10Consultation.SavedFindingsInWorkplacesFolder.Should().BeNull();
        }

        [Fact]
        public async Task Patch_ExistingSection10Consultation_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var Section10ConsultationTask = DatabaseModelBuilder.BuildSection10ConsultationTask(project.Rid);
            context.Milestones.Add(Section10ConsultationTask);

            await context.SaveChangesAsync();

            var dateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
                Section10Consultation = new Section10ConsultationTask()
                {
                    FundingArrangementAgreed = true,
                    RealisticYearOfOpening = "2049/2050",
                    FundingArrangementDetailsAgreed = "text",
                    ProvisionalOpeningDate = dateNineDaysInFuture,
                    SharepointLink = "https://sharepoint/completed"
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.Section10Consultation.ToString());

            projectResponse.Section10Consultation.SharepointLink.Should().Be(request.Section10Consultation.SharepointLink);
            projectResponse.Section10Consultation.RealisticYearOfOpening.Should().Be(request.Section10Consultation.RealisticYearOfOpening);
            projectResponse.Section10Consultation.ProvisionalOpeningDate.Should().Be(request.Section10Consultation.ProvisionalOpeningDate);
            projectResponse.Section10Consultation.FundingArrangementAgreed.Should().Be(request.Section10Consultation.FundingArrangementAgreed);
            projectResponse.Section10Consultation.FundingArrangementDetailsAgreed.Should().Be(request.Section10Consultation.FundingArrangementDetailsAgreed);
        }
    }
}
