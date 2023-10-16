using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    [Authorize(Roles = RolesConstants.ProjectRecordCreator)]
    public class CheckYourAnswersModel : PageModel
    {
        public CreateProjectCacheItem Project { get; set; }

        private readonly ErrorService _errorService;
        private readonly ICreateProjectCache _createProjectCache;
        private readonly ICreateProjectService _createProjectService;

        public CheckYourAnswersModel(ErrorService errorService, ICreateProjectCache createProjectCache, ICreateProjectService createProjectService)
        {
            _createProjectCache = createProjectCache;
            _createProjectService = createProjectService;
            _errorService = errorService;
        }

        public IActionResult OnGet()
        {
            Project = _createProjectCache.Get();
            Project.Navigation = CreateProjectNavigation.BackToCheckYourAnswers;
            _createProjectCache.Update(Project);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            CreateProjectRequest createProjectRequest = new CreateProjectRequest();
            var project = _createProjectCache.Get();
            ProjectDetails projReq = new ProjectDetails
            {
                ProjectId = project.ProjectId,
                SchoolName = project.SchoolName,
                CreatedBy = User.Identity.Name,
            };

            createProjectRequest.Projects.Add(projReq);

            try
            {
                await _createProjectService.Execute(createProjectRequest);
            }
            catch(HttpRequestException e)
            {
                if(e.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
                {
                    _errorService.AddError("projectid", $"Project with ID {project.ProjectId} already exists");
                    Project = project;
                    return Page();
                }
                else
                {
                    throw;
                }
            }

            return Redirect(RouteConstants.CreateProjectConfirmation);

        }
    }
}
