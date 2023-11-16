using System;
using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class SchoolPhaseModel : PageModel
    {
        [BindProperty(Name = "school-phase")]
        [Display(Name = "school-phase")]
        [Required(ErrorMessage = "Select the school phase.")]
        public string SchoolPhase { get; set; }

        public string BackLink { get; set; }

        private readonly ErrorService _errorService;

        private readonly ICreateProjectCache _createProjectCache;

        public SchoolPhaseModel(ErrorService errorService, ICreateProjectCache createProjectCache)
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

            if (project.SchoolPhase != 0)
                SchoolPhase = _createProjectCache.Get().SchoolPhase.ToString();

            BackLink = CreateProjectBackLinkHelper.GetBackLink(project.Navigation, RouteConstants.CreateProjectSchool); 
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
            project.SchoolPhase = (SchoolPhase)Enum.Parse(typeof(SchoolPhase), SchoolPhase);
            _createProjectCache.Update(project);

            return Redirect(RouteConstants.CreateProjectCheckYourAnswers);
        }
    }
}