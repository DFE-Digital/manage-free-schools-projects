using System;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.Models;


namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.OfstedPreRegistration
{
    public class EditOfstedPreRegistrationAfterTaskModel(
        IGetProjectByTaskService getProjectService,
        IUpdateProjectByTaskService updateProjectTaskService,
        ILogger<EditOfstedPreRegistrationAfterTaskModel> logger,
        ErrorService errorService) : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "shared-outcome-with-trust")]
        public bool? SharedOutcomeWithTrust { get; set; }

        [BindProperty(Name = "inspection-conditions-met")]
        public YesNoNotApplicable? InspectionConditionsMet { get; set; }

        [BindProperty(Name = "proposed-to-open-on-gias")]
        public bool? ProposedToOpenOnGias { get; set; }

        [BindProperty(Name = "saved-to-workplaces")]
        public bool? SavedToWorkplaces { get; set; }

        public string SchoolName { get; set; }
        
        [BindProperty(Name = "date-inspection-and-any-actions-completed", BinderType = typeof(DateInputModelBinder))]
        public DateTime? DateInspectionsAndAnyActionsCompleted { get; set; }
        
        public async Task<ActionResult> OnGet()
        {
            logger.LogMethodEntered();

            await LoadProject();
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            var project = await getProjectService.Execute(ProjectId, TaskName.OfstedInspection);
            SchoolName = project.SchoolName;
            
            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }
            
            var request = new UpdateProjectByTaskRequest()
            {
                OfstedInspection = project.OfstedInspection
            };

            request.OfstedInspection.SharedOutcomeWithTrust = SharedOutcomeWithTrust;
            request.OfstedInspection.InspectionConditionsMet = InspectionConditionsMet;
            request.OfstedInspection.ProposedToOpenOnGias = ProposedToOpenOnGias;
            request.OfstedInspection.SavedToWorkplaces = SavedToWorkplaces;
            request.OfstedInspection.DateInspectionsAndAnyActionsCompleted = DateInspectionsAndAnyActionsCompleted;

            await updateProjectTaskService.Execute(ProjectId, request);
            return Redirect(string.Format(RouteConstants.ViewOfstedPreRegistrationTask, ProjectId));
        }

        private async Task LoadProject()
        {
            var project = await getProjectService.Execute(ProjectId, TaskName.OfstedInspection);

            SharedOutcomeWithTrust = project.OfstedInspection.SharedOutcomeWithTrust;
            InspectionConditionsMet = project.OfstedInspection.InspectionConditionsMet;
            ProposedToOpenOnGias = project.OfstedInspection.ProposedToOpenOnGias;
            SavedToWorkplaces = project.OfstedInspection.SavedToWorkplaces;
            DateInspectionsAndAnyActionsCompleted = project.OfstedInspection.DateInspectionsAndAnyActionsCompleted;

            SchoolName = project.SchoolName;
        }
    }
}