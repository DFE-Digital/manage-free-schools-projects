using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.DraftGovernancePlan
{
    public class EditDraftGovernancePlanModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditDraftGovernancePlanModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "forecast-date", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Forecast date")]
        public DateTime? ForecastDate { get; set; }

        [BindProperty(Name = "actual-date", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Actual date")]
        public DateTime? ActualDate { get; set; }

        [BindProperty(Name = "comments")]
        [DisplayName("Comments on decision to approve (if applicable)")]
        [ValidText(999)]
        public string CommentsOnDecisionToApprove { get; set; }

        [BindProperty(Name = "sharepoint-link")]
        [DisplayName("SharePoint link")]
        [StringLength(500, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string SharePointLink { get; set; }

        [BindProperty]
        public string SchoolName { get; set; }

        public EditDraftGovernancePlanModel(IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditDraftGovernancePlanModel> logger,
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

            await LoadProject();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var updateTaskRequest = new UpdateProjectByTaskRequest()
            {
                DraftGovernancePlan = new()
                {
                    ForecastDate = ForecastDate,
                    ActualDate = ActualDate,
                    CommentsOnDecisionToApprove = CommentsOnDecisionToApprove,
                    SharepointLink = SharePointLink
                }
            };

            await _updateProjectTaskService.Execute(ProjectId, updateTaskRequest);

            return Redirect(string.Format(RouteConstants.ViewDraftGovernancePlanTask, ProjectId));
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.DraftGovernancePlan);

            ForecastDate = project.DraftGovernancePlan.ForecastDate;
            ActualDate = project.DraftGovernancePlan.ActualDate;
            CommentsOnDecisionToApprove = project.DraftGovernancePlan.CommentsOnDecisionToApprove;
            SharePointLink = project.DraftGovernancePlan.SharepointLink;

            SchoolName = project.SchoolName;
        }
    }
}
