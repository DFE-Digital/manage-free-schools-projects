using System;
using System.ComponentModel;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Extensions;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.TagHelpers;
using Dfe.ManageFreeSchoolProjects.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectStatusType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.ProjectStatus;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.ProjectStatus
{
    public class EditProjectStatusModel : PageModel
    {

        private readonly IGetProjectOverviewService _getProjectOverviewService;
        private readonly IUpdateProjectStatusService _updateProjectStatusService;
        private readonly ILogger<EditProjectStatusModel> _logger;
        private readonly ErrorService _errorService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProjectOverviewResponse Project { get; set; }

        [BindProperty(Name = "projectId")] public string ProjectId { get; set; }
        
        [BindProperty(Name = "project-status")]
        public ProjectStatusType ProjectStatus { get; set; }

        [BindProperty(Name = "closed-year")]
        [DisplayName("Closed year")]
        public String? ClosedYear { get; set; }

        [BindProperty(Name = "cancelled-year")]
        [DisplayName("Cancelled year")]
        public String? CancelledYear { get; set; }

        [BindProperty(Name = "withdrawn-year")]
        [DisplayName("Withdrawn year")]
        public String? WithdrawnYear { get; set; }

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
                ClosedYear = convertToYearString(Project.ProjectStatus.ProjectClosedDate);
                CancelledYear = convertToYearString(Project.ProjectStatus.ProjectCancelledDate);
                WithdrawnYear = convertToYearString(Project.ProjectStatus.ProjectWithdrawnDate);
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

            bool isClosedNumber = int.TryParse(ClosedYear, out int intClosedYear);
            bool isCancelledNumber = int.TryParse(CancelledYear, out int intCancelledYear);
            bool isWithdrawnNumber = int.TryParse(WithdrawnYear, out int intWithdrawnYear);

            var yearFormatErrorMessage = "Enter a year in the correct format";
            var yearCountErrorMessage = "Enter a year between 2000 and 2050";
            
            if (ProjectStatus == ProjectStatusType.Closed && (ClosedYear == null || !isClosedNumber))
            {
                ModelState.AddModelError("closed-year-error", yearFormatErrorMessage);
            }
            
            else if (ProjectStatus == ProjectStatusType.Closed &&
                     (intClosedYear is < 2000 or > 2050))
            {
                ModelState.AddModelError("closed-year-count-error", yearCountErrorMessage);
            }
            
            if (ProjectStatus == ProjectStatusType.Cancelled && (CancelledYear == null || !isCancelledNumber))
            {
                ModelState.AddModelError("cancelled-year-error", yearFormatErrorMessage);
            }
            
            else if (ProjectStatus == ProjectStatusType.Cancelled &&
                     (intCancelledYear is < 2000 or > 2050))
            {
                ModelState.AddModelError("cancelled-year-count-error", yearCountErrorMessage);
            }
            
            if (ProjectStatus == ProjectStatusType.WithdrawnDuringPreOpening && (WithdrawnYear == null || !isWithdrawnNumber))
            {
                ModelState.AddModelError("withdrawn-year-error", yearFormatErrorMessage);
            }
            
            else if (ProjectStatus == ProjectStatusType.WithdrawnDuringPreOpening &&
                     (intWithdrawnYear is < 2000 or > 2050)) 
            {
                ModelState.AddModelError("withdrawn-year-count-error", yearCountErrorMessage);
            }
            
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);

                Project = await _getProjectOverviewService.Execute(ProjectId);

                return Page();
            }

            if (ProjectStatus == ProjectStatusType.Open || ProjectStatus == ProjectStatusType.Preopening)
            {
                CancelledYear = null;
                ClosedYear = null;
                WithdrawnYear = null;
            }
            
            if (ProjectStatus == ProjectStatusType.Closed)
            {
                CancelledYear = null;
                WithdrawnYear = null;
            }
            
            if (ProjectStatus == ProjectStatusType.Cancelled)
            {
                ClosedYear = null;
                WithdrawnYear = null;
            }
            
            if (ProjectStatus == ProjectStatusType.WithdrawnDuringPreOpening)
            {
                ClosedYear = null;
                CancelledYear = null;
            }

            UpdateProjectStatusRequest request = new UpdateProjectStatusRequest()
            {
                ProjectStatus = ProjectStatus,
                ClosedDate = ClosedYear == null ? null : convertToDateTime(ClosedYear),
                CancelledDate = CancelledYear == null ? null : convertToDateTime(CancelledYear), 
                WithdrawnDate  = WithdrawnYear == null ? null : convertToDateTime(WithdrawnYear),
            };

            var projectId = RouteData.Values["projectId"] as string;

            await _updateProjectStatusService.Execute(projectId, request);
            TempData["projectStatusUpdated"] = true;
            return Redirect(GetNextPage());
        }

        private static DateTime convertToDateTime(string year)
        {
            DateTime DateTimeYear = DateTime.ParseExact(year, 
                "yyyy",
                CultureInfo.InvariantCulture);
            return DateTimeYear;
        }
        
        private static string convertToYearString(DateTime? year)
        {
            string s = year.HasValue ? year.Value.ToString("yyyy") : null;
            return s;
            
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
