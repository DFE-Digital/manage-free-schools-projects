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
    public class DeletePDGPaymentModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IGetProjectPaymentsService _getProjectPaymentsService;
        private readonly IDeleteProjectPaymentsService _deleteProjectPaymentsService;
        private readonly ILogger<DeletePDGPaymentModel> _logger;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(SupportsGet = true, Name = "paymentIndex")]
        public int PaymentIndex { get; set; }
        public string CurrentFreeSchoolName { get; set; }
        public Payment Payment { get; set; }

        public DeletePDGPaymentModel(IGetProjectByTaskService getProjectService, IGetProjectPaymentsService getProjectPaymentsService, IDeleteProjectPaymentsService deleteProjectPaymentsService, ILogger<DeletePDGPaymentModel> logger)
        {
            _getProjectService = getProjectService;
            _getProjectPaymentsService = getProjectPaymentsService;
            _deleteProjectPaymentsService = deleteProjectPaymentsService;
            _logger = logger;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            await LoadProject();
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            try
            {
                await _deleteProjectPaymentsService.Execute(ProjectId, PaymentIndex);

                TempData["paymentDeleted"] = true;

                return Redirect(string.Format(RouteConstants.EditPDGPaymentScheduleCentral, ProjectId));
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

            var projectPayments = await _getProjectPaymentsService.Execute(ProjectId);

            Payment = projectPayments.Payments.Where(p => p.PaymentIndex == PaymentIndex).FirstOrDefault();
        }
    }
}
