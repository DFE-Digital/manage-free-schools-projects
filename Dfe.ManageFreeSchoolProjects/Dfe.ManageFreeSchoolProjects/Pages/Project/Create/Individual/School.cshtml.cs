using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class SchoolModel : PageModel
    {
        [BindProperty(Name = "school")]
        [Display(Name = "school name")]
        [Required(ErrorMessage = "Enter the current free school name.")]
        [SchoolNameValidator]
        public string School { get; set; }

        public string BackLink { get; set; }

        private readonly ErrorService _errorService;
        private readonly ICreateProjectCache _createProjectCache;

        public SchoolModel(ErrorService errorService, ICreateProjectCache createProjectCache)
        {
            _errorService = errorService;
            _createProjectCache = createProjectCache;
        }

        public IActionResult OnGet()
        {
            if (!User.IsInRole(RolesConstants.ProjectRecordCreator))
            {
                return new UnauthorizedResult();
            }
            BackLink = SetBackLink();

            var project = _createProjectCache.Get();
            School = project.SchoolName;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var project = _createProjectCache.Get();
            project.SchoolName = School;
            _createProjectCache.Update(project);

            return Redirect(RouteConstants.CreateProjectRegion);
        }

        private string SetBackLink()
        {
            var project = _createProjectCache.Get();
            if (project.Navigation == CreateProjectNavigation.BackToCheckYourAnswers)
            {
                return RouteConstants.CreateProjectCheckYourAnswers;
            }
            else
            {
                return RouteConstants.CreateProjectId;
            }
        }
    }
}
