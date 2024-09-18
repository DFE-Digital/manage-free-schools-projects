using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.ReadinessToOpenMeeting;

public class EditROMViewModel(
    IGetProjectByTaskService getProjectService,
    ErrorService errorService,
    IUpdateProjectByTaskService updateProjectTaskService,
    ILogger<EditROMViewModel> logger) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    public GetProjectByTaskResponse Project { get; set; }
    
    [BindProperty(Name = "date-of-the-informal-meeting", BinderType = typeof(DateInputModelBinder))]
    public DateTime? DateOfInformalTheMeeting { get; set; }

    [BindProperty(Name = "date-of-the-formal-meeting", BinderType = typeof(DateInputModelBinder))]
    public DateTime? DateOfFormalTheMeeting { get; set; }

    [BindProperty(Name = "why-meeting-not-held")]
    public string WhyMeetingWasNotHeld { get; set; }

    [BindProperty(Name = "principal-designate-has-provided-checklist")]
    public bool? PrincipalDesignateHasProvidedChecklist { get; set; }

    [BindProperty(Name = "commissioned-external-expert-to-attend-any-meetings")]
    public bool? CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable { get; set; }

    [BindProperty(Name = "saved-the-internal-rom-report-workplaces-folder")]
    public bool? SavedTheInternalROMReportInWorkplacesFolder { get; set; }

    [BindProperty(Name = "saved-the-external-rom-report-workplaces-folder")]
    public bool? SavedTheExternalROMReportInWorkplacesFolder { get; set; }
    
    [BindProperty(Name = "type-of-meeting-held")]
    public TypeOfMeetingHeld TypeOfMeetingHeld { get; set; }
    
    public async Task<IActionResult> OnGet()
    {
        Project = await getProjectService.Execute(ProjectId, TaskName.ReadinessToOpenMeeting);

        TypeOfMeetingHeld = Project.ReadinessToOpenMeetingTask.TypeOfMeetingHeld;

        var dateOfTheMeeting = Project.ReadinessToOpenMeetingTask.DateOfTheMeeting;

        DateOfInformalTheMeeting = TypeOfMeetingHeld == TypeOfMeetingHeld.InformalMeeting
            ? dateOfTheMeeting
            : null;

        DateOfFormalTheMeeting = TypeOfMeetingHeld == TypeOfMeetingHeld.FormalMeeting
            ? dateOfTheMeeting
            : null;
        
        WhyMeetingWasNotHeld = Project.ReadinessToOpenMeetingTask.WhyMeetingWasNotHeld;
        PrincipalDesignateHasProvidedChecklist =
            Project.ReadinessToOpenMeetingTask.PrincipalDesignateHasProvidedTheChecklist;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                Project = await getProjectService.Execute(ProjectId, TaskName.ReadinessToOpenMeeting);
                errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }
            
            var request = BuildUpdateRequest();

            await updateProjectTaskService.Execute(ProjectId, request);

            return Redirect(string.Format(RouteConstants.ViewReadinessToOpenMeeting, ProjectId));
        }
        catch (Exception e)
        {
            logger.LogErrorMsg(e);
            throw;
        }
    }

    private UpdateProjectByTaskRequest BuildUpdateRequest()
    {
        return new UpdateProjectByTaskRequest
        {
            ReadinessToOpenMeetingTask = new ReadinessToOpenMeetingTask
            {
                DateOfTheMeeting = DateOfInformalTheMeeting ?? DateOfFormalTheMeeting,
                TypeOfMeetingHeld = TypeOfMeetingHeld,
                WhyMeetingWasNotHeld = WhyMeetingWasNotHeld,
                PrincipalDesignateHasProvidedTheChecklist = PrincipalDesignateHasProvidedChecklist,
                CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable =
                    CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable,
                SavedTheExternalRomReportToWorkplacesFolder = SavedTheExternalROMReportInWorkplacesFolder,
                SavedTheInternalRomReportToWorkplacesFolder = SavedTheInternalROMReportInWorkplacesFolder
            }
        };
    }
}