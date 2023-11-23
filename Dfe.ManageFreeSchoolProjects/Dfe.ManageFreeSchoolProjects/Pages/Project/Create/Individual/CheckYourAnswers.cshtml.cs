using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Extensions;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class CheckYourAnswersModel : PageModel
    {
        public CreateProjectCacheItem Project { get; set; }

        private readonly ErrorService _errorService;
        private readonly ICreateProjectCache _createProjectCache;
        private readonly ICreateProjectService _createProjectService;
        private readonly MfspApiClient _mfspApiClient;

        public CheckYourAnswersModel(ErrorService errorService, ICreateProjectCache createProjectCache, ICreateProjectService createProjectService, MfspApiClient mfspApiClient)
        {
            _createProjectCache = createProjectCache;
            _createProjectService = createProjectService;
            _mfspApiClient = mfspApiClient;
            _errorService = errorService;
        }

        public IActionResult OnGet()
        {
            if (!User.IsInRole(RolesConstants.ProjectRecordCreator))
            {
                return new UnauthorizedResult();
            }
            Project = _createProjectCache.Get();
            Project.Navigation = CreateProjectNavigation.BackToCheckYourAnswers;
            _createProjectCache.Update(Project);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var createProjectRequest = new CreateProjectRequest();
            var project = _createProjectCache.Get();
            
            var projReq = new ProjectDetails
            {
                ProjectId = project.ProjectId,
                SchoolName = project.SchoolName,
                SchoolType = project.SchoolType,
                SchoolPhase = project.SchoolPhase,
                CreatedBy = User.Identity.Name,
                LocalAuthority = project.LocalAuthority,
                LocalAuthorityCode = project.LocalAuthorityCode,
                Region = project.Region.ToDescription(),
                TRN = project.TRN,
                TrustName = project.TrustName,
                Nursery = project.Nursery, 
                SixthForm = project.SixthForm
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

            var emailToNotify = _createProjectCache.Get().EmailToNotify;
            await _mfspApiClient.Post<string, string>("/api/v1.0/email", emailToNotify);

            return Redirect(RouteConstants.CreateProjectConfirmation);

        }
    }
}
