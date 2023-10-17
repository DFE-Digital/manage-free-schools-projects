using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
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
using Dfe.ManageFreeSchoolProjects.API.Contracts.Task;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.Construction
{
    public class ViewPropertyTaskModel : PageModel
    {
        private readonly ILogger<ViewSchoolTaskModel> _logger;
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IGetTaskStatusService _getTaskStatusService;
        private readonly IUpdateTaskStatusService _updateTaskStatusService;
        private readonly ErrorService _errorService;

        private const string TaskName = "Construction";

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public GetProjectByTaskResponse Project { get; set; }

        [BindProperty] public bool MarkAsCompleted { get; set; }

        public ProjectTaskStatus ProjectTaskStatus { get; set; }

        public ViewPropertyTaskModel(
            IGetProjectByTaskService getProjectService,
            ILogger<ViewSchoolTaskModel> logger, IGetTaskStatusService getTaskStatusService,
            IUpdateTaskStatusService updateTaskStatusService, ErrorService errorService)
        {
            _logger = logger;
            _getTaskStatusService = getTaskStatusService;
            _updateTaskStatusService = updateTaskStatusService;
            _errorService = errorService;
            _getProjectService = getProjectService;
        }

        public async Task<IActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            try
            {
                Project = await _getProjectService.Execute(ProjectId);
                var statusResponse = await _getTaskStatusService.Execute(ProjectId, TaskName);
                ProjectTaskStatus = statusResponse.ProjectTaskStatus;

                MarkAsCompleted = ProjectTaskStatus == ProjectTaskStatus.Completed;
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            ProjectTaskStatus = MarkAsCompleted ? ProjectTaskStatus.Completed : ProjectTaskStatus.InProgress;

            await _updateTaskStatusService.Execute(ProjectId, new UpdateTaskStatusRequest
            {
                TaskName = TaskName, ProjectTaskStatus = ProjectTaskStatus
            });

            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}