using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Task;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.School
{
    public class ViewSchoolTask : PageModel
    {
        private readonly ILogger<ViewSchoolTask> _logger;
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IGetTaskStatusService _getTaskStatusService;
        private readonly IUpdateTaskStatusService _updateTaskStatusService;
        private readonly ErrorService _errorService;

        private const string SchoolTaskName = "School";

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty] public bool MarkAsCompleted { get; set; }

        public ProjectTaskStatus ProjectTaskStatus { get; set; }

        public GetProjectByTaskResponse Project { get; set; }

        public ViewSchoolTask(
            IGetProjectByTaskService getProjectService,
            ILogger<ViewSchoolTask> logger,
            IGetTaskStatusService getTaskStatusService, IUpdateTaskStatusService updateTaskStatusService,
            ErrorService errorService)
        {
            _logger = logger;
            _getTaskStatusService = getTaskStatusService;
            _updateTaskStatusService = updateTaskStatusService;
            _errorService = errorService;
            _getProjectService = getProjectService;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            Project = await _getProjectService.Execute(ProjectId, TaskName.School);

            var taskStatusResponse = await _getTaskStatusService.Execute(ProjectId, SchoolTaskName);

            ProjectTaskStatus = taskStatusResponse.ProjectTaskStatus;
            MarkAsCompleted = ProjectTaskStatus == ProjectTaskStatus.Completed;

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
                TaskName = SchoolTaskName, ProjectTaskStatus = ProjectTaskStatus
            });
            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}