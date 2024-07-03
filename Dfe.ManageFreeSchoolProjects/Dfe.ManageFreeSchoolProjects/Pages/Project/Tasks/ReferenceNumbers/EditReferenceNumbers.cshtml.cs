using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.ReferenceNumbers
{
    public class EditReferenceNumbersTaskModel : PageModel
    {

        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditReferenceNumbersTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectID")]
        public string ProjectIdToUpdate { get; set; }

        [BindProperty(Name = "project-id")]
        [Required(ErrorMessage = "Enter the project ID")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "urn")]
        public string Urn { get; set; }

        public string SchoolName { get; set; }

        public EditReferenceNumbersTaskModel(
            IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditReferenceNumbersTaskModel> logger,
            ErrorService errorService)
        {
            _getProjectService = getProjectService;
            _updateProjectTaskService = updateProjectTaskService;
            _logger = logger;
            _errorService = errorService;
        }
        public async Task<IActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            try
            {

                var project = await _getProjectService.Execute(ProjectIdToUpdate, TaskName.ReferenceNumbers);

                ProjectId = project.ReferenceNumbers.ProjectId;
                Urn = project.ReferenceNumbers.Urn;
                SchoolName = project.SchoolName;
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            try
            {
                var request = new UpdateProjectByTaskRequest()
                {
                    ReferenceNumbers = CreateUpdatedReferenceNumbersTask()
                };

                await _updateProjectTaskService.Execute(ProjectIdToUpdate, request);

                return Redirect(string.Format(RouteConstants.ViewReferenceNumbers, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }
        private ReferenceNumbersTask CreateUpdatedReferenceNumbersTask()
        {
            return new ReferenceNumbersTask
            {
                ProjectId = ProjectId,
                Urn = Urn,

            };
        }
    }
}
