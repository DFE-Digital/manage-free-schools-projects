using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Task.School
{
    public class EditSchoolTaskModel : PageModel
    {
        private readonly IGetProjectService _getProjectService;
        private readonly IUpdateProjectTaskService _updateProjectTaskService;
        private readonly ILogger<EditSchoolTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "school-type")]
        [Display(Name = "School type")]
        [Required]
        public string SchoolType { get; set; }

        [BindProperty(Name = "school-phase")]
        [Display(Name = "School phase")]
        [Required]
        public string SchoolPhase { get; set; }

        [BindProperty(Name = "age-range")]
        [Display(Name = "Age range")]
        [Required]
        public string AgeRange { get; set; }

        [BindProperty(Name = "nursery")]
        [Display(Name = "Nursery")]
        [Required]
        public string Nursery { get; set; }

        [BindProperty(Name = "sixth-form")]
        [Display(Name = "Sixth form")]
        [Required]
        public string SixthForm { get; set; }

        [BindProperty(Name = "company-name")]
        [Display(Name = "Company name")]
        [Required]
        public string CompanyName { get; set; }

        [BindProperty(Name = "number-of-company-members")]
        [Display(Name = "Number of company members")]
        [Required]
        public string NumberOfCompanyMembers { get; set; }

        [BindProperty(Name = "proposed-chair-of-trustees")]
        [Display(Name = "Proposed chair of trustees")]
        [Required]
        public string ProposedChairOfTrustees { get; set; }

        public EditSchoolTaskModel(
            IGetProjectService getProjectService,
            IUpdateProjectTaskService updateProjectTaskService,
            ILogger<EditSchoolTaskModel> logger,
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

            try
            {
                var project = await _getProjectService.Execute(ProjectId);
                SchoolType = project.SchoolType;
                SchoolPhase = project.SchoolPhase;
                AgeRange = project.AgeRange;
                Nursery = project.Nursery;
                SixthForm = project.SixthForm;
                CompanyName = project.CompanyName;
                NumberOfCompanyMembers = project.NumberOfCompanyMembers;
                ProposedChairOfTrustees = project.ProposedChairOfTrustees;
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
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

            try
            {
                var request = new UpdateProjectTasksRequest()
                {
                    Tasks = new ProjectTaskRequest()
                    {
                        School = new SchoolTaskRequest()
                        {
                            SchoolType = SchoolType,
                            SchoolPhase = SchoolPhase,
                            AgeRange = AgeRange,
                            Nursery = Nursery,
                            SixthForm = SixthForm,
                            CompanyName = CompanyName,
                            NumberOfCompanyMembers = NumberOfCompanyMembers,
                            ProposedChairOfTrustees = ProposedChairOfTrustees
                        }
                    }
                };

                await _updateProjectTaskService.Execute(ProjectId, request);

                return Redirect($"/projects/{ProjectId}/tasks/school");
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
            }

            return Page();
        }
    }
}
