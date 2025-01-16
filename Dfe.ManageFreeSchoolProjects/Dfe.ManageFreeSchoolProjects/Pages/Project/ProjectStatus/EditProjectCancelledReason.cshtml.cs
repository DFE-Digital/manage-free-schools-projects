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
using Dfe.ManageFreeSchoolProjects.Extensions;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.ProjectCancelledReason
{
    public class EditProjectCancelledModel(
        IGetProjectOverviewService getProjectOverviewService,
        IUpdateProjectStatusService updateProjectStatusService,
        ILogger<EditProjectCancelledModel> logger,
        IHttpContextAccessor httpContextAccessor,
        ErrorService errorService)
        : PageModel
    {
        public const string CancelledYearId = "year-cancelled";

        public ProjectOverviewResponse Project { get; set; }
        
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = CancelledYearId, BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the project was cancelled")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        public DateTime? CancelledYear { get; set; }

        [BindProperty(Name = "project-cancelled-reason-type")]
        public string ProjectCancelledReason { get; set; }

        [BindProperty(Name = "project-cancelled-as-a-result-of-national-review-of-pipeline")]
        public YesNo? ProjectCancelledAsAResultOfNationalPipelineReview { get; set; }

        [BindProperty(Name = "add-notes-about-the-cancellation")]
        public string Notes { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var projectId = RouteData.Values["projectId"] as string;

                Project = await getProjectOverviewService.Execute(projectId);
                CancelledYear = Project.ProjectStatus.ProjectCancelledDate;
                ProjectCancelledReason = Project.ProjectStatus.ProjectCancelledReason.ToIntString();
                ProjectCancelledAsAResultOfNationalPipelineReview = Project.ProjectStatus.ProjectCancelledDueToNationalReviewOfPipelineProjects;
                Notes = Project.ProjectStatus.CommentaryForCancellation;
            }
            catch (Exception ex)
            {
                logger.LogErrorMsg(ex);
            }

            ProjectId = Project.ProjectStatus.ProjectId;
            TempData["projectStatusUpdated"] = false;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var yearFormatErrorMessage = "Enter a date in the correct format";

            if (ModelState.IsValid && CancelledYear == null)
            {
                ModelState.AddModelError(CancelledYearId, yearFormatErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
                Project = await getProjectOverviewService.Execute(ProjectId);

                return Page();
            }

            UpdateProjectStatusRequest request = new UpdateProjectStatusRequest()
            {
                ProjectStatus = ProjectStatusType.Cancelled,

                CancelledDate = CancelledYear,
                ProjectCancelledReason = (ProjectCancelledReasonType)Enum.Parse(typeof(ProjectCancelledReasonType), ProjectCancelledReason ?? "NotSet"),
                ProjectCancelledDueToNationalReviewOfPipelineProjects = ProjectCancelledAsAResultOfNationalPipelineReview,
                CommentaryForCancellation = Notes,

                WithdrawnDate = null,
                ProjectWithdrawnReason = ProjectWithdrawnReasonType.NotSet,
                ProjectWithdrawnDueToNationalReviewOfPipelineProjects = null,
                CommentaryForWithdrawal = null,

                ClosedDate = null
            };

            var projectId = RouteData.Values["projectId"] as string;

            await updateProjectStatusService.Execute(projectId, request);
            TempData["projectStatusUpdated"] = true;
            return Redirect(GetNextPage());
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
