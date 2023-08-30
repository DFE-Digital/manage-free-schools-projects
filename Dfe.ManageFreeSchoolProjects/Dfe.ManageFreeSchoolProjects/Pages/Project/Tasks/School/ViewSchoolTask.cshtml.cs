using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Task
{
    public class ViewSchoolTaskModel : PageModel
    {
        private readonly ILogger<ViewSchoolTaskModel> _logger;
        private readonly IGetProjectService _getProjectService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public GetProjectResponse Project { get; set; }

        public ViewSchoolTaskModel(
            IGetProjectService getProjectService,
            ILogger<ViewSchoolTaskModel> logger)
        {
            _logger = logger;
            _getProjectService = getProjectService;
        }

        public async Task<ActionResult> OnGet()
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
    }
}
