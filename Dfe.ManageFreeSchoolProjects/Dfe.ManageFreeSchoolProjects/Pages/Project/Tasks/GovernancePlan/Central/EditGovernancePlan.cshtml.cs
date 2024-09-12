using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.GovernancePlan.Central
{
    public class EditGovernancePlanModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditGovernancePlanModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "plan-received-from-trust")]
        public bool? PlanReceivedFromTrust { get; set; }

        [BindProperty(Name = "date-plan-received", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Date received")]
        public DateTime? DatePlanReceived { get; set; }

        [BindProperty(Name = "plan-assessed-using-template")]
        public bool? PlanAssessedUsingTemplate { get; set; }

        [BindProperty(Name = "plan-and-assessment-shared-with-expert")]
        public bool? PlanAndAssessmentSharedWithExpert { get; set; }

        [BindProperty(Name = "plan-and-assessment-shared-with-esfa")]
        public bool? PlanAndAssessmentSharedWithEsfa { get; set; }

        [BindProperty(Name = "plan-fed-back-to-trust")]
        public bool? PlanFedBackToTrust { get; set; }

        [BindProperty(Name = "final-governance-plan-agreed")]
        public bool? FinalGovernancePlanAgreed { get; set; }

        [BindProperty(Name = "date-plan-agreed", BinderType = typeof(DateInputModelBinder))]
        public DateTime? DatePlanAgreed { get; set; }

        [BindProperty(Name = "saved-documents-in-workplaces-folder")]
        public bool? SavedDocumentsInWorkplacesFolder { get; set; }

        [BindProperty(Name = "comments")]
        [DisplayName("Comments")]
        [ValidText(999)]
        public string Comments { get; set; }

        [BindProperty]
        public string SchoolName { get; set; }

        public EditGovernancePlanModel(IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditGovernancePlanModel> logger,
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

            await LoadProject();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (PlanReceivedFromTrust != true)
            {
                var errorKeys = ModelState.Keys.Where(k => k.StartsWith("date-plan-received")).ToList();

                errorKeys.ForEach(k => ModelState.Remove(k));

                DatePlanReceived = null;
            }

            if (FinalGovernancePlanAgreed != true)
            {
                var errorKeys = ModelState.Keys.Where(k => k.StartsWith("date-plan-agreed")).ToList();

                errorKeys.ForEach(k => ModelState.Remove(k));

                DatePlanAgreed = null;
            }

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var updateTaskRequest = new UpdateProjectByTaskRequest()
            {
                GovernancePlan = new()
                {
                    PlanReceivedFromTrust = PlanReceivedFromTrust,
                    DatePlanReceived = DatePlanReceived,
                    PlanAssessedUsingTemplate = PlanAssessedUsingTemplate,
                    PlanAndAssessmentSharedWithExpert = PlanAndAssessmentSharedWithExpert,
                    PlanAndAssessmentSharedWithEsfa = PlanAndAssessmentSharedWithEsfa,
                    PlanFedBackToTrust = PlanFedBackToTrust,
                    FinalGovernancePlanAgreed = FinalGovernancePlanAgreed,
                    DatePlanAgreed = DatePlanAgreed,
                    SavedDocumentsInWorkplacesFolder = SavedDocumentsInWorkplacesFolder,
                    Comments = Comments
                }
            };

            await _updateProjectTaskService.Execute(ProjectId, updateTaskRequest);

            return Redirect(string.Format(RouteConstants.ViewGovernancePlanCentralTask, ProjectId));
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.GovernancePlan);

            PlanReceivedFromTrust = project.GovernancePlan.PlanReceivedFromTrust;
            DatePlanReceived = project.GovernancePlan.DatePlanReceived;
            PlanAssessedUsingTemplate = project.GovernancePlan.PlanAssessedUsingTemplate;
            PlanAndAssessmentSharedWithExpert = project.GovernancePlan.PlanAndAssessmentSharedWithExpert;
            PlanAndAssessmentSharedWithEsfa = project.GovernancePlan.PlanAndAssessmentSharedWithEsfa;
            PlanFedBackToTrust = project.GovernancePlan.PlanFedBackToTrust;
            FinalGovernancePlanAgreed = project.GovernancePlan.FinalGovernancePlanAgreed;
            DatePlanAgreed = project.GovernancePlan.DatePlanAgreed;
            SavedDocumentsInWorkplacesFolder = project.GovernancePlan.SavedDocumentsInWorkplacesFolder;
            Comments = project.GovernancePlan.Comments;

            SchoolName = project.SchoolName;
        }
    }
}
