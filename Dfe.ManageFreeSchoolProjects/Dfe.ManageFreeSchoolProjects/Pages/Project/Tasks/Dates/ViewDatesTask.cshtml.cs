using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Task;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.Dates
{
    public class ViewPropertyTaskModel : PageModel
    {
        private readonly ILogger<ViewSchoolTaskModel> _logger;
        private readonly IGetProjectByTaskService _getProjectService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public GetProjectByTaskResponse Project { get; set; }

        public ViewPropertyTaskModel(
            IGetProjectByTaskService getProjectService,
            ILogger<ViewSchoolTaskModel> logger)
        {
            _logger = logger;
            _getProjectService = getProjectService;
        }

        public async Task<IActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            try
            {
                Project = await _getProjectService.Execute(ProjectId);
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }

            return Page();
        }

        public ActionResult OnPost()
        {
            return Redirect(string.Format(RouteConstants.ProjectOverview, ProjectId));
        }
    }
}
