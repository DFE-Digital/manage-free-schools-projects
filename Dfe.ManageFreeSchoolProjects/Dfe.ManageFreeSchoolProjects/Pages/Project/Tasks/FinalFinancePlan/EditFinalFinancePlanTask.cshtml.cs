using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using System.ComponentModel;


namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.FinalFinancePlan
{
    public class EditFinalFinancePlanTaskModel(
        IGetProjectByTaskService getProjectService,
        IUpdateProjectByTaskService updateProjectTaskService,
        ILogger<EditFinalFinancePlanTaskModel> logger,
        ErrorService errorService)
        : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }
        
        [BindProperty(Name = "confirmed-trust-has-provided-final-plan")]
        [DisplayName("Confirmed that the trust has provided the final finance plan")]
        public bool? ConfirmedTrustHasProvidedFinalPlan { get; set; }

        [BindProperty(Name = "grade-6-signed-off-final-plan-date", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Date that the Grade 6 signed-off the final plan")]
        public DateTime? Grade6SignedOffFinalPlanDate { get; set; }

        [BindProperty(Name = "sent-final-plan-to-revenue-funding-mailbox")]
        [DisplayName("Sent the final plan to the Revenue Funding mailbox")]
        public bool? SentFinalPlanToRevenueFundingMailbox { get; set; }

        [BindProperty(Name = "saved-final-plan-in-workplaces-folder")]
        [DisplayName("Saved final plan in Workplaces folder")]
        public bool? SavedFinalPlanInWorkplacesFolder { get; set; }

        [BindProperty(Name = "expected-date-grade6-will-signoff-final-plan", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Expected date that the Grade 6 will sign-off the final plan")]
        public DateTime? ExpectedDateGrade6WillSignOffFinalPlan { get; set; }
        
        [BindProperty]
        public string SchoolName { get; set; }


        public async Task<ActionResult> OnGet()
        {
            logger.LogMethodEntered();

            await LoadProject();
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var updateTaskRequest = new UpdateProjectByTaskRequest
            {
                FinalFinancePlan = new FinalFinancePlanTask
                {
                    ExpectedDateGrade6WillSignOffTheFinalPlan = ExpectedDateGrade6WillSignOffFinalPlan,
                    ConfirmedTrustHasProvidedFinalPlan = ConfirmedTrustHasProvidedFinalPlan,
                    Grade6SignedOffFinalPlanDate = Grade6SignedOffFinalPlanDate,
                    SentFinalPlanToRevenueFundingMailbox = SentFinalPlanToRevenueFundingMailbox,
                    SavedFinalPlanInWorkplacesFolder = SavedFinalPlanInWorkplacesFolder
                }
            };

            await updateProjectTaskService.Execute(ProjectId, updateTaskRequest);

            return Redirect(string.Format(RouteConstants.ViewFinalFinancePlanTask, ProjectId));
        }

        private async Task LoadProject()
        {
            var project = await getProjectService.Execute(ProjectId, TaskName.FinalFinancePlan);

            ExpectedDateGrade6WillSignOffFinalPlan = project.FinalFinancePlan.ExpectedDateGrade6WillSignOffTheFinalPlan;
            ConfirmedTrustHasProvidedFinalPlan = project.FinalFinancePlan.ConfirmedTrustHasProvidedFinalPlan;
            Grade6SignedOffFinalPlanDate = project.FinalFinancePlan.Grade6SignedOffFinalPlanDate;
            SentFinalPlanToRevenueFundingMailbox = project.FinalFinancePlan.SentFinalPlanToRevenueFundingMailbox;
            SavedFinalPlanInWorkplacesFolder = project.FinalFinancePlan.SavedFinalPlanInWorkplacesFolder;
            
            SchoolName = project.SchoolName;
        }

    }
}
