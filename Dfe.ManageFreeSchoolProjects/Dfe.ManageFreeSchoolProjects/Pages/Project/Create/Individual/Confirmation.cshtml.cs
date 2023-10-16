using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    [Authorize(Roles = RolesConstants.ProjectRecordCreator)]
    public class ConfirmationModel : PageModel
    {
        private readonly ICreateProjectCache _createProjectCache;

        public string ProjectID { get; set; }

        public ConfirmationModel(ICreateProjectCache createProjectCache)
        {
            _createProjectCache = createProjectCache;
        }

        public void OnGet()
        {
            ProjectID = _createProjectCache.Get().ProjectId;
        }
    }
}
