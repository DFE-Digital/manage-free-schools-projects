using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Task
{
    public class ViewSchoolTaskModel : PageModel
    {
        private readonly ILogger<ViewSchoolTaskModel> _logger;
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IGetTaskStatusService _getTaskStatusService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty] public bool MarkAsCompleted { get; set; }

        public ProjectTaskStatus ProjectTaskStatus { get; set; }

        public GetProjectByTaskResponse Project { get; set; }

        public ViewSchoolTaskModel(
            IGetProjectByTaskService getProjectService,
            ILogger<ViewSchoolTaskModel> logger,
            IGetTaskStatusService getTaskStatusService)
        {
            _logger = logger;
            _getTaskStatusService = getTaskStatusService;
            _getProjectService = getProjectService;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            try
            {
                Project = await _getProjectService.Execute(ProjectId);
                ProjectTaskStatus = await _getTaskStatusService.Execute(ProjectId, "School");
                MarkAsCompleted = ProjectTaskStatus == ProjectTaskStatus.Completed;
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