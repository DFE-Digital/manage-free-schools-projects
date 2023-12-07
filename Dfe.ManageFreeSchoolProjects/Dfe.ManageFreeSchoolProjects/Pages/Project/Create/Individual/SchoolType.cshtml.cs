using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Extensions;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    public class SchoolTypeModel : CreateProjectBaseModel
    {
        private readonly ErrorService _errorService;
        private readonly ICreateProjectCache _createProjectCache;

        [BindProperty(Name = "school-type")]
        [Display(Name = "School type")]
        [Required(ErrorMessage = "Select school type")]
        public string SchoolType { get; set; }
        
        public SchoolTypeModel(ErrorService errorService, ICreateProjectCache createProjectCache)
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
            BackLink = GetPreviousPage(CreateProjectPageName.SchoolType, project.Navigation, project.TRN);

            SchoolType = project.SchoolType.ToIntString();

            return Page();
        }

        public IActionResult OnPost()
        {
            var project = _createProjectCache.Get();
            BackLink = GetPreviousPage(CreateProjectPageName.SchoolType, project.Navigation, project.TRN);

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            project.SchoolType = (SchoolType)Enum.Parse(typeof(SchoolType), SchoolType);

            _createProjectCache.Update(project);

            return Redirect(GetNextPage(CreateProjectPageName.SchoolType));
        }
    }
}
