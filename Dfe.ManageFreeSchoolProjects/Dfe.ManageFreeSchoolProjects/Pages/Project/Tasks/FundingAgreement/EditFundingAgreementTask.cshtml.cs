using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using System;
using Dfe.ManageFreeSchoolProjects.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;


namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.FundingAgreement
{
    public class EditFundingAgreementTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditFundingAgreementTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }
        
        [BindProperty(Name = "tailored-the-funding-agreement")]
        public bool? TailoredTheFundingAgreement { get; set; }
        
        [BindProperty(Name = "shared-fa-with-the-trust")]
        public bool? SharedFAWithTheTrust { get; set; }
        
        [BindProperty(Name = "trust-has-signed-the-fa")]
        public string TrustHasSignedTheFA { get; set; }
        
        [BindProperty(Name = "date-the-trust-signed-fa", BinderType = typeof(DateInputModelBinder))]
        public DateTime?  DateTheTrustSignedFA{ get; set; }

        [BindProperty(Name = "expected-date-fa-is-signed-on-secretary-of-states-behalf", BinderType = typeof(DateInputModelBinder))]
        public DateTime? ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf { get; set; }

        [BindProperty(Name = "funding-agreement-signed")]
        public bool? FundingAgreementSigned { get; set; }

        [BindProperty(Name = "date-fa-was-signed", BinderType = typeof(DateInputModelBinder))]
        public DateTime? DateFAWasSigned { get; set; }

        [BindProperty(Name = "saved-fa-documents-in-workplaces-folder")]
        public bool? SavedFADocumentsInWorkplacesFolder { get; set; }

        public string SchoolName { get; set; }
        
        public EditFundingAgreementTaskModel(IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditFundingAgreementTaskModel> logger,
            ErrorService errorService)
        {
            _getProjectService = getProjectService;
            _updateProjectTaskService = updateProjectTaskService;
            _logger = logger;
            _errorService = errorService;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            await LoadProject();
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            var trustHasSignedTheFA = ConvertYesNo(TrustHasSignedTheFA);

            if (trustHasSignedTheFA != YesNo.Yes)
            {
                // Ignore any errors for the RPA fields if the trust is not opting into RPA
                var errorKeys = ModelState.Keys.Where(k => k.StartsWith("date-trust-signed-fa")).ToList();

                errorKeys.ForEach(k => ModelState.Remove(k));
            }
            
            var project = await _getProjectService.Execute(ProjectId, TaskName.FundingAgreement);
            SchoolName = project.SchoolName;
            
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            if (FundingAgreementSigned == true && DateFAWasSigned.HasValue == false)
            {
                ModelState.AddModelError("date-fa-was-signed", "Enter the actual date FA was signed");
            }

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            try
            {
                var request = new UpdateProjectByTaskRequest()
                {
                    FundingAgreement = new FundingAgreementTask()
                    {
                        TailoredTheFundingAgreement = TailoredTheFundingAgreement,
                        SharedFAWithTheTrust = SharedFAWithTheTrust,
                        TrustHasSignedTheFA = ConvertYesNo(TrustHasSignedTheFA),
                        ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf = ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf,
                        DateFAWasSigned = FundingAgreementSigned == true ? DateFAWasSigned : null,
                        SavedFADocumentsInWorkplacesFolder = SavedFADocumentsInWorkplacesFolder,
                           
                    }
                };
                
                if (trustHasSignedTheFA == YesNo.Yes)
                {
                    request.FundingAgreement.DateTheTrustSignedFA = DateTheTrustSignedFA;
                }

                await _updateProjectTaskService.Execute(ProjectId, request);

                return Redirect(string.Format(RouteConstants.ViewFundingAgreement, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.FundingAgreement);

            TailoredTheFundingAgreement = project.FundingAgreement.TailoredTheFundingAgreement;
            SharedFAWithTheTrust = project.FundingAgreement.SharedFAWithTheTrust;
            TrustHasSignedTheFA = project.FundingAgreement.TrustHasSignedTheFA?.ToString();
            DateTheTrustSignedFA = project.FundingAgreement.DateTheTrustSignedFA;
            ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf = project.FundingAgreement.ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf;
            DateFAWasSigned = project.FundingAgreement.DateFAWasSigned;
            SavedFADocumentsInWorkplacesFolder = project.FundingAgreement.SavedFADocumentsInWorkplacesFolder;

            FundingAgreementSigned = DateFAWasSigned.HasValue ? true : null;

            SchoolName = project.SchoolName;
        }
        
        public static YesNo? ConvertYesNo(string value)
        {
            return Enum.TryParse<YesNo>(value, true, out var result) ? result : null;
        }
    }
}
