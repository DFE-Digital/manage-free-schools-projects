using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Services;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class MethodModel : PageModel
    {
        [BindProperty(Name = "method")]
        [Display(Name = "method")]
        [Required]
        public string? Method { get; set; }

        private readonly ErrorService _errorService;

        public MethodModel(ErrorService errorService)
        {
            _errorService = errorService;
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

            var chosenMethod = (ProjectCreateMethod)Enum.Parse(typeof(ProjectCreateMethod), Method);

            switch (chosenMethod)
            {
                case ProjectCreateMethod.Individual:
                    return Redirect("/project/create/school");
                case ProjectCreateMethod.Bulk:
                    return Redirect("/project/create/bulk");
                default:
                    throw new InvalidOperationException($"Unrecognised method {Method}");
            }
        }
    }
}
