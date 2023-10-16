using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    [Authorize(Roles = RolesConstants.ProjectRecordCreator)]
    public class ProjectIdModel : PageModel
    {
        [BindProperty(Name = "projectid")]
        [Display(Name = "temporary project id")]
        [Required]
        [StringLength(25, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string ProjectId { get; set; }

        public string BackLink { get; set; }

        private readonly ErrorService _errorService;
        private readonly ICreateProjectCache _createProjectCache;
        private readonly IGetProjectOverviewService _getProjectOverviewService;

        public ProjectIdModel(ErrorService errorService, ICreateProjectCache createProjectCache, IGetProjectOverviewService getProjectOverviewService)
        {
            _errorService = errorService;
            _createProjectCache = createProjectCache;
            _getProjectOverviewService = getProjectOverviewService;
        }

        public IActionResult OnGet()
        {
            var project = _createProjectCache.Get();
            ProjectId = project.ProjectId;
            SetBackLink(project);
            return Page();
        }

        private void SetBackLink(CreateProjectCacheItem project)
        {
            if (project.Navigation == CreateProjectNavigation.BackToCheckYourAnswers)
            {
                BackLink = RouteConstants.CreateProjectCheckYourAnswers;
            }
            else
            {
                BackLink = RouteConstants.CreateProjectMethod;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            if (ProjectId.Contains(" "))
            {
                ModelState.AddModelError("projectid", "Temporary project ID must not include spaces");
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            if (Regex.Match(ProjectId, "[^a-zA-Z\\d\\s:]", RegexOptions.None, TimeSpan.FromSeconds(5)).Success)
            {
                ModelState.AddModelError("projectid", "Temporary project ID must only include numbers and letters");
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            try
            {
                var a = await _getProjectOverviewService.Execute(ProjectId);
                ModelState.AddModelError("projectid", "Project Id already exists");
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    throw;
                }
            }

            var project = _createProjectCache.Get();
            project.ProjectId = ProjectId;
            _createProjectCache.Update(project);

            return Redirect(RouteConstants.CreateProjectSchool);
        }
    }
}
