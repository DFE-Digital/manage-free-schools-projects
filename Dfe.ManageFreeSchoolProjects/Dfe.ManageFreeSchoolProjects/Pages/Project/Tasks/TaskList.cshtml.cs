using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks
{
    public class TaskListModel : PageModel
    {
        private readonly IGetProjectTaskListSummaryService _getProjectTaskListSummaryService;
        private readonly ILogger<TaskListModel> _logger;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public ProjectTaskListSummaryResponse ProjectTaskListSummary { get; set; }

        public TaskListModel(
            IGetProjectTaskListSummaryService getProjectTaskListSummaryService,
            ILogger<TaskListModel> logger)
        {
            _getProjectTaskListSummaryService = getProjectTaskListSummaryService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            try
            {
                ProjectTaskListSummary = await _getProjectTaskListSummaryService.Execute(ProjectId);
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
