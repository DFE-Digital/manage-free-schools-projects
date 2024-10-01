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
        public ProjectCreateMethod Method { get; set; }

        [FromQuery(Name = "newProject")] 
        public bool? IsNewProject { get; set; }

        public IActionResult OnGet()
        {
            if (!User.IsInRole(RolesConstants.ProjectRecordCreator))
            {
                return new UnauthorizedResult();
            }

            if (IsNewProject != null && (bool)IsNewProject)
                CreateProjectCache.Delete();

            Method = CreateProjectCache.Get().ProjectCreateMethod;

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

            UpdateCacheWithCreateMethod(Method, projCache);

            return nextPage;
        }

        private RedirectResult GetNextPage(CreateProjectCacheItem projCache, ProjectCreateMethod chosenMethod)
        {
            var hasApplicationWaveOrApplicationNumber = !string.IsNullOrEmpty(projCache.ApplicationWave) ||
                                                        !string.IsNullOrEmpty(projCache.ApplicationNumber);

            switch (chosenMethod)
            {
                case ProjectCreateMethod.PresumptionRoute:
                    ClearCentralRouteFields(projCache);

                    return Redirect(projCache.ReachedCheckYourAnswers
                        ? RouteConstants.CreateProjectCheckYourAnswers
                        : RouteConstants.CreateProjectId);

                case ProjectCreateMethod.CentralRoute:
                    return Redirect(projCache.ReachedCheckYourAnswers && hasApplicationWaveOrApplicationNumber
                        ? RouteConstants.CreateProjectCheckYourAnswers
                        : RouteConstants.CreateApplicationNumber);
                default:
                    throw new InvalidOperationException($"Unrecognized method {chosenMethod}");
            }
        }

        private void ClearCentralRouteFields(CreateProjectCacheItem projCache)
        {
            projCache.ApplicationNumber = null;
            projCache.ApplicationWave = null;
            CreateProjectCache.Update(projCache);
        }

        private void UpdateCacheWithCreateMethod(ProjectCreateMethod method, CreateProjectCacheItem projCache)
        {
            projCache.ProjectCreateMethod = method;
            CreateProjectCache.Update(projCache);
        }
    }
}