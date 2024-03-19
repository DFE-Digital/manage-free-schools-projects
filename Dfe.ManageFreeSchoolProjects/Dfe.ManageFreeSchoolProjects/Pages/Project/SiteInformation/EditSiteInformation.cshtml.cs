using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Sites;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
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

        [BindProperty(SupportsGet = true, Name = "siteType")]
        public ProjectSiteType SiteType { get; set; }

        public string SchoolName { get; set; }

        [BindProperty(Name = "address-line1")]
        [Display(Name = "Address line 1")]
        [StringLength(10, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string AddressLine1 { get; set; }

        [BindProperty(Name = "address-line2")]
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

            var site = GetProjectSite(sites);

            AddressLine1 = site.Address.AddressLine1;
            AddressLine2 = site.Address.AddressLine2;
            Postcode = site.Address.Postcode;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var updateRequest = new UpdateProjectSitesRequest
            {
                Site = new ProjectSite
                {
                    Address = new ProjectSiteAddress
                    {
                        AddressLine1 = AddressLine1,
                        AddressLine2 = AddressLine2,
                        Postcode = Postcode
                    }
                }
            };

            await _updateProjectSitesService.Execute(ProjectId, updateRequest, SiteType);

            return Redirect(string.Format(RouteConstants.ViewSiteInformation, ProjectId));
        }

        private ProjectSite GetProjectSite(GetProjectSitesResponse sites)
        {
            if (SiteType == ProjectSiteType.Permanent)
            {
                return sites.PermanentSite;
            }

            if (SiteType == ProjectSiteType.Temporary)
            {
                return sites.TemporarySite;
            }

            throw new ArgumentException($"Invalid site type {SiteType}");
        }
    }
}
