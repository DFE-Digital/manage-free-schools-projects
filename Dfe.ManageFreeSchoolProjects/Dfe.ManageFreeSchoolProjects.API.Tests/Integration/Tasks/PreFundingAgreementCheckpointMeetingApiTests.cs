using System;
using System.Net;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks;

[Collection(ApiTestCollection.ApiTestCollectionName)]
public class PreFundingAgreementCheckpointMeetingApiTests(ApiTestFixture apiTestFixture) : ApiTestsBase(apiTestFixture)
{
    [Fact]
    public async Task Patch_NewPFACM_Then_NewPFACMCreated()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);
        await context.SaveChangesAsync();

        var request = new UpdateProjectByTaskRequest()
        {
            PreFundingAgreementCheckpointMeetingTask = new PreFundingAgreementCheckpointMeetingTask
            {
                DateOfTheMeeting = new DateTime().Date,
                TypeOfMeetingHeld = TypeOfMeetingHeld.FormalCheckpointMeeting,
                WhyMeetingWasNotHeld = null,
                CommissionedExternalExpert = true,
                SavedMeetingNoteInWorkplacesFolder = true,
                SentAnEmailToTheTrust = true
            }
        };

        var projectResponse =
            await _client.UpdateProjectTask(projectId, request, TaskName.PreFundingAgreementCheckpointMeeting.ToString());

        projectResponse.PreFundingAgreementCheckpointMeetingTask.DateOfTheMeeting.Should()
            .Be(request.PreFundingAgreementCheckpointMeetingTask.DateOfTheMeeting);
        projectResponse.PreFundingAgreementCheckpointMeetingTask.TypeOfMeetingHeld.Should()
            .Be(request.PreFundingAgreementCheckpointMeetingTask.TypeOfMeetingHeld);
        projectResponse.PreFundingAgreementCheckpointMeetingTask.WhyMeetingWasNotHeld.Should()
            .BeNull();
        projectResponse.PreFundingAgreementCheckpointMeetingTask.SavedMeetingNoteInWorkplacesFolder.Should()
            .Be(request.PreFundingAgreementCheckpointMeetingTask.SavedMeetingNoteInWorkplacesFolder);
        projectResponse.PreFundingAgreementCheckpointMeetingTask.SentAnEmailToTheTrust.Should()
            .Be(request.PreFundingAgreementCheckpointMeetingTask.SentAnEmailToTheTrust);
        projectResponse.PreFundingAgreementCheckpointMeetingTask.CommissionedExternalExpert.Should()
            .Be(request.PreFundingAgreementCheckpointMeetingTask.CommissionedExternalExpert);
    }


    [Fact]
    public async Task Patch_Existing_PFACM_Then_ExistingPFACMUpdated()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        var preFundingAgreementCheckpointMeetingTask = DatabaseModelBuilder.PreFundingAgreementCheckpointMeetingTask(project.Rid,
            TypeOfMeetingHeld.NoMeetingHeld, DateTime.Today, whyMeetingWasNotHeld: string.Empty);
        context.Milestones.Add(preFundingAgreementCheckpointMeetingTask);
        await context.SaveChangesAsync();
        
        var request = new UpdateProjectByTaskRequest()
        {
            PreFundingAgreementCheckpointMeetingTask = new PreFundingAgreementCheckpointMeetingTask
            {
                SavedMeetingNoteInWorkplacesFolder = false,
                CommissionedExternalExpert = false,
                SentAnEmailToTheTrust = false
            }
        };
        
        var updateProjRes = await _client.UpdateProjectTask(projectId, request, TaskName.PreFundingAgreementCheckpointMeeting.ToString());
        
        updateProjRes.PreFundingAgreementCheckpointMeetingTask.SavedMeetingNoteInWorkplacesFolder.Should().BeFalse();
        updateProjRes.PreFundingAgreementCheckpointMeetingTask.CommissionedExternalExpert.Should().BeFalse();
        updateProjRes.PreFundingAgreementCheckpointMeetingTask.SentAnEmailToTheTrust.Should().BeFalse();
    }
    
    
    [Fact]
    public async Task PFACM_DateOfMeetingSet_ButMeetingTypeIsNotSet_ReturnsFormalCheckpointMeetingType()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        var preFundingAgreementCheckpointMeetingTask = DatabaseModelBuilder.PreFundingAgreementCheckpointMeetingTask(project.Rid,
            TypeOfMeetingHeld.NotSet, DateTime.Today, whyMeetingWasNotHeld: string.Empty);
        context.Milestones.Add(preFundingAgreementCheckpointMeetingTask);
        await context.SaveChangesAsync();

        var getProjRes =
            await _client.GetAsync(
                $"/api/v1/client/projects/{projectId}/tasks/{TaskName.PreFundingAgreementCheckpointMeeting.ToString()}");

        getProjRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var pfacmTask = await getProjRes.Content.ReadResponseFromWrapper<GetProjectByTaskResponse>();

        pfacmTask.PreFundingAgreementCheckpointMeetingTask.DateOfTheMeeting.Should().NotBeNull();
        pfacmTask.PreFundingAgreementCheckpointMeetingTask.TypeOfMeetingHeld.Should().Be(TypeOfMeetingHeld.FormalCheckpointMeeting);
    }
}