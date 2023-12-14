using System;
using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dfe.ManageFreeSchoolProjects.Extensions;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    public class SchoolPhaseModel : CreateProjectBaseModel
    {
        [BindProperty(Name = "school-phase")]
        [Display(Name = "school-phase")]
        [Required(ErrorMessage = "Select the school phase")]
        public string SchoolPhase { get; set; }
        
        private readonly ErrorService _errorService;

        private readonly ICreateProjectCache _createProjectCache;

        public SchoolPhaseModel(ErrorService errorService, ICreateProjectCache createProjectCache)
        {
            _errorService = errorService;
            _createProjectCache = createProjectCache;
        }

        public IActionResult OnGet()
        {
            if (!IsUserAuthorised())
            {
                return new UnauthorizedResult();
            }


            var project = _createProjectCache.Get();
            BackLink = BackLink = GetPreviousPage(CreateProjectPageName.SchoolPhase, project.Navigation, project.TRN);

            if (project.SchoolPhase != 0)
                SchoolPhase = project.SchoolPhase.ToString();

            return Page();
        }

        public IActionResult OnPost()
        {
            var project = _createProjectCache.Get();
            BackLink = BackLink = GetPreviousPage(CreateProjectPageName.SchoolPhase, project.Navigation, project.TRN);

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            project.SchoolPhase = (SchoolPhase)Enum.Parse(typeof(SchoolPhase), SchoolPhase);

            _createProjectCache.Update(project);

            return Redirect(GetNextPage(CreateProjectPageName.SchoolPhase));
        }
    }
}