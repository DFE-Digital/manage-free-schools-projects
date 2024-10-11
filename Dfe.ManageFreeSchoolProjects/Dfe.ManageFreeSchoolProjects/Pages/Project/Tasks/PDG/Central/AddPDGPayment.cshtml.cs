using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.Extensions.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Validators;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central
{
    public class AddPDGPaymentModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IAddProjectPaymentsService _addProjectPaymentsService;

        private readonly ILogger<AddPDGPaymentModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public string CurrentFreeSchoolName { get; set; }
        [BindProperty(Name = "payment-due-date", BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the payment is due")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        [Required]
        public DateTime? PaymentScheduleDate { get; set; }

        [BindProperty(Name = "payment-due-amount", BinderType = typeof(DecimalInputModelBinder))]
        [Display(Name = "Due amount")]
        [ValidMoney(0, 640000)]
        [Required]
        public decimal? PaymentScheduleAmount { get; set; }

        [BindProperty(Name = "payment-actual-date", BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the payment was sent")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        public DateTime? PaymentActualDate { get; set; }

        [BindProperty(Name = "payment-actual-amount", BinderType = typeof(DecimalInputModelBinder))]
        [Display(Name = "Amount sent")]
        [ValidMoney(0, 640000)]
        public decimal? PaymentActualAmount { get; set; }

        public AddPDGPaymentModel(IGetProjectByTaskService getProjectService, IAddProjectPaymentsService addProjectPaymentsService, ILogger<AddPDGPaymentModel> logger,
            ErrorService errorService)
        {
            _getProjectService = getProjectService;
            _logger = logger;
            _errorService = errorService;
            _addProjectPaymentsService = addProjectPaymentsService;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            if (!User.IsInRole(RolesConstants.GrantManagers))
            {
                return new UnauthorizedResult();
            }

            await LoadProject();
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            _logger.LogMethodEntered();

            if (!User.IsInRole(RolesConstants.GrantManagers))
            {
                return new UnauthorizedResult();
            }
            
            var project = await _getProjectService.Execute(ProjectId, TaskName.PDG);

            var totalGrant = project.PDGDashboard.RevisedGrant ?? project.PDGDashboard.InitialGrant;

            CurrentFreeSchoolName = project.SchoolName;

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            try
            {
                var request = new Payment()
                {       
                        PaymentScheduleDate = PaymentScheduleDate,
                        PaymentScheduleAmount = PaymentScheduleAmount,
                        PaymentActualDate = PaymentActualDate,
                        PaymentActualAmount = PaymentActualAmount,
                };

                await _addProjectPaymentsService.Execute(ProjectId, request);

                TempData["paymentAdded"] = true;

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
            var project = await _getProjectService.Execute(ProjectId, TaskName.PDG);

            var totalGrant = project.PDGDashboard.RevisedGrant ?? project.PDGDashboard.InitialGrant;

            CurrentFreeSchoolName = project.SchoolName;
        }

    }
}
