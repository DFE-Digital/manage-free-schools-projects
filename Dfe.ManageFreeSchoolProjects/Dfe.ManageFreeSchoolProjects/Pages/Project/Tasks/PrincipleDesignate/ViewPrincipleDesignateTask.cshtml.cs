using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PrincipleDesignate
{
    public class ViewPrincipleDesignateTaskModel : ViewTaskBaseModel
    {
        private readonly ILogger<ViewPrincipleDesignateTaskModel> _logger;

        public ViewPrincipleDesignateTaskModel(
            IGetProjectByTaskService getProjectService,
            ILogger<ViewPrincipleDesignateTaskModel> logger,
            IGetTaskStatusService getTaskStatusService, IUpdateTaskStatusService updateTaskStatusService) : base(getProjectService, getTaskStatusService, updateTaskStatusService)
        {
            _logger = logger;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            await GetTask(TaskName.PrincipleDesignate);

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            _logger.LogMethodEntered();

            await PostTask(TaskName.PrincipleDesignate);

            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}
