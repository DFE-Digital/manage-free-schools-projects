
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.Trust
{
    public class EditTrustTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditTrustTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public string CurrentFreeSchoolName { get; set; }

        [BindProperty(Name = "trn")]
        [Display(Name = "TRN")]
        [Required]
        public string TRN { get; set; }

        [BindProperty(Name = "trust-name")]
        [Display(Name = "Trust Name")]
        [Required]
        public string TrustName { get; set; }

        [BindProperty(Name = "trust-type")]
        [Display(Name = "Trust Type")]
        [Required]
        public string TrustType { get; set; }

        public EditTrustTaskModel(
            IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditTrustTaskModel> logger,
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
                var project = await _getProjectService.Execute(ProjectId);
                CurrentFreeSchoolName = project.School.CurrentFreeSchoolName;
                TRN = project.Trust.TRN;
                TrustName = project.Trust.TrustName;
                TrustType = project.Trust.TrustType;

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
                    Trust = new TrustTask()
                    {
                        TRN = TRN,
                        TrustName = TrustName,
                        TrustType = TrustType
                    }
                };

                await _updateProjectTaskService.Execute(ProjectId, request);

                return Redirect(string.Format(RouteConstants.ViewTrustTask, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }
    }
}
