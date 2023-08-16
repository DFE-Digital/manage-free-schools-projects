using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
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

        private ICreateProjectCache _createProjectCache;

        public RegionModel(ErrorService errorService, ICreateProjectCache createProjectCache)
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
            project.Region = (ProjectRegion)Enum.Parse(typeof(ProjectRegion), Region);
            _createProjectCache.Update(project);

            return Redirect("/project/create/localauthority");
        }
    }
}
