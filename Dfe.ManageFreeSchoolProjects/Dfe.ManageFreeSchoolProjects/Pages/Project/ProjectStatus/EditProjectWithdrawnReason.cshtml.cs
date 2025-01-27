using System;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectStatusType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.ProjectStatus;
using ProjectCancelledReasonType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.ProjectCancelledReason;
using ProjectWithdrawnReasonType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.ProjectWithdrawnReason;
using Dfe.ManageFreeSchoolProjects.Models;
using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.Validators;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.ProjectWithdrawnReason
{
    public class EditProjectWithdrawnModel(
        IGetProjectOverviewService getProjectOverviewService,
        IUpdateProjectStatusService updateProjectStatusService,
        ILogger<EditProjectWithdrawnModel> logger,
        IHttpContextAccessor httpContextAccessor,
        ErrorService errorService)
        : PageModel
    {
        public const string WithdrawnYearId = "year-withdrawn";

        public ProjectOverviewResponse Project { get; set; }
        
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(SupportsGet = true, Name = "projectStatusId")]
        public int ProjectStatusId { get; set; }

        [Required]
        [BindProperty(Name = WithdrawnYearId, BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the project was withdrawn")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        public DateTime? WithdrawnYear { get; set; }

        [Required(ErrorMessage = "Enter the main reason for withdrawal")]
        [BindProperty(Name = "project-withdrawn-reason-type")]
        [Display(Name = "Main reason for withdrawal")]
        public ProjectWithdrawnReasonType ProjectWithdrawnReason { get; set; }

        [Required(ErrorMessage = "Enter whether the project was withdrawn as a result of the 2024/25 national review of pipeline projects")]
        [BindProperty(Name = "project-withdrawn-as-a-result-of-national-review-of-pipeline")]
        [Display(Name = "Project withdrawn as a result of the 2024/25 national review of pipeline projects")]
        public YesNo? ProjectWithdrawnAsAResultOfNationalPipelineReview { get; set; }

        [Required(ErrorMessage = "Enter the notes about the withdrawal")]
        [BindProperty(Name = "add-notes-about-the-withdrawal")]
        [Display(Name = "Notes about the withdrawal")]
        [ValidText(500)]
        public string Notes { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                ProjectStatusType ProjectStatus = (ProjectStatusType)ProjectStatusId;
                if (ProjectStatus != ProjectStatusType.WithdrawnInPreOpening && ProjectStatus != ProjectStatusType.WithdrawnDuringApplication)
                {
                    return new NotFoundResult();
                }

                Project = await getProjectOverviewService.Execute(ProjectId);
                WithdrawnYear = Project.ProjectStatus.ProjectWithdrawnDate;
                ProjectWithdrawnReason = Project.ProjectStatus.ProjectWithdrawnReason;
                ProjectWithdrawnAsAResultOfNationalPipelineReview = Project.ProjectStatus.ProjectWithdrawnDueToNationalReviewOfPipelineProjects;
                Notes = Project.ProjectStatus.CommentaryForWithdrawal;
            }
            catch (Exception ex)
            {
                logger.LogErrorMsg(ex);
            }

            TempData["projectStatusUpdated"] = false;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var yearFormatErrorMessage = "Enter a date in the correct format";

            if (ModelState.IsValid && WithdrawnYear == null)
            {
                ModelState.AddModelError(WithdrawnYearId, yearFormatErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
                Project = await getProjectOverviewService.Execute(ProjectId);

                return Page();
            }

            ProjectStatusType ProjectStatus = (ProjectStatusType)ProjectStatusId;
            
            if(ProjectStatus == ProjectStatusType.WithdrawnInPreOpening || ProjectStatus == ProjectStatusType.WithdrawnDuringApplication)
            {
                UpdateProjectStatusRequest request = new UpdateProjectStatusRequest()
                {
                    ProjectStatus = (ProjectStatusType)ProjectStatusId,

                    CancelledDate = null,
                    ProjectCancelledReason = ProjectCancelledReasonType.NotSet,
                    ProjectCancelledDueToNationalReviewOfPipelineProjects = null,
                    CommentaryForCancellation = null,

                    WithdrawnDate = WithdrawnYear,
                    ProjectWithdrawnReason = ProjectWithdrawnReason,
                    ProjectWithdrawnDueToNationalReviewOfPipelineProjects = ProjectWithdrawnAsAResultOfNationalPipelineReview,
                    CommentaryForWithdrawal = Notes,

                    ClosedDate = null
                };

                var projectId = RouteData.Values["projectId"] as string;

                await updateProjectStatusService.Execute(projectId, request);
                TempData["projectStatusUpdated"] = true;
                return Redirect(GetNextPage());
            }

           else
            {
                return new NotFoundResult();
            }
        }

       
        
        public string GetNextPage()
        {
            var referrerQuery = httpContextAccessor.HttpContext.Request.Query["referrer"];

            var isReferred = Enum.TryParse(referrerQuery, out Referrer referrer);

            if (referrer == Referrer.ProjectOverview)
            {
                return string.Format(RouteConstants.ProjectOverview, ProjectId);
            }
            
            else if (referrer == Referrer.TaskList)
            {
                return string.Format(RouteConstants.TaskList, ProjectId);
            }
            
            else if (referrer == Referrer.ContactsOverview)
            {
                return string.Format(RouteConstants.Contacts, ProjectId);
            }

            if (!isReferred)
            {
                return string.Format(RouteConstants.ProjectOverview, ProjectId);
            }

            return string.Format(RouteConstants.ProjectOverview, ProjectId);
        }
    }
}
