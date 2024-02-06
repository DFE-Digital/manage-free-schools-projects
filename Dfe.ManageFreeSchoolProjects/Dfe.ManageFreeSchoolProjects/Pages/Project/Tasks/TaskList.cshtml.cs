using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks
{
    public class TaskListModel : PageModel
    {
        private readonly IGetProjectByTaskSummaryService _getProjectTaskListSummaryService;
        private readonly ICreateTasksService _createTasksService;
        private readonly ILogger<TaskListModel> _logger;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "schoolName")]
        public string SchoolName { get; set; }

        public ProjectByTaskSummaryResponse ProjectTaskListSummary { get; set; }

        public TaskListModel(
            IGetProjectByTaskSummaryService getProjectTaskListSummaryService,
            ICreateTasksService createTasksService,
            ILogger<TaskListModel> logger)
        {
            _getProjectTaskListSummaryService = getProjectTaskListSummaryService;
            _createTasksService = createTasksService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            ProjectTaskListSummary = await _getProjectTaskListSummaryService.Execute(ProjectId);

            if (ProjectTaskListSummary is not null)
            {
                SchoolName = ProjectTaskListSummary.SchoolName;
                return Page(); 
            }                
            
            await _createTasksService.Execute(ProjectId);
            ProjectTaskListSummary = await _getProjectTaskListSummaryService.Execute(ProjectId);
            SchoolName = ProjectTaskListSummary.School.Name;

            return Page();
        }
    }
}