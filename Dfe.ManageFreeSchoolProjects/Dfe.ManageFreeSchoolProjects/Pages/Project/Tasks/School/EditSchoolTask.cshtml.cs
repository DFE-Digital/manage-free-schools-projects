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
        [FreeSchoolNameValidator]
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
        [StringLengthValidator(2, ErrorMessage = ValidationConstants.TextValidationMessage)]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number.")]
        [Required]
        public string AgeRangeFrom { get; set; }

        [BindProperty(Name = "age-range-to")]
        [Display(Name = "Age range to")]
        [StringLengthValidator(2, ErrorMessage = ValidationConstants.TextValidationMessage)]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        [Required]
        public string AgeRangeTo { get; set; }

        [BindProperty(Name = "gender")]
        [Display(Name = "Gender")]
        [Required]
        public Gender Gender { get; set; }

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
        public FaithStatus FaithStatus { get; set; }

        [BindProperty(Name = "faith-type")]
        [Display(Name = "Faith type")]
        public FaithType FaithType { get; set; }

        [BindProperty(Name = "other-faith-type")]
        [Display(Name = "Other Faith type")]
        public string OtherFaithType { get; set; }

        public bool DisplayFaithType => FaithStatus is not (FaithStatus.None or FaithStatus.NotSet);

        public bool DisplayOtherFaithType => FaithType is FaithType.Other;

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
                CurrentFreeSchoolName = project.School.CurrentFreeSchoolName;
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
            if (int.TryParse(AgeRangeTo, out int ageRangeTo) && int.TryParse(AgeRangeFrom, out int ageRangeFrom))
            {
                if (ageRangeFrom > ageRangeTo)
                    ModelState.AddModelError("age-range-from", "'Age range from' must be less than 'Age range to'");
            }

            if (DisplayFaithType && FaithType == FaithType.NotSet)
            {
                ModelState.AddModelError("faith-type", "Faith type is required.");
            }

            if (DisplayOtherFaithType && string.IsNullOrEmpty(OtherFaithType))
            {
                ModelState.AddModelError("other-faith-type", "Other faith type is required.");
            }
            else if (FaithType != FaithType.Other && !string.IsNullOrEmpty(OtherFaithType))
            {
                OtherFaithType = string.Empty;
            }

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
                CurrentFreeSchoolName = CurrentFreeSchoolName,
                SchoolType = SchoolType,
                SchoolPhase = SchoolPhase,
                Nursery = Nursery,
                SixthForm = SixthForm,
                Gender = Gender,
                FaithStatus = FaithStatus,
                FaithType = FaithType,
                OtherFaithType = OtherFaithType,
                AgeRange = string.Concat(AgeRangeFrom, "-", AgeRangeTo)
            };
        }

        private static string[] SplitAgeRange(string ageRange)
        {
            return ageRange.Split('-');
        }
    }
}