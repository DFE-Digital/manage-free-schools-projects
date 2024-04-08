using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.PupilNumbers
{
    public class EditCapacityWhenFullModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "nursery")]
        [ValidNumberPupilNumbers]
        public int Nursery { get; set; }

        [BindProperty(Name = "reception-to-year6")]
        [ValidNumberPupilNumbers]
        [Display(Name = "Reception to year 6")]
        public int ReceptionToYear6 { get; set; }

        [BindProperty(Name = "year7-to-year11")]
        [ValidNumberPupilNumbers]
        [Display(Name = "Year 7 to year 11")]
        public int Year7ToYear11 { get; set; }

        [BindProperty(Name = "year12-to-year14")]
        [ValidNumberPupilNumbers]
        [Display(Name = "Year 12 to year 14")]
        public int Year12ToYear14 { get; set; }

        [BindProperty(Name = "special-education-needs")]
        [ValidNumberPupilNumbers]
        [Display(Name = "Special educational needs")]
        public int SpecialEducationNeeds { get; set; }

        [BindProperty(Name = "alternative-provision")]
        [ValidNumberPupilNumbers]
        [Display(Name = "Alternative provision")]
        public int AlternativeProvision { get; set; }

        [BindProperty(Name = "school-name")]
        public string SchoolName { get; set; }

        private readonly IGetPupilNumbersService _getPupilNumbersService;
        private readonly IUpdatePupilNumbersService _updatePupilNumbersService;
        private readonly ErrorService _errorService;

        public EditCapacityWhenFullModel(
            IGetPupilNumbersService getPupilNumbersService,
            IUpdatePupilNumbersService updatePupilNumbersService,
            ErrorService errorService)
        {
            _getPupilNumbersService = getPupilNumbersService;
            _updatePupilNumbersService = updatePupilNumbersService;
            _errorService = errorService;
        }

        public async Task<IActionResult> OnGet()
        {
            var pupilNumbers = await _getPupilNumbersService.Execute(ProjectId);

            Nursery = pupilNumbers.CapacityWhenFull.Nursery;
            ReceptionToYear6 = pupilNumbers.CapacityWhenFull.ReceptionToYear6;
            Year7ToYear11 = pupilNumbers.CapacityWhenFull.Year7ToYear11;
            Year12ToYear14 = pupilNumbers.CapacityWhenFull.Year12ToYear14;
            SpecialEducationNeeds = pupilNumbers.CapacityWhenFull.SpecialEducationNeeds;
            AlternativeProvision = pupilNumbers.CapacityWhenFull.AlternativeProvision;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var request = new UpdatePupilNumbersRequest()
            {
                CapacityWhenFull = new CapacityWhenFull()
                {
                    Nursery = Nursery,
                    ReceptionToYear6 = ReceptionToYear6,
                    Year7ToYear11 = Year7ToYear11,
                    Year12ToYear14 = Year12ToYear14,
                    SpecialEducationNeeds = SpecialEducationNeeds,
                    AlternativeProvision = AlternativeProvision
                }
            };

            await _updatePupilNumbersService.Execute(ProjectId, request);

            return Redirect(string.Format(RouteConstants.ViewPupilNumbers, ProjectId));
        }
    }
}
