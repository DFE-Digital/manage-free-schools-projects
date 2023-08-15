using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class LocalAuthorityModel : PageModel
    {
        [BindProperty(Name = "local-authority")]
        [Required]
        public string? LocalAuthority { get; set; }

        private ErrorService _errorService;

        public LocalAuthorityModel(ErrorService errorService)
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

            return Redirect("/project/create/confirmation");
        }
    }
}
