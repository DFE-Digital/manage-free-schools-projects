using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    public class SchoolModel : CreateProjectBaseModel
    {
        [BindProperty(Name = "school")]
        [Display(Name = "school name")]
        [Required(ErrorMessage = "Enter the current free school name.")]
        [ValidText(100, ErrorMessage = "test")]
        public string School { get; set; }
        
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

            var project = _createProjectCache.Get();
            School = project.SchoolName;
            BackLink = GetPreviousPage(CreateProjectPageName.SchoolName, project.Navigation);

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
    }
}
