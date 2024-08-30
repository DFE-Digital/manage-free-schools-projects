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
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG;
using Dfe.ManageFreeSchoolProjects.Validators;
using System.Linq;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central
{
    public class EditPDGPaymentModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IGetProjectPaymentsService _getProjectPaymentsService;
        private readonly IUpdateProjectPaymentsService _updateProjectPaymentsService;
        private readonly ILogger<EditPDGPaymentModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(SupportsGet = true, Name = "paymentIndex")]
        public int PaymentIndex { get; set; }

        public int TotalGrant { get; set; }

        public string CurrentFreeSchoolName { get; set; }
        [BindProperty(Name = "payment-due-date", BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the payment is due")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        [Required]
        public DateTime? PaymentScheduleDate { get; set; }

        [BindProperty(Name = "payment-due-amount", BinderType = typeof(DecimalInputModelBinder))]
        [Display(Name = "Due amount")]
        [Required]
        public decimal? PaymentScheduleAmount { get; set; }

        [BindProperty(Name = "payment-actual-date", BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the payment was sent")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        public DateTime? PaymentActualDate { get; set; }

        [BindProperty(Name = "payment-actual-amount", BinderType = typeof(DecimalInputModelBinder))]
        [Display(Name = "Actual amount")]
        public decimal? PaymentActualAmount { get; set; }

        public EditPDGPaymentModel(IGetProjectByTaskService getProjectService, IGetProjectPaymentsService getProjectPaymentsService, IUpdateProjectPaymentsService updateProjectPaymentsService, ILogger<EditPDGPaymentModel> logger,
            ErrorService errorService)
        {
            _getProjectService = getProjectService;
            _logger = logger;
            _errorService = errorService;
            _getProjectPaymentsService = getProjectPaymentsService;
            _updateProjectPaymentsService = updateProjectPaymentsService;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            await LoadProject();
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.PDG);

            var totalGrant = project.PDGDashboard.RevisedGrant ?? project.PDGDashboard.InitialGrant;

            TotalGrant = (int)totalGrant;

            CurrentFreeSchoolName = project.SchoolName;

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var validPaymentScheduleAmount = IsValidMoney(PaymentScheduleAmount, "Due amount");

            if (validPaymentScheduleAmount != ValidationResult.Success)
            {
                ModelState.AddModelError("payment-due-amount", validPaymentScheduleAmount.ErrorMessage);
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var validPaymentActualAmount = IsValidMoney(PaymentActualAmount, "Actual amount");

            if (validPaymentActualAmount != ValidationResult.Success)
            {
                ModelState.AddModelError("payment-actual-amount", validPaymentActualAmount.ErrorMessage);
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            try
            {
                var request = new Payment()
                {
                    PaymentIndex = PaymentIndex,
                    PaymentScheduleDate = PaymentScheduleDate,
                    PaymentScheduleAmount = PaymentScheduleAmount,
                    PaymentActualDate = PaymentActualDate,
                    PaymentActualAmount = PaymentActualAmount,
                };

                await _updateProjectPaymentsService.Execute(ProjectId, request);

                TempData["paymentUpdated"] = true;
                TempData["paymentIndex"] = PaymentIndex;                  

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

            TotalGrant = (int)totalGrant;

            CurrentFreeSchoolName = project.SchoolName;

            var projectPayments = await _getProjectPaymentsService.Execute(ProjectId);

            var payment = projectPayments.Payments.Where(p => p.PaymentIndex == PaymentIndex).FirstOrDefault();

            PaymentScheduleDate = payment.PaymentScheduleDate;
            PaymentScheduleAmount = payment.PaymentScheduleAmount;
            PaymentActualDate = payment.PaymentActualDate;
            PaymentActualAmount = payment.PaymentActualAmount;
        }

        private ValidationResult IsValidMoney(object value, string displayName)
        {
            if (value is null)
                return ValidationResult.Success;

            var valueAsDec = (decimal)value;

            if (valueAsDec < 0 || valueAsDec > TotalGrant)
                return new ValidationResult(string.Format(ValidationConstants.NumberValidationMessage, displayName, 0, TotalGrant));

            if (Math.Round(valueAsDec, 2) != valueAsDec)
            {
                return new ValidationResult($"{displayName} must be two decimal places");
            }

            return ValidationResult.Success;
        }
    }
}
