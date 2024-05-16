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


namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.MovingToOpen
{
    public class EditMovingToOpenTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditMovingToOpenTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }
        
        [BindProperty(Name = "project-brief-to-sfso")]
        
        public bool? ProjectBriefToSfso { get; set; }
        
        [BindProperty(Name = "project-brief-to-education-estates")]
        
        public bool? ProjectBriefToEducationEstates { get; set; }
        
        [BindProperty(Name = "project-brief-to-new-delivery-officer")]
        
        public bool? ProjectBriefToNewDeliveryOfficer { get; set; }
        
        [BindProperty(Name = "sent-emails-to-relevant-contacts")]
        
        public bool? SentEmailsToRelevantContacts { get; set; }
        
        [BindProperty(Name = "sent-emails-to-schools-principle")]
        
        public bool? SentEmailsToSchoolsPrinciple { get; set; }
        
        [BindProperty(Name = "saved-to-workplaces-folder-annexe")]
        
        public bool? SavedToWorkplacesFolderAnnexE { get; set; }
        
        [BindProperty(Name = "saved-to-workplaces-folder-annexb")]
        
        public bool? SavedToWorkplacesFolderAnnexB { get; set; }
        
        [BindProperty(Name = "saved-to-workplaces-folder-project-brief")]
        
        public bool? SavedToWorkplacesFolderProjectBrief { get; set; }
        
        
        [BindProperty(Name = "actual-opening-date", BinderType = typeof(DateInputModelBinder))]
        public DateTime? ActualOpeningDate { get; set; }
        
        public string SchoolName { get; set; }

        public EditMovingToOpenTaskModel(IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditMovingToOpenTaskModel> logger,
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
            var project = await _getProjectService.Execute(ProjectId, TaskName.MovingToOpen);
            SchoolName = project.SchoolName;

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            try
            {
                var request = new UpdateProjectByTaskRequest()
                {
                    MovingToOpen = new MovingToOpenTask()
                    {
                        ProjectBriefToSfso = ProjectBriefToSfso,
                        ProjectBriefToNewDeliveryOfficer = ProjectBriefToNewDeliveryOfficer,
                        ProjectBriefToEducationEstates = ProjectBriefToEducationEstates,
                        SentEmailsToSchoolsPrinciple = SentEmailsToSchoolsPrinciple,
                        SentEmailsToRelevantContacts = SentEmailsToRelevantContacts,
                        SavedToWorkplacesFolderAnnexB = SavedToWorkplacesFolderAnnexB,
                        SavedToWorkplacesFolderAnnexE = SavedToWorkplacesFolderAnnexE,
                        SavedToWorkplacesFolderProjectBrief = SavedToWorkplacesFolderProjectBrief,
                        ActualOpeningDate = ActualOpeningDate
                    }
                };

                await _updateProjectTaskService.Execute(ProjectId, request);

                return Redirect(string.Format(RouteConstants.ViewMovingToOpenTask, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.MovingToOpen);
            
            ProjectBriefToSfso = project.MovingToOpen.ProjectBriefToSfso;
            ProjectBriefToNewDeliveryOfficer = project.MovingToOpen.ProjectBriefToNewDeliveryOfficer;
            ProjectBriefToEducationEstates = project.MovingToOpen.ProjectBriefToEducationEstates;
            SentEmailsToRelevantContacts = project.MovingToOpen.SentEmailsToRelevantContacts;
            SentEmailsToSchoolsPrinciple = project.MovingToOpen.SentEmailsToSchoolsPrinciple;
            SavedToWorkplacesFolderAnnexE = project.MovingToOpen.SavedToWorkplacesFolderAnnexE;
            SavedToWorkplacesFolderAnnexB = project.MovingToOpen.SavedToWorkplacesFolderAnnexB;
            ActualOpeningDate = project.MovingToOpen.ActualOpeningDate;
            
            
            SchoolName = project.SchoolName;
        }
    }
}
