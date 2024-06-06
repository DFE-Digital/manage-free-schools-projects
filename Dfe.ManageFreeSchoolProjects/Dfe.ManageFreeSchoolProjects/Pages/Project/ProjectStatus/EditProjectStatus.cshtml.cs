using System;
using System.ComponentModel;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Extensions;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.ProjectStatus
{
    public class EditProjectStatusModel : PageModel
    {

        private readonly IGetProjectOverviewService _getProjectOverviewService;
        private readonly ILogger<ProjectOverviewModel> _logger;
        private readonly ErrorService _errorService;
        public ProjectOverviewResponse Project { get; set; }
        
        public string ProjectId { get; set; }

        public API.Contracts.Project.ProjectStatus ProjectStatus { get; set; }

        public EditProjectStatusModel(IGetProjectOverviewService getProjectOverviewService,
            ILogger<ProjectOverviewModel> logger,
            ErrorService errorService)
        {
            _getProjectOverviewService = getProjectOverviewService;
            _logger = logger;
            _errorService = errorService;
        }

        public async Task<IActionResult> OnGet()
        {

            try
            {
                var projectId = RouteData.Values["projectId"] as string;

                Project = await _getProjectOverviewService.Execute(projectId);
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
            }
            
            return Page();
        }
    

    public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
                
                
            }
            
            return null;
        }
    }
}
