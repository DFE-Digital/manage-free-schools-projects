using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;



namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.FundingAgreementSubmission
{
    public class EditFundingAgreementSubmissionTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditFundingAgreementSubmissionTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }


        [BindProperty(Name = "drafted-fa-submission")]
        public bool? DraftedFASubmission { get; set; }

        [BindProperty(Name = "regional-director-signed-off-fa-submission")]
        public bool? RegionalDirectorSignedOffFaSubmission { get; set; }

        [BindProperty(Name = "minister-signed-off-fa-submission")]
        public bool? MinisterSignedOffFaSubmission { get; set; }

        [BindProperty(Name = "saved-fa-submission-in-workplaces-folder")]
        public bool? SavedFaSubmissionInWorkplacesFolder { get; set; }

        [BindProperty]
        public string SchoolName { get; set; }

        public bool IsPresumptionRoute {  get; set; }

        public EditFundingAgreementSubmissionTaskModel(
            IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditFundingAgreementSubmissionTaskModel> logger,
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

            if (IsPresumptionRoute)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var updateTaskRequest = new UpdateProjectByTaskRequest()
            {
                FundingAgreementSubmission = new()
                {
                    DraftedFundingAgreementSubmission = DraftedFASubmission,
                    RegionalDirectorSignedOffFundingAgreementSubmission = RegionalDirectorSignedOffFaSubmission,
                    MinisterSignedOffFundingAgreementSubmission = MinisterSignedOffFaSubmission,
                    SavedFundingAgreementSubmissionInWorkplacesFolder = SavedFaSubmissionInWorkplacesFolder
                }
            };

            await _updateProjectTaskService.Execute(ProjectId, updateTaskRequest);

            return Redirect(string.Format(RouteConstants.ViewFundingAgreementSubmissionTask, ProjectId));
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.FundingAgreementSubmission);

            DraftedFASubmission = project.FundingAgreementSubmission.DraftedFundingAgreementSubmission;
            RegionalDirectorSignedOffFaSubmission = project.FundingAgreementSubmission.RegionalDirectorSignedOffFundingAgreementSubmission;
            MinisterSignedOffFaSubmission = project.FundingAgreementSubmission.MinisterSignedOffFundingAgreementSubmission;
            SavedFaSubmissionInWorkplacesFolder = project.FundingAgreementSubmission.SavedFundingAgreementSubmissionInWorkplacesFolder;

            SchoolName = project.SchoolName;
            IsPresumptionRoute = project.IsPresumptionRoute;
        }

    }
}
