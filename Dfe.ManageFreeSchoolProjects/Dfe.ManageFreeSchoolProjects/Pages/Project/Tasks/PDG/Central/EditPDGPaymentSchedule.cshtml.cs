using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Microsoft.Extensions.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using System.Linq;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central
{
    public class EditPDGPaymentScheduleModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IGetProjectPaymentsService _getProjectPaymentsService;
        private readonly ILogger<EditPDGPaymentScheduleModel> _logger;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public string CurrentFreeSchoolName { get; set; }
        public ProjectPayments ProjectPayments { get; set; }

        public EditPDGPaymentScheduleModel(IGetProjectByTaskService getProjectService, IGetProjectPaymentsService getProjectPaymentsService, ILogger<EditPDGPaymentScheduleModel> logger)
        {
            _getProjectService = getProjectService;
            _getProjectPaymentsService = getProjectPaymentsService;
            _logger = logger;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            await LoadProject();
            return Page();
        }

        public ActionResult OnPost()
        {
            try
            {
                return Redirect(string.Format(RouteConstants.AddPDGPaymentCentral, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.PaymentSchedule);

            CurrentFreeSchoolName = project.SchoolName;

            ProjectPayments = await _getProjectPaymentsService.Execute(ProjectId);
        }
    }
}
