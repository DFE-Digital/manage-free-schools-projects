using System;
using System.Net;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks;

[Collection(ApiTestCollection.ApiTestCollectionName)]
public class ReadinessToOpenMeetingApiTests(ApiTestFixture apiTestFixture) : ApiTestsBase(apiTestFixture)
{
    [Fact]
    public async Task Patch_NewROM_Then_NewROMCreated()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);
        await context.SaveChangesAsync();

        var request = new UpdateProjectByTaskRequest()
        {
            ReadinessToOpenMeetingTask = new ReadinessToOpenMeetingTask
            {
                DateOfTheMeeting = new DateTime().Date,
                TypeOfMeetingHeld = TypeOfMeetingHeld.InformalMeeting,
                WhyMeetingWasNotHeld = null,
                PrincipalDesignateHasProvidedTheChecklist = true,
                SavedTheExternalRomReportToWorkplacesFolder = true,
                SavedTheInternalRomReportToWorkplacesFolder = true,
                CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable = true
            }
        };

        var projectResponse =
            await _client.UpdateProjectTask(projectId, request, TaskName.ReadinessToOpenMeeting.ToString());

        projectResponse.ReadinessToOpenMeetingTask.DateOfTheMeeting.Should()
            .Be(request.ReadinessToOpenMeetingTask.DateOfTheMeeting);
        projectResponse.ReadinessToOpenMeetingTask.TypeOfMeetingHeld.Should()
            .Be(request.ReadinessToOpenMeetingTask.TypeOfMeetingHeld);
        projectResponse.ReadinessToOpenMeetingTask.WhyMeetingWasNotHeld.Should()
            .BeNull();
        projectResponse.ReadinessToOpenMeetingTask.PrincipalDesignateHasProvidedTheChecklist.Should()
            .Be(request.ReadinessToOpenMeetingTask.PrincipalDesignateHasProvidedTheChecklist);
        projectResponse.ReadinessToOpenMeetingTask.SavedTheExternalRomReportToWorkplacesFolder.Should()
            .Be(request.ReadinessToOpenMeetingTask.SavedTheExternalRomReportToWorkplacesFolder);
        projectResponse.ReadinessToOpenMeetingTask.SavedTheInternalRomReportToWorkplacesFolder.Should()
            .Be(request.ReadinessToOpenMeetingTask.SavedTheInternalRomReportToWorkplacesFolder);
        projectResponse.ReadinessToOpenMeetingTask.CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable.Should()
            .Be(request.ReadinessToOpenMeetingTask.CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable);
    }


    [Fact]
    public async Task Patch_Existing_ROM_Then_ExistingROMUpdated()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        var pupilNumbersChecksTask = DatabaseModelBuilder.ReadinessToOpenMeetingTask(project.Rid,
            TypeOfMeetingHeld.NoMeetingHeld, DateTime.Today, whyMeetingWasNotHeld: string.Empty);
        context.Milestones.Add(pupilNumbersChecksTask);
        await context.SaveChangesAsync();
        
        var request = new UpdateProjectByTaskRequest()
        {
            ReadinessToOpenMeetingTask = new ReadinessToOpenMeetingTask
            {
                SavedTheInternalRomReportToWorkplacesFolder = false,
                CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable = false,
                SavedTheExternalRomReportToWorkplacesFolder = false
            }
        };
        
        var updateProjRes = await _client.UpdateProjectTask(projectId, request, TaskName.ReadinessToOpenMeeting.ToString());
        
        updateProjRes.ReadinessToOpenMeetingTask.SavedTheInternalRomReportToWorkplacesFolder.Should().BeFalse();
        updateProjRes.ReadinessToOpenMeetingTask.CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable.Should().BeFalse();
        updateProjRes.ReadinessToOpenMeetingTask.SavedTheExternalRomReportToWorkplacesFolder.Should().BeFalse();
    }
    
    
    [Fact]
    public async Task ROM_DateOfMeetingSet_ButMeetingTypeIsNotSet_ReturnsFormalMeetingType()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        var pupilNumbersChecksTask = DatabaseModelBuilder.ReadinessToOpenMeetingTask(project.Rid,
            TypeOfMeetingHeld.NotSet, DateTime.Today, whyMeetingWasNotHeld: string.Empty);
        context.Milestones.Add(pupilNumbersChecksTask);
        await context.SaveChangesAsync();

        var getProjRes =
            await _client.GetAsync(
                $"/api/v1/client/projects/{projectId}/tasks/{TaskName.ReadinessToOpenMeeting.ToString()}");

        getProjRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var romTask = await getProjRes.Content.ReadResponseFromWrapper<GetProjectByTaskResponse>();

        romTask.ReadinessToOpenMeetingTask.DateOfTheMeeting.Should().NotBeNull();
        romTask.ReadinessToOpenMeetingTask.TypeOfMeetingHeld.Should().Be(TypeOfMeetingHeld.FormalMeeting);
    }
}