using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class SchoolModel : PageModel
    {
        [BindProperty(Name = "school")]
        [Display(Name = "free school name")]
        [Required]
        [StringLength(80, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string School { get; set; }

        private ErrorService _errorService;
        private ICreateProjectCache _createProjectCache;

        public SchoolModel(ErrorService errorService, ICreateProjectCache createProjectCache)
        {
            _errorService = errorService;
            _createProjectCache = createProjectCache;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var project = _createProjectCache.Get();
            project.School = School;
            _createProjectCache.Update(project);

            return Redirect("/project/create/region");
        }
    }
}
