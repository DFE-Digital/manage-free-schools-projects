using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
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

    [BindProperty(Name = "a-rom-is-expected-to-happen")]
    public YesNo? AROMIsExpectedToHappen { get; set; }

    [BindProperty(Name = "expected-date-of-the-meeting", BinderType = typeof(DateInputModelBinder))]
    [DisplayName("Expected date of the meeting")]
    public DateTime? ExpectedDateOfTheMeeting { get; set; }


    [BindProperty(Name = "date-of-the-informal-meeting", BinderType = typeof(DateInputModelBinder))]
    [DisplayName("Date of the informal meeting")]
    public DateTime? DateOfInformalTheMeeting { get; set; }

    [BindProperty(Name = "date-of-the-formal-meeting", BinderType = typeof(DateInputModelBinder))]
    [DisplayName("Date of the formal meeting")]
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

        var romTask = Project.ReadinessToOpenMeetingTask;

        AROMIsExpectedToHappen = romTask.AROMIsExpectedToHappen;

        ExpectedDateOfTheMeeting = romTask.ExpectedDateOfTheMeeting;
        
        TypeOfMeetingHeld = romTask.TypeOfMeetingHeld;

        var dateOfTheMeeting = romTask.DateOfTheMeeting;

        DateOfInformalTheMeeting = TypeOfMeetingHeld == TypeOfMeetingHeld.InformalMeeting
            ? dateOfTheMeeting
            : null;

        DateOfFormalTheMeeting = TypeOfMeetingHeld == TypeOfMeetingHeld.FormalMeeting
            ? dateOfTheMeeting
            : null;
        
        WhyMeetingWasNotHeld = romTask.WhyMeetingWasNotHeld;
        
        PrincipalDesignateHasProvidedChecklist =
            romTask.PrincipalDesignateHasProvidedTheChecklist;

        CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable =
            romTask.CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable;

        SavedTheInternalROMReportInWorkplacesFolder =
            romTask.SavedTheInternalRomReportToWorkplacesFolder;
        
        SavedTheExternalROMReportInWorkplacesFolder =
            romTask.SavedTheExternalRomReportToWorkplacesFolder;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            CheckForErrors("date-of-the-informal-meeting", TypeOfMeetingHeld.InformalMeeting, DateOfInformalTheMeeting);
            CheckForErrors("date-of-the-formal-meeting", TypeOfMeetingHeld.FormalMeeting, DateOfFormalTheMeeting);
            
            ClearNotApplicableValues();
            
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
                AROMIsExpectedToHappen = AROMIsExpectedToHappen,
                ExpectedDateOfTheMeeting = AROMIsExpectedToHappen == YesNo.Yes ? ExpectedDateOfTheMeeting : null,
                DateOfTheMeeting = DateOfInformalTheMeeting ?? DateOfFormalTheMeeting,
                TypeOfMeetingHeld = TypeOfMeetingHeld,
                WhyMeetingWasNotHeld = WhyMeetingWasNotHeld,
                PrincipalDesignateHasProvidedTheChecklist = PrincipalDesignateHasProvidedChecklist,
                CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable =
                    CommissionedAnExternalExpertToAttendAnyMeetingsIfApplicable,
                SavedTheInternalRomReportToWorkplacesFolder = SavedTheInternalROMReportInWorkplacesFolder,
                SavedTheExternalRomReportToWorkplacesFolder = SavedTheExternalROMReportInWorkplacesFolder,
            }
        };
    }
    
    private void CheckForErrors(string id, TypeOfMeetingHeld typeOfMeetingHeld, DateTime? dateOfMeeting)
    {
        if (ModelState.IsValid && TypeOfMeetingHeld == typeOfMeetingHeld && dateOfMeeting == null)
        {
            errorService.AddErrors(ModelState.Keys, ModelState);
        }
        
        if (TypeOfMeetingHeld != typeOfMeetingHeld)
        {
            ModelState.Keys.Where(errorKey => errorKey.StartsWith(id))
                .ToList()
                .ForEach(errorKey => ModelState.Remove(errorKey));
        }
    }

    private void ClearNotApplicableValues()
    {
        switch (TypeOfMeetingHeld)
        {
            case TypeOfMeetingHeld.FormalMeeting:
                DateOfInformalTheMeeting = null;
                WhyMeetingWasNotHeld = null;
                return;
            case TypeOfMeetingHeld.InformalMeeting:
                DateOfFormalTheMeeting = null;
                WhyMeetingWasNotHeld = null;
                return;
            case TypeOfMeetingHeld.NoMeetingHeld:
                DateOfInformalTheMeeting = null;
                DateOfFormalTheMeeting = null;
                return;
        }

        DateOfInformalTheMeeting = null;
        DateOfFormalTheMeeting = null;
        WhyMeetingWasNotHeld = null;
    }
}