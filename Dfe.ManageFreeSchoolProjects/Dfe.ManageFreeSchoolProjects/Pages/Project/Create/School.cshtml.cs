using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
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

        public SchoolModel(ErrorService errorService)
        {
            _errorService = errorService;
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

            return Redirect("/project/create/region");
        }
    }
}
