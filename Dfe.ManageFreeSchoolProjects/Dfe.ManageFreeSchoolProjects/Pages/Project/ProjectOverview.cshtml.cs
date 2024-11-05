using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace Dfe.ManageFreeSchoolProjects.Pages.Project
{
    public class ProjectOverviewModel(
        IGetProjectOverviewService getProjectOverviewService,
        ILogger<ProjectOverviewModel> logger)
        : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public ProjectOverviewResponse Project { get; set; }

        public async Task<IActionResult> OnGet()
        {
            logger.LogMethodEntered();
            
            try
            {
                var projectId = RouteData.Values["projectId"] as string;
                Project = await getProjectOverviewService.Execute(projectId);
            }
            catch (Exception ex)
            {
                logger.LogErrorMsg(ex);
            }

            return Page();
        }
    }
}
