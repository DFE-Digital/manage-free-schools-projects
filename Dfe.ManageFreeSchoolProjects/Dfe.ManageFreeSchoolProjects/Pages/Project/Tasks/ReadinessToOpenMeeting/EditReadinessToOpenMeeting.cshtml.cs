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
    
    public async Task<IActionResult> OnGet()
    {
        Project = await getProjectService.Execute(ProjectId, TaskName.ReadinessToOpenMeeting);
        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        return Redirect(string.Format(RouteConstants.ViewReadinessToOpenMeeting, ProjectId));
    }
}