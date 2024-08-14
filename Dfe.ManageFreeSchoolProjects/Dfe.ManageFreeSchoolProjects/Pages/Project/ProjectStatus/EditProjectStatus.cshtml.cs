using System;
using System.ComponentModel;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectStatusType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.ProjectStatus;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.ProjectStatus
{
    public class EditProjectStatusModel : PageModel
    {
        private const string CancelledYearId = "year-cancelled";
        private const string ClosedYearId = "year-closed";
        private const string WithdrawnYearId = "year-withdrawn";
        private const string WithdrawnApplicationYearId = "year-withdrawn-application";

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

        [BindProperty(Name = CancelledYearId)]
        [DisplayName("Year the project was cancelled")]
        public string CancelledYear { get; set; }

        [BindProperty(Name = ClosedYearId)]
        [DisplayName("Year the school was closed")]
        public string ClosedYear { get; set; }

        [BindProperty(Name = WithdrawnYearId)]
        [DisplayName("Year the project was withdrawn")]
        public string WithdrawnYear { get; set; }

        [BindProperty(Name = WithdrawnApplicationYearId)]
        [DisplayName("Year the project was withdrawn")]
        public string WithdrawnApplicationYear { get; set; }

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
                    ClosedYear = ConvertToYearString(Project.ProjectStatus.ProjectClosedDate);
    
                if (Project.ProjectStatus.ProjectStatus == ProjectStatusType.Cancelled)
                    CancelledYear = ConvertToYearString(Project.ProjectStatus.ProjectCancelledDate);
                
                if(Project.ProjectStatus.ProjectStatus == ProjectStatusType.WithdrawnDuringPreOpening)
                    WithdrawnYear = ConvertToYearString(Project.ProjectStatus.ProjectWithdrawnDate);
                
                if (Project.ProjectStatus.ProjectStatus == ProjectStatusType.WithdrawnDuringApplication)
                    WithdrawnApplicationYear = ConvertToYearString(Project.ProjectStatus.ProjectWithdrawnDate);
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


            if (ProjectStatus == ProjectStatusType.Open || ProjectStatus == ProjectStatusType.Preopening)
            {
                CancelledYear = null;
                ClosedYear = null;
                WithdrawnYear = null;
                WithdrawnApplicationYear = null;
            }

            if (ProjectStatus == ProjectStatusType.Closed)
            {
                CancelledYear = null;
                WithdrawnYear = null;
                WithdrawnApplicationYear = null;
            }

            if (ProjectStatus == ProjectStatusType.Cancelled)
            {
                ClosedYear = null;
                WithdrawnYear = null;
                WithdrawnApplicationYear = null;
            }

            if (ProjectStatus == ProjectStatusType.WithdrawnDuringPreOpening)
            {
                ClosedYear = null;
                CancelledYear = null;
                WithdrawnApplicationYear = null;
            }

            if (ProjectStatus == ProjectStatusType.WithdrawnDuringApplication)
            {
                ClosedYear = null;
                CancelledYear = null;
                WithdrawnYear = null;
            }

            CheckErrors(ClosedYearId, ProjectStatusType.Closed, ClosedYear);
            CheckErrors(CancelledYearId, ProjectStatusType.Cancelled, CancelledYear);
            CheckErrors(WithdrawnYearId, ProjectStatusType.WithdrawnDuringPreOpening, WithdrawnYear);
            CheckErrors(WithdrawnApplicationYearId, ProjectStatusType.WithdrawnDuringApplication, WithdrawnApplicationYear);

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                Project = await _getProjectOverviewService.Execute(ProjectId);

                return Page();
            }

            UpdateProjectStatusRequest request = new UpdateProjectStatusRequest()
            {
                ProjectStatus = ProjectStatus,
                ClosedDate = ClosedYear == null ? null : ConvertToDateTime(ClosedYear),
                CancelledDate = CancelledYear == null ? null : ConvertToDateTime(CancelledYear),
                WithdrawnDate = GetWithdrawnYear(),
            };

            var projectId = RouteData.Values["projectId"] as string;

            await _updateProjectStatusService.Execute(projectId, request);
            TempData["projectStatusUpdated"] = true;
            return Redirect(GetNextPage());
        }

        private void CheckErrors(string id, ProjectStatusType status, string year)
        {
            bool isNumber = int.TryParse(year, out int intyear);

            var yearFormatErrorMessage = "Enter a year in the correct format";
            var yearCountErrorMessage = "Enter a year between 2000 and 2050";

            if (ProjectStatus == status && (year == null || !isNumber))
            {
                ModelState.AddModelError(id, yearFormatErrorMessage);
            }
            else if (ProjectStatus == status &&
                     (intyear is < 2000 or > 2050))
            {
                ModelState.AddModelError(id, yearCountErrorMessage);
            }
        }

        private static DateTime ConvertToDateTime(string year)
        {
            DateTime DateTimeYear = DateTime.ParseExact(year, 
                "yyyy",
                CultureInfo.InvariantCulture);
            return DateTimeYear;
        }
        
        private static string ConvertToYearString(DateTime? year)
        {
            string s = year.HasValue ? year.Value.ToString("yyyy") : null;
            return s;
            
        }

        private DateTime? GetWithdrawnYear()
        {
            if(WithdrawnYear != null)
            {
                return ConvertToDateTime(WithdrawnYear);
            }

            if (WithdrawnApplicationYear != null)
            {
                return ConvertToDateTime(WithdrawnApplicationYear);
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

    }
}
