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
    public class EditProjectStatusModel : PageModel
    {
        public const string CancelledYearId = "year-cancelled";
        public const string ClosedYearId = "year-closed";
        public const string WithdrawnPreopeningYearId = "year-withdrawn-preopening";
        public const string WithdrawnApplicationYearId = "year-withdrawn-application";

        private readonly IGetProjectOverviewService _getProjectOverviewService;
        private readonly IUpdateProjectStatusService _updateProjectStatusService;
        private readonly ILogger<EditProjectStatusModel> _logger;
        private readonly ErrorService _errorService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProjectOverviewResponse Project { get; set; }
        
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public string CurrentFreeSchoolName { get; set; }

        [BindProperty(Name = "project-status")]
        public ProjectStatusType ProjectStatus { get; set; }

        [BindProperty(Name = CancelledYearId, BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the project was cancelled")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        public DateTime? CancelledYear { get; set; }

        [BindProperty(Name = ClosedYearId, BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the school was closed")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        public DateTime? ClosedYear { get; set; }

        [BindProperty(Name = WithdrawnPreopeningYearId, BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the project was withdrawn")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        public DateTime? WithdrawnYear { get; set; }

        [BindProperty(Name = WithdrawnApplicationYearId, BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the project was withdrawn")]
        [DateValidation(DateRangeValidationService.DateRange.PastOrFuture)]
        public DateTime? WithdrawnApplicationYear { get; set; }

        public EditProjectStatusModel(IGetProjectOverviewService getProjectOverviewService,
            IUpdateProjectStatusService updateProjectStatusService,
            ILogger<EditProjectStatusModel> logger,IHttpContextAccessor httpContextAccessor,
            ErrorService errorService)
        {
            _getProjectOverviewService = getProjectOverviewService;
            _updateProjectStatusService = updateProjectStatusService;
            _logger = logger;
            _errorService = errorService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var projectId = RouteData.Values["projectId"] as string;

                Project = await _getProjectOverviewService.Execute(projectId);
                ProjectStatus = Project.ProjectStatus.ProjectStatus;
                
                if (Project.ProjectStatus.ProjectStatus == ProjectStatusType.Closed)
                    ClosedYear = Project.ProjectStatus.ProjectClosedDate;
    
                if (Project.ProjectStatus.ProjectStatus == ProjectStatusType.Cancelled)
                    CancelledYear = Project.ProjectStatus.ProjectCancelledDate;
                
                if(Project.ProjectStatus.ProjectStatus == ProjectStatusType.WithdrawnDuringPreOpening)
                    WithdrawnYear = Project.ProjectStatus.ProjectWithdrawnDate;
                
                if (Project.ProjectStatus.ProjectStatus == ProjectStatusType.WithdrawnDuringApplication)
                    WithdrawnApplicationYear = Project.ProjectStatus.ProjectWithdrawnDate;
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
            }

            ProjectId = Project.ProjectStatus.ProjectId;
            TempData["projectStatusUpdated"] = false;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            CheckErrors(ClosedYearId, ProjectStatusType.Closed, ClosedYear);
            CheckErrors(CancelledYearId, ProjectStatusType.Cancelled, CancelledYear);
            CheckErrors(WithdrawnPreopeningYearId, ProjectStatusType.WithdrawnDuringPreOpening, WithdrawnYear);
            CheckErrors(WithdrawnApplicationYearId, ProjectStatusType.WithdrawnDuringApplication, WithdrawnApplicationYear);

            ClearNotApplicableValues();

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                Project = await _getProjectOverviewService.Execute(ProjectId);

                return Page();
            }

            UpdateProjectStatusRequest request = new UpdateProjectStatusRequest()
            {
                ProjectStatus = ProjectStatus,
                ClosedDate = ClosedYear,
                CancelledDate = CancelledYear,
                WithdrawnDate = GetWithdrawnYear(),
            };

            var projectId = RouteData.Values["projectId"] as string;

            await _updateProjectStatusService.Execute(projectId, request);
            TempData["projectStatusUpdated"] = true;
            return Redirect(GetNextPage());
        }

        private void ClearNotApplicableValues()
        {
            if (ProjectStatus == ProjectStatusType.Closed)
            {
                CancelledYear = null;
                WithdrawnYear = null;
                WithdrawnApplicationYear = null;
                return;
            }

            if (ProjectStatus == ProjectStatusType.Cancelled)
            {
                ClosedYear = null;
                WithdrawnYear = null;
                WithdrawnApplicationYear = null;
                return;
            }

            if (ProjectStatus == ProjectStatusType.WithdrawnDuringPreOpening)
            {
                ClosedYear = null;
                CancelledYear = null;
                WithdrawnApplicationYear = null;
                return;
            }

            if (ProjectStatus == ProjectStatusType.WithdrawnDuringApplication)
            {
                ClosedYear = null;
                CancelledYear = null;
                WithdrawnYear = null;
                return;
            }

            CancelledYear = null;
            ClosedYear = null;
            WithdrawnYear = null;
            WithdrawnApplicationYear = null;
        }

        private DateTime? GetWithdrawnYear()
        {
            if(WithdrawnYear != null)
            {
                return WithdrawnYear;
            }

            if (WithdrawnApplicationYear != null)
            {
                return WithdrawnApplicationYear;
            }

            return null;
        }
        
        public string GetNextPage()
        {
            var referrerQuery = _httpContextAccessor.HttpContext.Request.Query["referrer"];

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
