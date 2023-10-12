using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    public class ProjectIdModel : PageModel
    {
        [BindProperty(Name = "projectid")]
        [Display(Name = "temporary project id")]
        [Required]
        [StringLength(25, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string ProjectId { get; set; }

        private readonly ErrorService _errorService;
        private readonly ICreateProjectCache _createProjectCache;

        public ProjectIdModel(ErrorService errorService, ICreateProjectCache createProjectCache)
        {
            _errorService = errorService;
            _createProjectCache = createProjectCache;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
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

            var project = _createProjectCache.Get();
            project.ProjectId = ProjectId;
            _createProjectCache.Update(project);

            return Redirect(RouteConstants.CreateProjectSchool);
        }
    }
}
