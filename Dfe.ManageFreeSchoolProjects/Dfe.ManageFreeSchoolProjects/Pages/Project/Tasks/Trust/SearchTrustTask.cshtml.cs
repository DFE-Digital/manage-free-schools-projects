using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Trust;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.EMMA;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.Trust
{
    public class SearchTrustTaskModel : PageModel
    {
        private readonly ILogger<SearchTrustTaskModel> _logger;
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IGetTrustByRefService _getTrustByRefService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public string CurrentFreeSchoolName { get; set; }

        [BindProperty(Name = "trn")]
        [Display(Name = "TRN (trust reference number)")]
        [Required]
        public string TRN { get; set; }

        public GetTrustByRefResponse Trust { get; set; }

        public SearchTrustTaskModel(
            IGetProjectByTaskService getProjectService,
            IGetTrustByRefService getTrustByRefService,
            ILogger<SearchTrustTaskModel> logger)
        {
            _logger = logger;
            _getProjectService = getProjectService;
            _getTrustByRefService = getTrustByRefService;
        }

        public async Task<IActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            try
            {
                var project = await _getProjectService.Execute(ProjectId);
                CurrentFreeSchoolName = project.School.CurrentFreeSchoolName;

            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            _logger.LogMethodEntered();

            Trust = await _getTrustByRefService.Execute(TRN);

            return Redirect(string.Format(RouteConstants.EditTrustTask, ProjectId, TRN));
        }

    }
}