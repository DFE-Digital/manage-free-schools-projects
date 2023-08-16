using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class RegionModel : PageModel
    {
        [BindProperty(Name = "region")]
        [Display(Name = "region")]
        [Required]
        public string? Region { get; set; }

        private ErrorService _errorService;

        public RegionModel(ErrorService errorService)
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

            return Redirect("/project/create/localauthority");
        }
    }
}
