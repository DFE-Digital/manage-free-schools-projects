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
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.KickOffMeeting
{
    public class EditKickOffMeetingTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditKickOffMeetingTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }
        
        [BindProperty(Name = "funding-arrangement-agreed")]
        public bool? FundingArrangementAgreed { get; set; }
        
        [BindProperty(Name = "funding-arrangement-details-agreed")]
        [Display(Name = "Comments")]
        [ValidText(100)]
        public string FundingArrangementDetailsAgreed { get; set; }
        
        [BindProperty(Name = "realistic-year-of-opening", BinderType= typeof(StartEndModelBinder))]
        public string RealisticYearOfOpening { get; set; }
        
        [BindProperty(Name = "provisional-opening-date", BinderType = typeof(DateInputModelBinder))]
        
        public DateTime? ProvisionalOpeningDate { get; set; }
        
        [BindProperty(Name = "saved-documents-in-workplaces-folder")]
        public bool? SavedDocumentsInWorkplacesFolder { get; set; }

        public string SchoolName { get; set; }
        public bool IsPresumptionRoute { get; set; }


        public EditKickOffMeetingTaskModel(IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditKickOffMeetingTaskModel> logger,
            ErrorService errorService)
        {
            _getProjectService = getProjectService;
            _updateProjectTaskService = updateProjectTaskService;
            _logger = logger;
            _errorService = errorService;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            await LoadProject();
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.KickOffMeeting);
            SchoolName = project.SchoolName;
            IsPresumptionRoute = project.IsPresumptionRoute;

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            try
            {
                var request = new UpdateProjectByTaskRequest()
                {
                    KickOffMeeting = new KickOffMeetingTask()
                    {
                        FundingArrangementAgreed = FundingArrangementAgreed,
                        FundingArrangementDetailsAgreed = FundingArrangementDetailsAgreed,
                        RealisticYearOfOpening = RealisticYearOfOpening,
                        ProvisionalOpeningDate = ProvisionalOpeningDate,
                        SavedDocumentsInWorkplacesFolder = SavedDocumentsInWorkplacesFolder
                        
                    }
                };

                await _updateProjectTaskService.Execute(ProjectId, request);

                return Redirect(string.Format(RouteConstants.ViewKickOffMeeting, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.KickOffMeeting);

            FundingArrangementAgreed = project.KickOffMeeting.FundingArrangementAgreed;
            FundingArrangementDetailsAgreed = project.KickOffMeeting.FundingArrangementDetailsAgreed;
            RealisticYearOfOpening = project.KickOffMeeting.RealisticYearOfOpening;
            ProvisionalOpeningDate = project.KickOffMeeting.ProvisionalOpeningDate;
            SavedDocumentsInWorkplacesFolder = project.KickOffMeeting.SavedDocumentsInWorkplacesFolder;
            
            SchoolName = project.SchoolName;
            IsPresumptionRoute = project.IsPresumptionRoute;
        }
    }
}
