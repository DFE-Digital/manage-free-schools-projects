using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using DocumentFormat.OpenXml.EMMA;
using System.Linq;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central
{
    public class ViewPDGModel : ViewTaskBaseModel
    {
        private readonly ILogger<ViewPDGModel> _logger;
        private readonly IGetProjectPaymentsService _getProjectPaymentsService;

        public ProjectPayments ProjectPayments { get; set; }

        public GrantLetters GrantLetters { get; set; }

        public ViewPDGModel(
            ILogger<ViewPDGModel> logger,
            IGetProjectPaymentsService getProjectPaymentsService,
            IGetProjectByTaskService getProjectService,
            IGetTaskStatusService getTaskStatusService,
            IUpdateTaskStatusService updateTaskStatusService) : base(getProjectService, getTaskStatusService, updateTaskStatusService)
        {
            _logger = logger;
            _getProjectPaymentsService = getProjectPaymentsService;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            await GetTask(TaskName.PDG);

            ProjectPayments = await _getProjectPaymentsService.Execute(ProjectId);
            GrantLetters = new GrantLetters(); //todo: get data
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            _logger.LogMethodEntered();

            await PostTask(TaskName.PDG);

            return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
        }
    }
}
