
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
using Dfe.ManageFreeSchoolProjects.Extensions;
using Dfe.ManageFreeSchoolProjects.Validators;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.IdentityModel.Tokens;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.Dates
{
    public class EditDatesTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditDatesTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }
        public string CurrentFreeSchoolName { get; set; }

        [BindProperty(Name = "entry-into-pre-opening", BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Entry into pre-opening")]
        [DateValidation(DateRangeValidationService.DateRange.Future)]
        public DateTime? EntryIntoPreOpening { get; set; }

        [BindProperty(Name = "provisional-opening-date-agreed-with-trust", BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Provisional opening date agreed with trust")]
        [DateValidation(DateRangeValidationService.DateRange.Future)]
        public DateTime? ProvisionalOpeningDateAgreedWithTrust { get; set; }
        
        [BindProperty(Name = "project-closed-date", BinderType = typeof(YearInputModelBinder))]
        [Display(Name = "project status closed")]
        public string ProjectClosedDate { get; set; }
        
        public bool ProjectClosedDateHasValue { get; set; }
        
        [BindProperty(Name = "project-cancelled-date",BinderType = typeof(YearInputModelBinder))]
        [Display(Name = "project cancelled date")]
        public string ProjectCancelledDate { get; set; }
        public bool ProjectCancelledDateHasValue { get; set; }
        
        [BindProperty(Name = "project-withdrawn-date")]
        [Display(Name = "project withdrawn date")]
        public string ProjectWithdrawnDate { get; set; }
        
        public bool ProjectWithdrawnDateHasValue { get; set; }
        public EditDatesTaskModel(
            IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditDatesTaskModel> logger,
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
                var project = await _getProjectService.Execute(ProjectId, TaskName.Dates);
                CurrentFreeSchoolName = project.SchoolName;
                ProjectClosedDateHasValue = project.Dates.ProjectClosedDate.HasValue;
                ProjectCancelledDateHasValue = project.Dates.ProjectCancelledDate.HasValue;
                ProjectWithdrawnDateHasValue = project.Dates.ProjectWithdrawnDate.HasValue;
                EntryIntoPreOpening = project.Dates.DateOfEntryIntoPreopening;
                ProjectClosedDate = project.Dates.ProjectClosedDate.ToYearString();
                ProjectCancelledDate = project.Dates.ProjectCancelledDate.ToYearString();
                ProjectWithdrawnDate = project.Dates.ProjectWithdrawnDate.ToYearString();
                ProvisionalOpeningDateAgreedWithTrust = project.Dates.ProvisionalOpeningDateAgreedWithTrust;
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
                var project = await _getProjectService.Execute(ProjectId, TaskName.Dates);
                
                ProjectClosedDateHasValue = project.Dates.ProjectClosedDate.HasValue;
                
                ProjectCancelledDateHasValue = project.Dates.ProjectCancelledDate.HasValue;
                
                ProjectWithdrawnDateHasValue = project.Dates.ProjectWithdrawnDate.HasValue;
                
                
                
                if (project.Dates.ProjectClosedDate.HasValue 
                    && !project.Dates.ProjectCancelledDate.HasValue
                    && !project.Dates.ProjectWithdrawnDate.HasValue
                    )
                {
                    ModelState.Remove("project-cancelled-date");
                    ModelState.Remove("project-withdrawn-date");
                }
                
                if (project.Dates.ProjectCancelledDate.HasValue
                    && !project.Dates.ProjectClosedDate.HasValue
                    && !project.Dates.ProjectWithdrawnDate.HasValue
                    )
                {
                    ModelState.Remove("project-closed-date");
                    ModelState.Remove("project-withdrawn-date");
                }
                
                if (project.Dates.ProjectWithdrawnDate.HasValue
                    && !project.Dates.ProjectClosedDate.HasValue
                    && !project.Dates.ProjectCancelledDate.HasValue
                    )
                {
                    ModelState.Remove("project-closed-date");
                    ModelState.Remove("project-cancelled-date");
                }
                
                if (!project.Dates.ProjectWithdrawnDate.HasValue
                    && !project.Dates.ProjectCancelledDate.HasValue
                    && !project.Dates.ProjectClosedDate.HasValue)
                {
                    ModelState.Remove("project-closed-date");
                    ModelState.Remove("project-cancelled-date");
                    ModelState.Remove("project-withdrawn-date");
                }
                
                _errorService.AddErrors(ModelState.Keys, ModelState);
                
                CurrentFreeSchoolName = project.SchoolName;
                
                if (!ModelState.IsValid)
                {
                
                return Page();
                }

            try
            {
                var request = new UpdateProjectByTaskRequest()
                {
                    Dates = new DatesTask()
                    {
                        DateOfEntryIntoPreopening = EntryIntoPreOpening,
                        ProvisionalOpeningDateAgreedWithTrust = ProvisionalOpeningDateAgreedWithTrust,
                        ProjectClosedDate = ProjectClosedDate.ToYearDate(),
                        ProjectCancelledDate = ProjectCancelledDate.ToYearDate(),
                        ProjectWithdrawnDate = ProjectWithdrawnDate.ToYearDate(),
                    }
                };

                await _updateProjectTaskService.Execute(ProjectId, request);

                return Redirect(string.Format(RouteConstants.ViewDatesTask, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }
        
    }
}
