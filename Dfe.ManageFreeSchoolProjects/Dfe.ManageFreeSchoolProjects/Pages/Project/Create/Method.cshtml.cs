using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class MethodModel(ErrorService errorService, ICreateProjectCache createProjectCache) : PageModel
    {
        [BindProperty(Name = "method")]
        [Display(Name = "method")]
        [Required(ErrorMessage = "Select what you want to do")]
        public string Method { get; set; }
        

        public IActionResult OnGet()
        {
            if(!User.IsInRole(RolesConstants.ProjectRecordCreator))
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
            
            switch (chosenMethod)
            {
                case ProjectCreateMethod.PresumptionRoute:
                    createProjectCache.Delete();
                    UpdateCacheWithCreateMethod(ProjectCreateMethod.PresumptionRoute);
                    return Redirect(RouteConstants.CreateProjectId);
                case ProjectCreateMethod.CentralRoute:
                    createProjectCache.Delete();
                    UpdateCacheWithCreateMethod(ProjectCreateMethod.CentralRoute);
                    return Redirect(RouteConstants.CreateApplicationNumber);
                default:
                    throw new InvalidOperationException($"Unrecognised method {Method}");
            }
        }
        
        private void UpdateCacheWithCreateMethod(ProjectCreateMethod method)
        {
            var projCache = createProjectCache.Get();
            projCache.ProjectCreateMethod = method;
            createProjectCache.Update(projCache);
        }
    }
}
