using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class CheckYourAnswersModel : PageModel
    {
        public CreateProjectCacheItem Project { get; set; }

        private readonly ICreateProjectCache _createProjectCache;
        private readonly ICreateProjectService _createProjectService;

        public CheckYourAnswersModel(ICreateProjectCache createProjectCache, ICreateProjectService createProjectService)
        {
            _createProjectCache = createProjectCache;
            _createProjectService = createProjectService;   
        }

        public IActionResult OnGet()
        {
            Project = _createProjectCache.Get();
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

            await _createProjectService.Execute(createProjectRequest);


            return Redirect(RouteConstants.CreateProjectConfirmation);

        }
    }
}
