using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class MethodModel(ErrorService errorService, ICreateProjectCache createProjectCache)
        : CreateProjectBaseModel(createProjectCache)
    {
        [BindProperty(Name = "method")]
        [Display(Name = "method")]
        [Required(ErrorMessage = "Select what you want to do")]
        public string Method { get; set; }

        public IActionResult OnGet()
        {
            if (!User.IsInRole(RolesConstants.ProjectRecordCreator))
            {
                return new UnauthorizedResult();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var chosenMethod = (ProjectCreateMethod)Enum.Parse(typeof(ProjectCreateMethod), Method);

            var projCache = CreateProjectCache.Get();
            
            return GetNextPage(projCache, chosenMethod);
        }

        private RedirectResult GetNextPage(CreateProjectCacheItem projCache, ProjectCreateMethod chosenMethod)
        {
            //TODO: is deleting cache needed here?
            CreateProjectCache.Delete();
            
            return chosenMethod switch
            {
                ProjectCreateMethod.PresumptionRoute => Redirect(projCache.ReachedCheckYourAnswers ? RouteConstants.CreateProjectCheckYourAnswers : RouteConstants.CreateProjectId),
                ProjectCreateMethod.CentralRoute => Redirect(projCache.ReachedCheckYourAnswers ? RouteConstants.CreateApplicationNumber : RouteConstants.CreateProjectCheckYourAnswers),
                _ => throw new InvalidOperationException($"Unrecognised method {chosenMethod}")
            };
        }

        private void DeleteCacheIfProjectMethodNull(CreateProjectCacheItem projCache)
        {
            if (projCache.ProjectCreateMethod == ProjectCreateMethod.NotSet)
                return;

            CreateProjectCache.Delete();
        }

        private void UpdateCacheWithCreateMethod(ProjectCreateMethod method, CreateProjectCacheItem projCache)
        {
            projCache.ProjectCreateMethod = method;
            CreateProjectCache.Update(projCache);
        }
    }
}