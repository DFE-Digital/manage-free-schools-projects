using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    public class ClassTypeModel : CreateProjectBaseModel
    {
        private readonly ErrorService _errorService;
        private readonly ICreateProjectCache _createProjectCache;
        
        [BindProperty(Name = "nursery")]
        [Display(Name = "Nursery")]
        [Required(ErrorMessage = "Select yes if it will have a nursery.")]
        public string Nursery { get; set; }
        
        [BindProperty(Name = "sixth-form")]
        [Display(Name = "Sixth form")]
        [Required(ErrorMessage = "Select yes if it will have a sixth form.")]
        public string SixthForm { get; set; }

        public string BackLink { get; set; }

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
            
            BackLink = GetPreviousPage(CreateProjectPageName.SchoolType, project.Navigation);
            
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
            project.SchoolType = (SchoolType)Enum.Parse(typeof(SchoolType), Nursery);

            _createProjectCache.Update(project);

            return Redirect(GetNextPage(CreateProjectPageName.SchoolType));
        }
    }
}
