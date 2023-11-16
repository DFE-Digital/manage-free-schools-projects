
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Trust;
using Dfe.ManageFreeSchoolProjects.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class ConfirmTrustTaskModel : PageModel
    {
        private readonly IGetTrustByRefService _getTrustByRefService;
        private readonly ILogger<ConfirmTrustTaskModel> _logger;
        private readonly ErrorService _errorService;

        public string BackLink { get; set; }

        [BindProperty(SupportsGet = true, Name = "trn")]
        [Display(Name = "TRN")]
        [Required]
        public string TRN { get; set; }

        [BindProperty(Name = "trust-name")]
        [Display(Name = "Trust Name")]
        public string TrustName { get; set; }

        [BindProperty(Name = "trust-type")]
        [Display(Name = "Trust Type")]
        public string TrustType { get; set; }

        [BindProperty(Name = "confirm-trust")]
        [Display(Name = "confirm-trust")]
        [Required(ErrorMessage = "Confirm that the trust displayed is correct.")]
        public string ConfirmTrust { get; set; }

        private readonly ICreateProjectCache _createProjectCache;

        public ConfirmTrustTaskModel(
            IGetTrustByRefService getTrustByRefService,
            ICreateProjectCache createProjectCache,
            ILogger<ConfirmTrustTaskModel> logger,
            ErrorService errorService)
        {
            _getTrustByRefService = getTrustByRefService;
            _createProjectCache = createProjectCache;
            _logger = logger;
            _errorService = errorService;
        }

        public async Task<IActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            try
            {
                var project = _createProjectCache.Get();

                var trust = await _getTrustByRefService.Execute(TRN);

                TRN = trust.Trust.TRN;
                TrustName = trust.Trust.TrustName;
                TrustType = trust.Trust.TrustType;

                BackLink = CreateProjectBackLinkHelper.GetBackLink(project.Navigation, RouteConstants.CreateProjectSearchTrust);

            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);

                var trust = await _getTrustByRefService.Execute(TRN);

                TRN = trust.Trust.TRN;
                TrustName = trust.Trust.TrustName;
                TrustType = trust.Trust.TrustType;

                return Page();
            }

            if (!bool.Parse(ConfirmTrust)) {
                return Redirect(RouteConstants.CreateProjectSearchTrust);
            }

            try
            {
                var project = _createProjectCache.Get();

                var trust = await _getTrustByRefService.Execute(TRN);

                project.TRN = trust.Trust.TRN;
                project.TrustName = trust.Trust.TrustName;

                _createProjectCache.Update(project);

                return Redirect(RouteConstants.CreateProjectCheckYourAnswers);
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }
    }
}
