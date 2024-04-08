using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.PupilNumbers
{
    public class EditCapacityWhenFullModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "nursery")]
        public int Nursery { get; set; }

        [BindProperty(Name = "reception-to-year6")]
        public int ReceptionToYear6 { get; set; }

        [BindProperty(Name = "year7-to-year11")]
        public int Year7ToYear11 { get; set; }

        [BindProperty(Name = "year12-to-year14")]
        public int Year12ToYear14 { get; set; }

        [BindProperty(Name = "special-education-needs")]
        public int SpecialEducationNeeds { get; set; }

        [BindProperty(Name = "alternative-provision")]
        public int AlternativeProvision { get; set; }

        private readonly IGetPupilNumbersService _getPupilNumbersService;
        private readonly IUpdatePupilNumbersService _updatePupilNumbersService;

        public EditCapacityWhenFullModel(
            IGetPupilNumbersService getPupilNumbersService,
            IUpdatePupilNumbersService updatePupilNumbersService)
        {
            _getPupilNumbersService = getPupilNumbersService;
            _updatePupilNumbersService = updatePupilNumbersService;
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

            return RedirectToPage("/Project/PupilNumbers/EditCapacityWhenFull", new { projectId = ProjectId });
        }
    }
}
