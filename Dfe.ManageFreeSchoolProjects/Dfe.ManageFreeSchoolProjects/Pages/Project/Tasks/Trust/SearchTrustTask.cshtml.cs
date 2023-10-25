using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Task;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.Trust
{
    public class SearchTrustTaskModel : PageModel
    {
        private readonly ILogger<SearchTrustTaskModel> _logger;
        private readonly IGetTrustByRefService _getTrustByRefService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public string CurrentFreeSchoolName { get; set; }

        public string TRN { get; set; }

        public GetTrustByRefResponse Trust { get; set; }

        public SearchTrustTaskModel(
            IGetTrustByRefService getTrustByRefService,
            ILogger<SearchTrustTaskModel> logger)
        {
            _logger = logger;
            _getTrustByRefService = getTrustByRefService;
        }

        public async Task<IActionResult> OnPost()
        {
            _logger.LogMethodEntered();

            Trust = await _getTrustByRefService.Execute(TRN);

            return Redirect(string.Format(RouteConstants.EditTrustTask, TRN));
        }

    }
}