using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.ReadinessToOpenMeeting;

public class EditReadinessToOpenMeeting(IGetProjectByTaskService getProjectService) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    public GetProjectByTaskResponse Project { get; set; }
    
    [BindProperty(Name = "type-of-meeting-held")]
    public TypeOfMeetingHeld? TypeOfMeetingHeld { get; set; }
    
    [BindProperty(Name = "date-of-the-meeting")]
    public DateTime? DateOfTheMeeting { get; set; }

    [BindProperty(Name = "why-meeting-not-held")]
    public string WhyMeetingWasNotHeld { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Project = await getProjectService.Execute(ProjectId, TaskName.ReadinessToOpenMeeting);

        TypeOfMeetingHeld = Project.ReadinessToOpenMeetingTask.TypeOfMeetingHeld ?? API.Contracts.Project.Tasks.TypeOfMeetingHeld.NotSet;
        DateOfTheMeeting = Project.ReadinessToOpenMeetingTask.DateOfTheMeeting;
        WhyMeetingWasNotHeld = Project.ReadinessToOpenMeetingTask.WhyMeetingWasNotHeld;
        
        
        
        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        return Redirect(string.Format(RouteConstants.ViewReadinessToOpenMeeting, ProjectId));
    }
}