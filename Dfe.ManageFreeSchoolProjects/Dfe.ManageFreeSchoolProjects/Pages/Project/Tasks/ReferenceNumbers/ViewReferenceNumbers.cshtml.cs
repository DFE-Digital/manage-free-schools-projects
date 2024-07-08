using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.ReferenceNumbers
{
    public class ViewReferenceNumbersModel : ViewTaskBaseModel
    {
        public ProjectByTaskSummaryResponse ProjectTaskListSummary { get; set; }

        private readonly ILogger<ViewReferenceNumbersModel> _logger;

        public ViewReferenceNumbersModel(
            IGetProjectByTaskService getProjectService,
            ILogger<ViewReferenceNumbersModel> logger,
            IGetTaskStatusService getTaskStatusService, IUpdateTaskStatusService updateTaskStatusService) : base(getProjectService, getTaskStatusService, updateTaskStatusService)
        {
            _logger = logger;
        }
        public ProjectOverviewResponse ProjectOverview { get; set; }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            await GetTask(TaskName.ReferenceNumbers);

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            _logger.LogMethodEntered();

            await PostTask(TaskName.ReferenceNumbers);

            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}
