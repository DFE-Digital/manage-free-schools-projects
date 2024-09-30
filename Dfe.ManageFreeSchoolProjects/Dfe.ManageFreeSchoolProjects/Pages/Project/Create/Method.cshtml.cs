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
        public ProjectCreateMethod Method { get; set; }
        
        public IActionResult OnGet()
        {
            if (!User.IsInRole(RolesConstants.ProjectRecordCreator))
            {
                return new UnauthorizedResult();
            }

            Method = CreateProjectCache.Get().ProjectCreateMethod;
            
            //TODO: remove, for testing only
            // CreateProjectCache.Delete();
            
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }
            
            var projCache = CreateProjectCache.Get();

            var nextPage = GetNextPage(projCache, Method);

            //TODO: when do we delete cache? 
            if (projCache.ProjectCreateMethod == ProjectCreateMethod.NotSet)
                CreateProjectCache.Delete();
            
            UpdateCacheWithCreateMethod(Method, projCache);
            
            return nextPage;
        }

        private RedirectResult GetNextPage(CreateProjectCacheItem projCache, ProjectCreateMethod chosenMethod)
        {
            var projectTypeChanged = projCache.ProjectCreateMethod != chosenMethod;
            
            return chosenMethod switch
            {
                ProjectCreateMethod.PresumptionRoute => Redirect(
                    projCache.ReachedCheckYourAnswers 
                        ? RouteConstants.CreateProjectCheckYourAnswers 
                        : RouteConstants.CreateProjectId),

                ProjectCreateMethod.CentralRoute => Redirect(
                    projCache.ReachedCheckYourAnswers && projectTypeChanged
                        ? RouteConstants.CreateApplicationNumber 
                        : RouteConstants.CreateProjectCheckYourAnswers),

                _ => throw new InvalidOperationException($"Unrecognized method {chosenMethod}")
            };
        }
        
        private void UpdateCacheWithCreateMethod(ProjectCreateMethod method, CreateProjectCacheItem projCache)
        {
            projCache.ProjectCreateMethod = method;
            CreateProjectCache.Update(projCache);
        }
    }
}