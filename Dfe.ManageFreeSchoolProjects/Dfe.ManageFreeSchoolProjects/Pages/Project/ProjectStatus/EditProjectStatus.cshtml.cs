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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
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
        public ProjectOverviewResponse Project { get; set; }
        
        [BindProperty(Name = "projectId")]
        public string ProjectId { get; set; }
        
        public string GetNextPage()
        {
            return string.Format(RouteConstants.ProjectOverview, ProjectId);
        }
        
        [BindProperty(Name = "project-status")]
        public ProjectStatusType ProjectStatus { get; set; }
        
        [BindProperty(Name = "closed-year")]
        [DisplayName("Closed year")]
        public String? ClosedYear { get; set; }
        
        [BindProperty(Name = "cancelled-year")]
        [DisplayName("Cancelled year")]
        public String? CancelledYear { get; set; }

        public EditProjectStatusModel(IGetProjectOverviewService getProjectOverviewService,
            IUpdateProjectStatusService updateProjectStatusService,
            ILogger<EditProjectStatusModel> logger,
            ErrorService errorService)
        {
            _getProjectOverviewService = getProjectOverviewService;
            _updateProjectStatusService = updateProjectStatusService;
            _logger = logger;
            _errorService = errorService;
        }

        public async Task<IActionResult> OnGet()
        {

            try
            {
                var projectId = RouteData.Values["projectId"] as string;

                Project = await _getProjectOverviewService.Execute(projectId);
                ProjectStatus = Project.ProjectStatus.ProjectStatus;
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
            }

            ProjectId = Project.ProjectStatus.ProjectId;

            return Page();
        }
        
    public async Task<IActionResult> OnPost()
    {

            bool isClosedNumber = int.TryParse(ClosedYear, out int intClosedYear);
            bool isCancelledNumber = int.TryParse(CancelledYear, out int intCancelledYear);
        
            if (ProjectStatus == ProjectStatusType.Closed && ClosedYear == null)
            {
                ModelState.AddModelError("closed-year-error", "Enter a year in the correct format");
            }
            
            else if (ProjectStatus == ProjectStatusType.Closed && !isClosedNumber)
            {
                ModelState.AddModelError("closed-year-error", "Enter a year in the correct format");
            }
            
            else if (ProjectStatus == ProjectStatusType.Closed && intClosedYear < 2000)
            {
                ModelState.AddModelError("closed-year-count-error", "Year must be between 2000 and 2050");
            }
            
            else if (ProjectStatus == ProjectStatusType.Closed && intClosedYear > 2050)
            {
                ModelState.AddModelError("closed-year-count-error", "Year must be between 2000 and 2050");
            }
            
            if (ProjectStatus == ProjectStatusType.Cancelled && CancelledYear == null)
            {
                ModelState.AddModelError("cancelled-year-error", "Enter a year in the correct format");
            }
            
            else if (ProjectStatus == ProjectStatusType.Cancelled && !isCancelledNumber)
            {
                ModelState.AddModelError("cancelled-year-error", "Enter a year in the correct format");
            }
            
            else if (ProjectStatus == ProjectStatusType.Cancelled && intClosedYear < 2000)
            {
                ModelState.AddModelError("cancelled-year-count-error", "Year must be between 2000 and 2050");
            }
            
            else if (ProjectStatus == ProjectStatusType.Cancelled && intCancelledYear > 2050)
            {
                ModelState.AddModelError("cancelled-year-count-error", "Year must be between 2000 and 2050");
            }
            
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                
                Project = await _getProjectOverviewService.Execute(ProjectId);
                ProjectStatus = ProjectStatus;
                return Page();
            }

            UpdateProjectStatusRequest request = new UpdateProjectStatusRequest()
            {
                ProjectStatus = ProjectStatus
            };
            
            var projectId = RouteData.Values["projectId"] as string;
            
            await _updateProjectStatusService.Execute(projectId,request);
            
            return Redirect(GetNextPage());
        }
    }
}
