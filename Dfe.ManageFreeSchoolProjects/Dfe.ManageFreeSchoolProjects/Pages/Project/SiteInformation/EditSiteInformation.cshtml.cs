using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Sites;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.SiteInformation
{
    public class EditSiteInformationModel : PageModel
    {
        private readonly IGetProjectSitesService _getProjectSitesService;
        private readonly IUpdateProjectSitesService _updateProjectSitesService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public string SchoolName { get; set; }

        [BindProperty(Name = "address-line-1")]
        [Display(Name = "Address line 1")]
        [StringLength(10, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string AddressLine1 { get; set; }

        [BindProperty(Name = "address-line-2")]
        [Display(Name = "Address line 2")]
        [StringLength(10, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string AddressLine2 { get; set; }

        [BindProperty(Name = "postcode")]
        [Display(Name = "Postcode")]
        [StringLength(10, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string Postcode { get; set; }


        public EditSiteInformationModel(
            IGetProjectSitesService getProjectSitesService,
            IUpdateProjectSitesService updateProjectSitesService)
        {
            _getProjectSitesService = getProjectSitesService;
            _updateProjectSitesService = updateProjectSitesService;
        }

        public async Task<IActionResult> OnGet()
        {
            var sites = await _getProjectSitesService.Execute(ProjectId);

            var site = sites.TemporarySite;
            AddressLine1 = site.Address.AddressLine1;
            AddressLine2 = site.Address.AddressLine2;
            Postcode = site.Address.Postcode;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var existingSites = await _getProjectSitesService.Execute(ProjectId);

            existingSites.TemporarySite.Address.AddressLine1 = AddressLine1;
            existingSites.TemporarySite.Address.AddressLine2 = AddressLine2;
            existingSites.TemporarySite.Address.Postcode = Postcode;

            var updateRequest = new UpdateProjectSitesRequest
            {
                PermanentSite = existingSites.PermanentSite,
                TemporarySite = existingSites.TemporarySite
            };

            await _updateProjectSitesService.Execute(ProjectId, updateRequest);

            return Redirect(string.Format(RouteConstants.ViewSiteInformation, ProjectId));
        }
    }
}
