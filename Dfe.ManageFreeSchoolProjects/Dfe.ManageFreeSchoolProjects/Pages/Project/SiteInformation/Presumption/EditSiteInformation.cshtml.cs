﻿using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Sites;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.SiteInformation.Presumption
{
    public class EditSiteInformationModel : PageModel
    {
        private readonly IGetProjectSitesPresumptionService _getProjectSitesPresumptionService;
        private readonly IUpdateProjectSitePresumptionService _updateProjectSitePresumptionService;
        private readonly ErrorService _errorService;

        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(SupportsGet = true, Name = "siteType")]
        public ProjectSiteType SiteType { get; set; }

        [BindProperty]
        public string SchoolName { get; set; }

        public string ProjectType { get; set; }

        [BindProperty(Name = "address-line1")]
        [Display(Name = "Address line 1")]
        [StringLength(100, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string AddressLine1 { get; set; }

        [BindProperty(Name = "address-line2")]
        [Display(Name = "Address line 2")]
        [StringLength(300, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string AddressLine2 { get; set; }

        [BindProperty(Name = "town-or-city")]
        [Display(Name = "Town or city")]
        [StringLength(100, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string TownOrCity { get; set; }

        [BindProperty(Name = "postcode")]
        [Display(Name = "Postcode")]
        [StringLength(10, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string Postcode { get; set; }

        [BindProperty(Name = "date-planning-permission-obtained", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Date planning permission obtained")]
        public DateTime? DatePlanningPermissionObtained { get; set; }

        [BindProperty(Name = "start-date-of-site-occupation", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Start date of site occupation")]
        public DateTime? StartDateOfSiteOccupation { get; set; }


        public EditSiteInformationModel(
            IGetProjectSitesPresumptionService getProjectSitesPresumptionService,
            IUpdateProjectSitePresumptionService updateProjectSitePresumptionService,
            ErrorService errorService)
        {
            _getProjectSitesPresumptionService = getProjectSitesPresumptionService;
            _updateProjectSitePresumptionService = updateProjectSitePresumptionService;
            _errorService = errorService;
        }

        public async Task<IActionResult> OnGet()
        {

            var sites = await _getProjectSitesPresumptionService.Execute(ProjectId);

            ProjectType = sites.ProjectType;

            if (ProjectType == "Central Route")
            {
                return new NotFoundResult();
            }

            var site = GetProjectSite(sites);

            AddressLine1 = site.Address.AddressLine1;
            AddressLine2 = site.Address.AddressLine2;
            TownOrCity = site.Address.TownOrCity;
            Postcode = site.Address.Postcode;
            StartDateOfSiteOccupation = site.StartDateOfSiteOccupation;
            DatePlanningPermissionObtained = site.DatePlanningPermissionObtained;
            SchoolName = sites.SchoolName;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var sites = await _getProjectSitesPresumptionService.Execute(ProjectId);

            ProjectType = sites.ProjectType;

            var updateRequest = new UpdateProjectSitePresumptionRequest
            {
                Address = new ProjectSiteAddress
                {
                    AddressLine1 = AddressLine1,
                    AddressLine2 = AddressLine2,
                    TownOrCity = TownOrCity,
                    Postcode = Postcode
                },
                StartDateOfSiteOccupation = StartDateOfSiteOccupation,
                DatePlanningPermissionObtained = DatePlanningPermissionObtained
            };

            await _updateProjectSitePresumptionService.Execute(ProjectId, updateRequest, SiteType);

            return Redirect(string.Format(RouteConstants.ViewSiteInformation, ProjectId, ProjectType));
        }

        /// <summary>
        /// To avoid having to write another API for getting a single site
        /// This figures out which site is being requested based on the requested route
        /// </summary>
        /// <param name="sites"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private ProjectSite GetProjectSite(GetProjectSitesPresumptionResponse sites)
        {
            if (SiteType == ProjectSiteType.Permanent)
            {
                return sites.PermanentSite;
            }

            if (SiteType == ProjectSiteType.Temporary)
            {
                return sites.TemporarySite;
            }

            throw new ArgumentException($"Invalid site type");
        }
    }
}