using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Constituency;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.Constituency
{
    public class EditConstituencyModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "search")]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty]
        public string CurrentFreeSchoolName { get; set; }

        [BindProperty(Name = "constituency")]
        [Display(Name = "Constituency")]
        [Required]
        public string Constituency { get; set; }

        public string[] Labels { get; set; }
        public string[] Values { get; set; }
        public string[] Hints { get; set; }

		private const string None = "#None#";

		private readonly ILogger<ViewSchoolTask> _logger;
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly ISearchConstituency _searchConstituency;
        private readonly ErrorService _errorService;

        public EditConstituencyModel(
            IGetProjectByTaskService getProjectService,
            ISearchConstituency searchConstituency,
            ILogger<ViewSchoolTask> logger,
            ErrorService errorService)
        {
            _logger = logger;
            _errorService = errorService;
            _getProjectService = getProjectService;
            _searchConstituency = searchConstituency;
        }

        public async Task<ActionResult> OnGet()
		{
			_logger.LogMethodEntered();

			var project = await _getProjectService.Execute(ProjectId);

			CurrentFreeSchoolName = project.School.CurrentFreeSchoolName;

			await LoadPage();

			return Page();
		}

		private async System.Threading.Tasks.Task LoadPage()
		{
			var response = await _searchConstituency.Execute(SearchTerm);

			Labels = response.Data.Constituencies.Select(x => x.Name).Append("None of the above - I want to search again").ToArray();
			Values = response.Data.Constituencies.Select(x => x.Name).Append(None).ToArray();
			Hints = response.Data.Constituencies.Select(x => $"MP - {x.MPName}{Environment.NewLine}Political Party - {x.Party}").Append("").ToArray();
		}

		public async Task<ActionResult> OnPost()
        {
            _logger.LogMethodEntered();

			if (!ModelState.IsValid)
			{
				_errorService.AddErrors(ModelState.Keys, ModelState);
				await LoadPage();
				return Page();
			}

            if(Constituency == None)
            {
				return Redirect(string.Format(RouteConstants.SearchConstituency, ProjectId));
			}

			return Redirect(string.Format(RouteConstants.ViewConstituency, ProjectId));
		}

	}
}
