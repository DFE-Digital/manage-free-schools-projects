using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PupilNumbersChecks
{
    public class EditPupilNumbersChecksTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditPupilNumbersChecksTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }
        
        [BindProperty(Name = "school-received-enough-applications")]
        
        public bool? SchoolReceivedEnoughApplications{ get; set; }
        
        [BindProperty(Name = "capacity-data-matches-funding-agreement")]
        
        public bool? CapacityDataMatchesFundingAgreement{ get; set; }
        
        [BindProperty(Name = "capacity-data-matches-gias-registration")]
        
        public bool? CapacityDataMatchesGiasRegistration{ get; set; }
        public string SchoolName { get; set; }

        public EditPupilNumbersChecksTaskModel(IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditPupilNumbersChecksTaskModel> logger,
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
            var project = await _getProjectService.Execute(ProjectId, TaskName.ApplicationsEvidence);
            SchoolName = project.SchoolName;
            
            try
            {
                var request = new UpdateProjectByTaskRequest
                {
                    PupilNumbersChecks = new PupilNumbersChecksTask
                    {
                        SchoolReceivedEnoughApplications = SchoolReceivedEnoughApplications, 
                        CapacityDataMatchesFundingAgreement = CapacityDataMatchesFundingAgreement,
                        CapacityDataMatchesGiasRegistration = CapacityDataMatchesGiasRegistration
                    }
                };

                await _updateProjectTaskService.Execute(ProjectId, request);
                return Redirect(string.Format(RouteConstants.ViewPupilNumbersChecksTask, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.PupilNumbersChecks);
            
            SchoolReceivedEnoughApplications = project.PupilNumbersChecks.SchoolReceivedEnoughApplications;
            CapacityDataMatchesFundingAgreement = project.PupilNumbersChecks.CapacityDataMatchesFundingAgreement;
            CapacityDataMatchesGiasRegistration = project.PupilNumbersChecks.CapacityDataMatchesGiasRegistration;
            
            SchoolName = project.SchoolName;
        }
    }
}