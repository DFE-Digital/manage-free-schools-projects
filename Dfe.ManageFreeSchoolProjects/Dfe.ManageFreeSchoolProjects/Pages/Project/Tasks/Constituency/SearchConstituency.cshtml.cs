using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.Constituency
{
    public class SearchConstituencyModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name="search-constituency")]
        [Display(Name = "search constituency")]
        [Required(ErrorMessage = "Enter a name or postcode. For example, South London or W1A 1AA")]
        [StringLength(50, ErrorMessage = "Name or postcode must be 50 characters or less.")]
        public string SearchConstituency { get; set; }

        [BindProperty]
        public string CurrentFreeSchoolName { get; set; }

        private readonly ILogger<ViewSchoolTask> _logger;
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly ErrorService _errorService;

        public SearchConstituencyModel(
            IGetProjectByTaskService getProjectService,
            ILogger<ViewSchoolTask> logger,
            ErrorService errorService)
        {
            _logger = logger;
            _errorService = errorService;
            _getProjectService = getProjectService;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            var project = await _getProjectService.Execute(ProjectId);

            CurrentFreeSchoolName = project.School.CurrentFreeSchoolName;

            return Page();
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            return Redirect(string.Format(RouteConstants.EditConstituency, ProjectId, SearchConstituency));
        }
    }
}
