using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Models;


namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.CommissionedExternalExpert
{
    public class EditCommissionedExternalExpertTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditCommissionedExternalExpertTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }
        
        [BindProperty(Name = "commissioned-external-expert-visit")]
        
        public bool? CommissionedExternalExpertVisit { get; set; }
        
        [BindProperty(Name = "external-expert-visit-date", BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "Date the EE visit took place")]
        
        public DateTime? ExternalExpertVisitDate { get; set; }
        
        [BindProperty(Name = "saved-to-workplaces")]
        
        public bool? SavedToWorkplaces { get; set; }
        public string SchoolName { get; set; }

        public EditCommissionedExternalExpertTaskModel(IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditCommissionedExternalExpertTaskModel> logger,
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
            var project = await _getProjectService.Execute(ProjectId, TaskName.CommissionedExternalExpert);
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
                    CommissionedExternalExpert= new CommissionedExternalExpertTask()
                    {
                        CommissionedExternalExpertVisit = CommissionedExternalExpertVisit,
                        ExternalExpertVisitDate = ExternalExpertVisitDate,
                        SavedExternalExpertSpecsToWorkplacesFolder = SavedToWorkplaces
                    }
                };

                await _updateProjectTaskService.Execute(ProjectId, request);
                return Redirect(string.Format(RouteConstants.ViewCommissionedExternalExpertTask, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.CommissionedExternalExpert);

            CommissionedExternalExpertVisit  = project.CommissionedExternalExpert.CommissionedExternalExpertVisit;
            ExternalExpertVisitDate = project.CommissionedExternalExpert.ExternalExpertVisitDate;
            SavedToWorkplaces = project.CommissionedExternalExpert.SavedExternalExpertSpecsToWorkplacesFolder;
            
            SchoolName = project.SchoolName;
        }
    }
}