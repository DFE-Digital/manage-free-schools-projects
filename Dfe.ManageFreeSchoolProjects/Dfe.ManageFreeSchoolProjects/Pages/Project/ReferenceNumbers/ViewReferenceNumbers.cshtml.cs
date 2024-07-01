using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.ReferenceNumbers;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.ReferenceNumbers
{
    public class ViewReferenceNumbersModel : PageModel
    {

        private readonly IGetProjectReferenceNumbersService _getProjectReferenceNumbersService;
        private readonly ILogger<ViewReferenceNumbersModel> _logger;

        [BindProperty(SupportsGet = true, Name = "projectID")]
        public string ProjectId { get ; set; }
        
        public GetProjectReferenceNumbersResponse ReferenceNumbers { get; set; }

        public ViewReferenceNumbersModel(
            IGetProjectReferenceNumbersService getProjectReferenceNumbersService,
            ILogger<ViewReferenceNumbersModel> logger)
        {
            _getProjectReferenceNumbersService = getProjectReferenceNumbersService;
            _logger = logger;
        }
        public async Task<IActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            ReferenceNumbers = await _getProjectReferenceNumbersService.Execute(ProjectId);

            return Page();
        }
    }
}
