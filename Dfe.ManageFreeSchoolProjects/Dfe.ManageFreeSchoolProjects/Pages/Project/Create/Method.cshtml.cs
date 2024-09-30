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
    public class MethodModel(ErrorService errorService, ICreateProjectCache createProjectCache) : CreateProjectBaseModel(createProjectCache)
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

            var nextPage = GetNextPage(projCache, chosenMethod);

            if (chosenMethod != projCache.ProjectCreateMethod)
                CreateProjectCache.Delete();
            
            UpdateCacheWithCreateMethod(chosenMethod, projCache);
            
            return nextPage;
        }

        private RedirectResult GetNextPage(CreateProjectCacheItem projCache, ProjectCreateMethod chosenMethod)
        {
            var centralRouteNextPage = GetCentralRouteNextPage(projCache, chosenMethod);
            
            return chosenMethod switch
            {
                ProjectCreateMethod.PresumptionRoute => Redirect(projCache.ReachedCheckYourAnswers
                    ? RouteConstants.CreateProjectCheckYourAnswers
                    : RouteConstants.CreateProjectId),

                ProjectCreateMethod.CentralRoute => Redirect(centralRouteNextPage),

                _ => throw new InvalidOperationException($"Unrecognized method {chosenMethod}")
            };
        }
        
        private static string GetCentralRouteNextPage(CreateProjectCacheItem cache, ProjectCreateMethod selectedMethod)
        {
            // If we reached "Check Your Answers" and the method changed, go to application number page
            if (cache.ReachedCheckYourAnswers)
            {
                return cache.ProjectCreateMethod != selectedMethod 
                    ? RouteConstants.CreateApplicationNumber 
                    : RouteConstants.CreateProjectCheckYourAnswers;
            }

            return RouteConstants.CreateApplicationNumber;
        }

        private void UpdateCacheWithCreateMethod(ProjectCreateMethod method, CreateProjectCacheItem projCache)
        {
            projCache.ProjectCreateMethod = method;
            CreateProjectCache.Update(projCache);
        }
    }
}