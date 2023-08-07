
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project
{
    public class CreateProjectModel : PageModel
    {
        [BindProperty(Name = "project-id")]
        [Display(Name = "Project ID")]
        [Required]
        [StringLength(10, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string ProjectID { get; set; }

        [BindProperty(Name ="school-name")]
        [Display(Name = "School name")]
        [Required]
        [StringLength(20, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string SchoolName { get; set; }

        [BindProperty(Name = "application-number")]
        [Display(Name = "Application number")]
        [Required]
        [StringLength(10, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string ApplicationNumber { get; set; }

        [BindProperty(Name = "application-wave")]
        [Display(Name = "Application wave")]
        [Required]
        [StringLength(10, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string ApplicationWave { get; set; }

        public ICreateProjectService _createProjectService { get; }
    //    public ILogger<CreateProjectModel> _logger { get; }

        public CreateProjectModel(
            ICreateProjectService createProjectService
         //   ILogger<CreateProjectModel> logger
            )
        {
            _createProjectService = createProjectService;
            //_logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //_logger.LogMethodEntered();

            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var caseUrn = await _createProjectService.CreateProject(ProjectID, SchoolName, ApplicationNumber, ApplicationWave, User.Identity.Name.ToString());

                return Redirect("~/");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "error");

                // TempData["Error.Message"] = ErrorOnPostPage;
            }

            return Page();
        }
    }
}
