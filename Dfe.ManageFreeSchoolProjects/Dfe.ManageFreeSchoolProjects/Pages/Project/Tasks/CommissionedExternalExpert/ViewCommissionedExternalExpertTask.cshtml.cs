using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.CommissionedExternalExpert
{
    public class ViewCommissionedExternalExpertModel : ViewTaskBaseModel
    {
        private readonly ILogger<ViewCommissionedExternalExpertModel> _logger;

        public ViewCommissionedExternalExpertModel(
            IGetProjectByTaskService getProjectService,
            ILogger<ViewCommissionedExternalExpertModel> logger,
            IGetTaskStatusService getTaskStatusService, IUpdateTaskStatusService updateTaskStatusService) : base(getProjectService, getTaskStatusService, updateTaskStatusService)
        {
            _logger = logger;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            await GetTask(TaskName.CommissionedExternalExpert);

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            _logger.LogMethodEntered();

            await PostTask(TaskName.CommissionedExternalExpert);

            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}
