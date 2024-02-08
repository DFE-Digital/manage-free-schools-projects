using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Task;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.FinancePlan
{
    public class ViewFinancePlanTaskModel : PageModel
    {
        private readonly ILogger<ViewFinancePlanTaskModel> _logger;
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IGetTaskStatusService _getTaskStatusService;
        private readonly IUpdateTaskStatusService _updateTaskStatusService;

        public ProjectTaskStatus ProjectTaskStatus { get; set; }

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty] 
        public bool MarkAsCompleted { get; set; }

        public GetProjectByTaskResponse Project { get; set; }

        public ViewFinancePlanTaskModel(
            IGetProjectByTaskService getProjectService,
            ILogger<ViewFinancePlanTaskModel> logger,
            IGetTaskStatusService getTaskStatusService, IUpdateTaskStatusService updateTaskStatusService)
        {
            _logger = logger;
            _getTaskStatusService = getTaskStatusService;
            _updateTaskStatusService = updateTaskStatusService;
            _getProjectService = getProjectService;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            Project = await _getProjectService.Execute(ProjectId, TaskName.FinancePlan);

            var taskStatusResponse = await _getTaskStatusService.Execute(ProjectId, TaskName.FinancePlan.ToString());

            ProjectTaskStatus = taskStatusResponse.ProjectTaskStatus;
            MarkAsCompleted = ProjectTaskStatus == ProjectTaskStatus.Completed;

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            ProjectTaskStatus = MarkAsCompleted ? ProjectTaskStatus.Completed : ProjectTaskStatus.InProgress;

            await _updateTaskStatusService.Execute(ProjectId, new UpdateTaskStatusRequest
            {
                TaskName = TaskName.FinancePlan.ToString(),
                ProjectTaskStatus = ProjectTaskStatus
            });
            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}
