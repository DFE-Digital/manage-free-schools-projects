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
using Dfe.ManageFreeSchoolProjects.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.ProjectStatus
{
    public class EditProjectStatusModel(
        IGetProjectOverviewService getProjectOverviewService,
        IUpdateProjectStatusService updateProjectStatusService,
        ILogger<EditProjectStatusModel> logger,
        IHttpContextAccessor httpContextAccessor,
        ErrorService errorService)
        : PageModel
    {
        public const string CancelledYearId = "year-cancelled";
        public const string ClosedYearId = "year-closed";
        public const string WithdrawnPreopeningYearId = "year-withdrawn-preopening";
        public const string WithdrawnApplicationYearId = "year-withdrawn-application";

        public ProjectOverviewResponse Project { get; set; }
        
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }
        
        [BindProperty(Name = "project-status")]
        public ProjectStatusType ProjectStatus { get; set; }

        [BindProperty(Name = ClosedYearId, BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the school was closed")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        public DateTime? ClosedYear { get; set; }

        public ProjectStatusReason ProjectStatusReason { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var projectId = RouteData.Values["projectId"] as string;

                Project = await getProjectOverviewService.Execute(projectId);
                ProjectStatus = Project.ProjectStatus.ProjectStatus;
                
                if (Project.ProjectStatus.ProjectStatus == ProjectStatusType.Closed)
                    ClosedYear = Project.ProjectStatus.ProjectClosedDate;

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
            CheckErrors(ClosedYearId, ProjectStatusType.Closed, ClosedYear);

            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
                Project = await getProjectOverviewService.Execute(ProjectId);

                return Page();
            }

            var projectId = RouteData.Values["projectId"] as string;
            int projectStatusIndex = (int)ProjectStatus;

            if (ProjectStatus == ProjectStatusType.Cancelled || ProjectStatus == ProjectStatusType.WithdrawnInPreOpening || ProjectStatus == ProjectStatusType.WithdrawnDuringApplication)
            {
                return Redirect(string.Format(RouteConstants.EditProjectStatusDetails, projectId, projectStatusIndex.ToString()));
            }
            UpdateProjectStatusRequest request = new UpdateProjectStatusRequest()
            {
                ProjectStatus = ProjectStatus,
                ClosedDate = ClosedYear,
            };

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

        private void CheckErrors(string id, ProjectStatusType status, DateTime? year)
        {
            var yearFormatErrorMessage = "Enter a date in the correct format";

            if (ModelState.IsValid && ProjectStatus == status && (year == null))
            {
                ModelState.AddModelError(id, yearFormatErrorMessage);
            }

            if (ProjectStatus != status)
            {
                ModelState.Keys.Where(errorKey => errorKey.StartsWith(id)).ToList()
                    .ForEach(errorKey => ModelState.Remove(errorKey));
            }

        }
    }
}
