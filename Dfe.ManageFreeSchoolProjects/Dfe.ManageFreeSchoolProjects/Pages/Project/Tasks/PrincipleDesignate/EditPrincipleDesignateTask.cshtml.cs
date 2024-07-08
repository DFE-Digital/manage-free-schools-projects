using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Validators;
using Dfe.ManageFreeSchoolProjects.Constants;
using System.ComponentModel;
using System.Linq;
using DocumentFormat.OpenXml.Drawing.Diagrams;


namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PrincipleDesignate
{
    public class EditPrincipleDesignateTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditPrincipleDesignateTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }


        [BindProperty(Name = "trust-appointed-principle-designate")]
        
        public string? TrustAppointedPrincipleDesignate { get; set; }

        [BindProperty(Name = "trust-appointed-principle-designate-date", BinderType = typeof(DateInputModelBinder))]
        [Display(Name = "trust appointed principle designate date")]
        public DateTime? TrustAppointedPrincipleDesignateDate { get; set; }
        
        [BindProperty(Name = "commissioned-external-expert-visit")]
        
        public bool? CommissionedExternalExpertVisit { get; set; }
        
        [BindProperty]
        public string SchoolName { get; set; }

        public EditPrincipleDesignateTaskModel(
            IGetProjectByTaskService getProjectService,
            IUpdateProjectByTaskService updateProjectTaskService,
            ILogger<EditPrincipleDesignateTaskModel> logger,
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
            var trustAppointedPrincipleDesignate = ConvertYesNo(TrustAppointedPrincipleDesignate);

            if (trustAppointedPrincipleDesignate != YesNo.Yes)
            {
                
              var errorKeys = ModelState.Keys.Where(k => k.StartsWith("trust-appointed-principle-designate-date") || k == "trust-appointed-principle-designate").ToList();
              errorKeys.ForEach(k => ModelState.Remove(k));
              TrustAppointedPrincipleDesignateDate = null;
            }
         

            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }
            
            if (trustAppointedPrincipleDesignate == YesNo.Yes && TrustAppointedPrincipleDesignateDate.HasValue == false)
            {
                ModelState.AddModelError("trust-appointed-principle-designate-date", "Enter the actual date principle designate was appointed");
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var updateTaskRequest = new UpdateProjectByTaskRequest()
            {
                PrincipleDesignate = new PrincipleDesignateTask()
                {
                    TrustAppointedPrincipleDesignateDate = TrustAppointedPrincipleDesignateDate,
                    CommissionedExternalExpertVisitToSchool = CommissionedExternalExpertVisit
                }
            };
            
            await _updateProjectTaskService.Execute(ProjectId, updateTaskRequest);

            return Redirect(string.Format(RouteConstants.ViewPrincipleDesignateTask, ProjectId));
        }

        private async Task LoadProject()
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.PrincipleDesignate);
            
            SchoolName = project.SchoolName;
            TrustAppointedPrincipleDesignateDate = project.PrincipleDesignate.TrustAppointedPrincipleDesignateDate;
            CommissionedExternalExpertVisit = project.PrincipleDesignate.CommissionedExternalExpertVisitToSchool;

            if (project.PrincipleDesignate.TrustAppointedPrincipleDesignateDate.HasValue == true)
            {
                TrustAppointedPrincipleDesignate = YesNo.Yes.ToString();
            }

            else
            {
                TrustAppointedPrincipleDesignate = YesNo.No.ToString();
            }
        }

        private static YesNoNotApplicable? ConvertYesNoNotApplicable(string value)
        {
            return Enum.TryParse<YesNoNotApplicable>(value, true, out var result) ? result : null;
        }

        public static YesNo? ConvertYesNo(string value)
        {
            return Enum.TryParse<YesNo>(value, true, out var result) ? result : null;
        }
    }
}
