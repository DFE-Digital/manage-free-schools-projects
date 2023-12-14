using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    public class ClassTypeModel : CreateProjectBaseModel
    {
        private readonly ErrorService _errorService;
        private readonly ICreateProjectCache _createProjectCache;
        
        [BindProperty(Name = "nursery")]
        [Display(Name = "Nursery")]
        [Required(ErrorMessage = "Select yes if it will have a nursery")]
        public ClassType.Nursery Nursery { get; set; }
        
        [BindProperty(Name = "sixth-form")]
        [Display(Name = "Sixth form")]
        [Required(ErrorMessage = "Select yes if it will have a sixth form")]
        public ClassType.SixthForm SixthForm { get; set; }
        
        public ClassTypeModel(ErrorService errorService, ICreateProjectCache createProjectCache)
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
            Nursery = project.Nursery;
            SixthForm = project.SixthForm;
            
            BackLink = GetPreviousPage(CreateProjectPageName.ClassType, project.Navigation);
            
            return Page();
        }

        public IActionResult OnPost()
        {
            var project = _createProjectCache.Get();
            BackLink = GetPreviousPage(CreateProjectPageName.ClassType, project.Navigation);

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            project.Nursery = Nursery;
            project.SixthForm = SixthForm;

            _createProjectCache.Update(project);

            return Redirect(GetNextPage(CreateProjectPageName.ClassType));
        }
    }
}
