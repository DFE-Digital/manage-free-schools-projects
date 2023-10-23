using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
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

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Task.School
{
    public class EditSchoolTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditSchoolTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "current-free-school-name")]
        [Display(Name = "Current Free School Name")]
        [Required]
        public string CurrentFreeSchoolName { get; set; }
        
        [BindProperty(Name = "school-type")]
        [Display(Name = "School type")]
        [Required]
        public string SchoolType { get; set; }

        [BindProperty(Name = "school-phase")]
        [Display(Name = "School phase")]
        [Required]
        public string SchoolPhase { get; set; }

        [BindProperty(Name = "age-range-from")]
        [Display(Name = "Age range from")]
        [Required]
        public string AgeRangeFrom { get; set; }
        
        [BindProperty(Name = "age-range-to")]
        [Display(Name = "Age range to")]
        [Required]
        public string AgeRangeTo { get; set; }

        [BindProperty(Name = "gender")]
        [Display(Name = "Gender")]
        [Required]
        public string Gender { get; set; }
        
        [BindProperty(Name = "nursery")]
        [Display(Name = "Nursery")]
        [Required]
        public string Nursery { get; set; }
        
        [BindProperty(Name = "sixth-form")]
        [Display(Name = "Sixth form")]
        [Required]
        public string SixthForm { get; set; }
        
        [BindProperty(Name = "faith-status")]
        [Display(Name = "Faith status")]
        [Required]
        public string FaithStatus { get; set; }

        [BindProperty(Name = "faith-type")]
        [Display(Name = "Faith type")]
        public string FaithType { get; set; }
        
        
        [BindProperty(Name = "other-faith-type")]
        [Display(Name = "Other Faith type")]
        public string OtherFaithType { get; set; }


        public EditSchoolTaskModel(
            IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
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
                SchoolType = project.School.SchoolType;
                SchoolPhase = project.School.SchoolPhase;
                AgeRange = project.School.AgeRange;
                Nursery = project.School.Nursery;
                SixthForm = project.School.SixthForm;
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
                var request = new UpdateProjectByTaskRequest()
                {
                    School = new SchoolTask()
                    {
                        SchoolType = SchoolType,
                        SchoolPhase = SchoolPhase,
                        AgeRange = AgeRange,
                        Nursery = Nursery,
                        SixthForm = SixthForm,
                    }
                };

                await _updateProjectTaskService.Execute(ProjectId, request);

                return Redirect(string.Format(RouteConstants.ViewSchoolTask, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }
    }
}
