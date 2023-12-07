using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;
using Dfe.ManageFreeSchoolProjects.Utils;
using System;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class ProvisionalOpeningDateModel : CreateProjectBaseModel
    {
        [BindProperty(Name = "provisional-opening-date", BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Provisional opening date")]
        [DateValidation(DateRangeValidationService.DateRange.Future)]
        [Required(ErrorMessage = "Enter the provisional opening date.")]
        public DateTime? ProvisionalOpeningDate { get; set; }
        
        private readonly ErrorService _errorService;
        private readonly ICreateProjectCache _createProjectCache;

        public ProvisionalOpeningDateModel(ErrorService errorService, ICreateProjectCache createProjectCache)
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
            ProvisionalOpeningDate = project.ProvisionalOpeningDate;
            BackLink = GetPreviousPage(CreateProjectPageName.ProvisionalOpeningDate, project.Navigation);

            return Page();
        }

        public IActionResult OnPost()
        {
            var project = _createProjectCache.Get();
            BackLink = GetPreviousPage(CreateProjectPageName.ProvisionalOpeningDate, project.Navigation);

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            project.ProvisionalOpeningDate = ProvisionalOpeningDate;
            _createProjectCache.Update(project);

            return Redirect(GetNextPage(CreateProjectPageName.ProvisionalOpeningDate));
        }
    }
}
