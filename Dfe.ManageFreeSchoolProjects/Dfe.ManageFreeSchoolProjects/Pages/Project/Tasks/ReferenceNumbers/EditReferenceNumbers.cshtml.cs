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
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.ReferenceNumbers
{
    public class EditReferenceNumbersTaskModel : PageModel
    {

        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IGetProjectOverviewService _getProjectOverviewService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditReferenceNumbersTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectID")]
        public string ProjectIdToUpdate { get; set; }

        [BindProperty(Name = "project-id")]
        [Display(Name = "Project ID")]
        [Required(ErrorMessage = "Enter the project ID")]
        [StringLength(25, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string ProjectId { get; set; }

        [BindProperty(Name = "urn")]
        public string Urn { get; set; }

        public string SchoolName { get; set; }

        public EditReferenceNumbersTaskModel(
            IGetProjectByTaskService getProjectService,
            IGetProjectOverviewService getProjectOverviewService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditReferenceNumbersTaskModel> logger,
            ErrorService errorService)
        {
            _getProjectService = getProjectService;
            _getProjectOverviewService = getProjectOverviewService;
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
            var project = await _getProjectService.Execute(ProjectIdToUpdate, TaskName.ReferenceNumbers);
            SchoolName = project.SchoolName;

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            if (ProjectId != ProjectIdToUpdate)
            {

                if (ProjectId.Contains(' '))
                {
                    ModelState.AddModelError("project-id", "Project ID must not include spaces");
                    _errorService.AddErrors(ModelState.Keys, ModelState);
                    return Page();
                }

                if (Regex.Match(ProjectId, "[^a-zA-Z\\d\\s:]", RegexOptions.None, TimeSpan.FromSeconds(5)).Success)
                {
                    ModelState.AddModelError("project-id", "Project ID must only include numbers and letters");
                    _errorService.AddErrors(ModelState.Keys, ModelState);
                    return Page();
                }

                try
                {
                    //Attempt to get project, will throw an exception when 404 is returned
                    await _getProjectOverviewService.Execute(ProjectId);
                    ModelState.AddModelError("project-id", "Project ID already exists");
                    _errorService.AddErrors(ModelState.Keys, ModelState);
                    return Page();
                }
                catch (HttpRequestException ex)
                {
                    if (ex.StatusCode != System.Net.HttpStatusCode.NotFound)
                    {
                        throw;
                    }
                }
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
