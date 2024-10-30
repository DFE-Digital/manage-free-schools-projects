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
using System.ComponentModel;
using System.Threading.Tasks;


namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.EducationBrief
{
    public class EditEducationBriefTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditEducationBriefTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }
        
        [BindProperty(Name = "trust-confirmed-education-plans-and-policies-in-place")]
        public bool? TrustConfirmedPlansAndPoliciesInPlace { get; set; }

        [BindProperty(Name = "date-trust-provided-education-brief", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Date the trust provided the education brief")]
        public DateTime? DateTrustProvidedEducationBrief { get; set; }

        [BindProperty(Name = "commissioned-ee-to-review-safeguarding-policy")]
        public bool? CommissionedEEToReviewSafeguardingPolicy { get; set; }

        [BindProperty(Name = "commissioned-ee-to-pupil-assessment-recording-and-reporting-policy")]
        public bool? CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy { get; set; }

        [BindProperty(Name = "date-ee-reviewed-education-brief", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Date the EE reviewed the education brief")]
        public DateTime? DateEEReviewedEducationBrief { get; set; }

        [BindProperty(Name = "saved-copies-of-plans-and-policies-in-workplaces")]
        public bool? SavedCopiesOfPlansAndPoliciesInWorkplaces { get; set; }

        [BindProperty(Name = "saved-copies-of-plans-and-policies-in-workplaces")]
        public bool? SavedEESpecificationAndAdviceInWorkplaces { get; set; }

        public bool IsPresumptionRoute { get; set; }
        public string SchoolName { get; set; }

        public EditEducationBriefTaskModel(IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditEducationBriefTaskModel> logger,
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
            var project = await _getProjectService.Execute(ProjectId, TaskName.EducationBrief);
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
                    EducationBrief = new EducationBriefTask()
                    {
                        TrustConfirmedPlansAndPoliciesInPlace = TrustConfirmedPlansAndPoliciesInPlace,
                        DateTrustProvidedEducationBrief = DateTrustProvidedEducationBrief,
                        CommissionedEEToReviewSafeguardingPolicy = CommissionedEEToReviewSafeguardingPolicy,
                        CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy = CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy,
                        DateEEReviewedEducationBrief = DateEEReviewedEducationBrief,
                        SavedCopiesOfPlansAndPoliciesInWorkplaces = SavedCopiesOfPlansAndPoliciesInWorkplaces,
                        SavedEESpecificationAndAdviceInWorkplaces = SavedEESpecificationAndAdviceInWorkplaces,
                    }
                };

                await _updateProjectTaskService.Execute(ProjectId, request);
                return Redirect(string.Format(RouteConstants.ViewEducationPlansAndPoliciesTask, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.EducationBrief);

            TrustConfirmedPlansAndPoliciesInPlace = project.EducationBrief.TrustConfirmedPlansAndPoliciesInPlace;
            DateTrustProvidedEducationBrief = project.EducationBrief.DateTrustProvidedEducationBrief;
            CommissionedEEToReviewSafeguardingPolicy = project.EducationBrief.CommissionedEEToReviewSafeguardingPolicy;
            CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy = project.EducationBrief.CommissionedEEToReviewPupilAssessmentRecordingAndReportingPolicy;
            DateEEReviewedEducationBrief = project.EducationBrief.DateEEReviewedEducationBrief;
            SavedCopiesOfPlansAndPoliciesInWorkplaces = project.EducationBrief.SavedCopiesOfPlansAndPoliciesInWorkplaces;
            SavedEESpecificationAndAdviceInWorkplaces = project.EducationBrief.SavedEESpecificationAndAdviceInWorkplaces;
           
            SchoolName = project.SchoolName;
            IsPresumptionRoute = project.IsPresumptionRoute;
        }
    }
}
