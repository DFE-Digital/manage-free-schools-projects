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
using Dfe.ManageFreeSchoolProjects.Extensions;

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
        public SchoolType SchoolType { get; set; }

        [BindProperty(Name = "school-phase")]
        [Display(Name = "School phase")]
        [Required]
        public SchoolPhase SchoolPhase { get; set; }

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
        public Gender Gender { get; set; }

        [BindProperty(Name = "nursery")]
        [Display(Name = "Nursery")]
        [Required]
        public Nursery? Nursery { get; set; } 
        
        [BindProperty(Name = "sixth-form")]
        [Display(Name = "Sixth form")]
        [Required]
        public SixthForm? SixthForm { get; set; }
        
        [BindProperty(Name = "faith-status")]
        [Display(Name = "Faith status")]
        [Required]
        public FaithStatus FaithStatus { get; set; }

        [BindProperty(Name = "faith-type")]
        [Display(Name = "Faith type")]
        public FaithType FaithType { get; set; }

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
                Nursery = project.School.Nursery;
                SixthForm = project.School.SixthForm;
                Gender = project.School.Gender;
                SixthForm = project.School.SixthForm;
                FaithStatus = project.School.FaithStatus;
                FaithType = project.School.FaithType;
                OtherFaithType = project.School.OtherFaithType;
                
                if (!string.IsNullOrEmpty(project.School.AgeRange))
                {
                    var ageRanges = SplitAgeRange(project.School.AgeRange);
                    AgeRangeFrom = ageRanges[0];
                    AgeRangeTo = ageRanges[1];
                }
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
                var request = new UpdateProjectByTaskRequest
                {
                    School = CreateUpdatedSchoolTask()
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

        private SchoolTask CreateUpdatedSchoolTask()
        {
            return new SchoolTask
            {
                SchoolType = SchoolType,
                SchoolPhase = SchoolPhase,
                Nursery = Nursery,
                SixthForm = SixthForm, 
                Gender = Gender,
                FaithStatus = FaithStatus, 
                FaithType = FaithType, 
                OtherFaithType = OtherFaithType
            };
        }
        
        private static string[] SplitAgeRange(string ageRange)
        {
            return ageRange.Split('-');
        }
    }
}
