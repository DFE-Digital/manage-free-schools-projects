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

            var dateNineteenDaysInFuture = new DateTime().AddDays(19);
            var dateTwentyDaysInFuture = new DateTime().AddDays(20);

            var request = new UpdateProjectByTaskRequest()
            {
                Section10Consultation = new Section10ConsultationTask()
                {
                    ExpectedDateForReceivingFindingsFromTrust = dateNineteenDaysInFuture,
                    ReceivedConsultationFindingsFromTrust = true,
                    DateReceived = dateTwentyDaysInFuture,
                    ConsultationFulfilsTrustSection10StatutoryDuty = true,
                    Comments = "The consultation has been completed with no issues.",
                    SavedFindingsInWorkplacesFolder = true
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.Section10Consultation.ToString());

            projectResponse.Section10Consultation.ExpectedDateForReceivingFindingsFromTrust.Should().Be(request.Section10Consultation.ExpectedDateForReceivingFindingsFromTrust);
            projectResponse.Section10Consultation.ReceivedConsultationFindingsFromTrust.Should().Be(request.Section10Consultation.ReceivedConsultationFindingsFromTrust);
            projectResponse.Section10Consultation.DateReceived.Should().Be(request.Section10Consultation.DateReceived);
            projectResponse.Section10Consultation.ConsultationFulfilsTrustSection10StatutoryDuty.Should().Be(request.Section10Consultation.ConsultationFulfilsTrustSection10StatutoryDuty);
            projectResponse.Section10Consultation.Comments.Should().Be(request.Section10Consultation.Comments);
            projectResponse.Section10Consultation.SavedFindingsInWorkplacesFolder.Should().Be(request.Section10Consultation.SavedFindingsInWorkplacesFolder);
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

            var dateNineteenDaysInFuture = new DateTime().AddDays(19);
            var dateTwentyDaysInFuture = new DateTime().AddDays(20);

            var request = new UpdateProjectByTaskRequest()
            {
                Section10Consultation = new Section10ConsultationTask()
                {
                    ExpectedDateForReceivingFindingsFromTrust = dateNineteenDaysInFuture,
                    ReceivedConsultationFindingsFromTrust = true,
                    DateReceived = dateTwentyDaysInFuture,
                    ConsultationFulfilsTrustSection10StatutoryDuty = true,
                    Comments = "The consultation has been completed with no issues.",
                    SavedFindingsInWorkplacesFolder = true
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.Section10Consultation.ToString());

            projectResponse.Section10Consultation.ExpectedDateForReceivingFindingsFromTrust.Should().Be(request.Section10Consultation.ExpectedDateForReceivingFindingsFromTrust);
            projectResponse.Section10Consultation.ReceivedConsultationFindingsFromTrust.Should().Be(request.Section10Consultation.ReceivedConsultationFindingsFromTrust);
            projectResponse.Section10Consultation.DateReceived.Should().Be(request.Section10Consultation.DateReceived);
            projectResponse.Section10Consultation.ConsultationFulfilsTrustSection10StatutoryDuty.Should().Be(request.Section10Consultation.ConsultationFulfilsTrustSection10StatutoryDuty);
            projectResponse.Section10Consultation.Comments.Should().Be(request.Section10Consultation.Comments);
            projectResponse.Section10Consultation.SavedFindingsInWorkplacesFolder.Should().Be(request.Section10Consultation.SavedFindingsInWorkplacesFolder);
        }
    }
}
