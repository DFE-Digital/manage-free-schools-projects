using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Risk
{
    public class AddRiskBaseModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "risk-rating")]
        [Display(Name = "risk rating")]
        [Required]
        public string RiskRating { get; set; }

        [BindProperty(Name = "summary")]
        [Display(Name = "summary")]
        [StringLength(1000, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string Summary { get; set; }

        public string SchoolName { get; set; }

        public string GetNextPage(RiskPageName currentRiskPageName, CreateRiskCacheItem cacheItem)
        {
            if (cacheItem.HasReachedCheckRiskPage)
            {
                return $"/projects/{ProjectId}/risk/check/add";
            }

            switch (currentRiskPageName)
            {
                case RiskPageName.GovernanceAndSuitability:
                    return $"/projects/{ProjectId}/risk/education/add";
                case RiskPageName.Overall:
                    return $"/projects/{ProjectId}/risk/check/add";
                case RiskPageName.Finance:
                    return $"/projects/{ProjectId}/risk/risk-appraisal-form/add";
                case RiskPageName.Education:
                    return $"/projects/{ProjectId}/risk/finance/add";
                case RiskPageName.SharepointLink:
                    return $"/projects/{ProjectId}/risk/overall/add";
                default:
                    throw new ArgumentOutOfRangeException($"Unsupported risk page {currentRiskPageName}");
            }
        }
    }
}
