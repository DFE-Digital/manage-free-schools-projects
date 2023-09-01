using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Pages.Project.Task.School;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.Construction
{
    public class EditPropertyTaskModel : PageModel
    {
        private readonly IGetProjectByTaskService _getProjectService;
        private readonly IUpdateProjectByTaskService _updateProjectTaskService;
        private readonly ILogger<EditSchoolTaskModel> _logger;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "name-of-site")]
        [Display(Name = "Name of site")]
        [Required]
        public string NameOfSite { get; set; }

        [BindProperty(Name = "address-of-site")]
        [Display(Name = "Address of site")]
        [Required]
        public string AddressOfSite { get; set; }

        [BindProperty(Name = "postcode-of-site")]
        [Display(Name = "Postcode of site")]
        [Required]
        public string PostcodeOfSite { get; set; }

        [BindProperty(Name = "building-type")]
        [Display(Name = "Building type")]
        [Required]
        public string BuildingType { get; set; }

        [BindProperty(Name = "trust-ref")]
        [Display(Name = "Trust ref")]
        [Required]
        public string TrustRef { get; set; }

        [BindProperty(Name = "trust-lead-sponsor")]
        [Display(Name = "Trust lead sponsor")]
        [Required]
        public string TrustLeadSponsor { get; set; }

        [BindProperty(Name = "trust-name")]
        [Display(Name = "Trust name")]
        [Required]
        public string TrustName { get; set; }

        [BindProperty(Name = "site-min-area")]
        [Display(Name = "Site min area")]
        [Required]
        public string SiteMinArea { get; set; }

        [BindProperty(Name = "type-of-works-location")]
        [Display(Name = "Type of works location")]
        [Required]
        public string TypeOfWorksLocation { get; set; }

        public EditPropertyTaskModel(
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

        public async Task<IActionResult> OnGet()
        {
            _logger.LogMethodEntered();

            try
            {
                var project = await _getProjectService.Execute(ProjectId);
                NameOfSite = project.Construction.NameOfSite;
                AddressOfSite = project.Construction.AddressOfSite;
                PostcodeOfSite = project.Construction.PostcodeOfSite;
                BuildingType = project.Construction.BuildingType;
                TrustRef = project.Construction.TrustRef;
                TrustLeadSponsor = project.Construction.TrustLeadSponsor;
                TrustName = project.Construction.TrustName;
                SiteMinArea = project.Construction.SiteMinArea;
                TypeOfWorksLocation = project.Construction.TypeofWorksLocation;

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
                    Construction = new ConstructionTask()
                    {
                        NameOfSite = NameOfSite,
                        AddressOfSite = AddressOfSite,
                        PostcodeOfSite = PostcodeOfSite,
                        BuildingType = BuildingType,
                        TrustRef = TrustRef,
                        TrustLeadSponsor = TrustLeadSponsor,
                        TrustName = TrustName,
                        SiteMinArea = SiteMinArea,
                        TypeofWorksLocation = TypeOfWorksLocation
                    }
                };

                await _updateProjectTaskService.Execute(ProjectId, request);

                return Redirect(string.Format(RouteConstants.ViewConstructionTask, ProjectId));
            }
            catch (Exception ex)
            {
                _logger.LogErrorMsg(ex);
                throw;
            }
        }
    }
}
