using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.Extensions.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG;
using Dfe.ManageFreeSchoolProjects.Validators;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central
{
    public class EditPDGPaymentScheduleModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditPDGPaymentScheduleModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public string CurrentFreeSchoolName { get; set; }

        public EditPDGPaymentScheduleModel(IGetProjectByTaskService getProjectService, IUpdateProjectByTaskService updateProjectTaskService, ILogger<EditPDGPaymentScheduleModel> logger,
            ErrorService errorService)
        {
            _getProjectService = getProjectService;
            _logger = logger;
            _errorService = errorService;
            _updateProjectTaskService = updateProjectTaskService;
        }

        public async Task<ActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            await LoadProject();
            return Page();
        }

        public ActionResult OnPost()
        {
            try
            {
                return Redirect(string.Format(RouteConstants.AddPDGPaymentCentral, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.PaymentSchedule);

            CurrentFreeSchoolName = project.SchoolName;
        }
    }
}
