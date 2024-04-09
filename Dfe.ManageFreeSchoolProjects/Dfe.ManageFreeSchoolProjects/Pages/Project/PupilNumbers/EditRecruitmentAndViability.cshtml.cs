using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Extensions;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.PupilNumbers
{
    public class EditRecruitmentAndViabilityModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        [BindProperty(Name = "reception")]
        public EditRecruitmentAndViabilityRowModel Reception { get; set; }

        [BindProperty(Name = "year7-to-year11")]
        public EditRecruitmentAndViabilityRowModel Year7ToYear11 { get; set; }

        [BindProperty(Name = "year12-to-year14")]
        public EditRecruitmentAndViabilityRowModel Year12ToYear14 { get; set; }

        [BindProperty]
        public string SchoolName { get; set; }

        private readonly IGetPupilNumbersService _getPupilNumbersService;
        private readonly IUpdatePupilNumbersService _updatePupilNumbersService;
        private readonly ErrorService _errorService;

        public EditRecruitmentAndViabilityModel(
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

            Reception = ToRowModel(pupilNumbers.RecruitmentAndViability.ReceptionToYear6);
            Year7ToYear11 = ToRowModel(pupilNumbers.RecruitmentAndViability.Year7ToYear11);
            Year12ToYear14 = ToRowModel(pupilNumbers.RecruitmentAndViability.Year12ToYear14);
            SchoolName = pupilNumbers.SchoolName;

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
                RecruitmentAndViability = new RecruitmentAndViability()
                {
                    ReceptionToYear6 = ToRecruitmentAndViabilityEntry(Reception),
                    Year7ToYear11 = ToRecruitmentAndViabilityEntry(Year7ToYear11),
                    Year12ToYear14 = ToRecruitmentAndViabilityEntry(Year12ToYear14)
                }
            };

            await _updatePupilNumbersService.Execute(ProjectId, request);

            return Redirect(string.Format(RouteConstants.ViewPupilNumbers, ProjectId));
        }

        private static EditRecruitmentAndViabilityRowModel ToRowModel(RecruitmentAndViabilityEntry entry)
        {
            return new EditRecruitmentAndViabilityRowModel()
            {
                MinimumViableNumber = entry.MinimumViableNumber.ToString(),
                ApplicationsReceived = entry.ApplicationsReceived.ToString()
            };
        }

        private static RecruitmentAndViabilityEntry ToRecruitmentAndViabilityEntry(EditRecruitmentAndViabilityRowModel rowModel)
        {
            return new RecruitmentAndViabilityEntry()
            {
                MinimumViableNumber = rowModel.MinimumViableNumber.ToInt(),
                ApplicationsReceived = rowModel.ApplicationsReceived.ToInt()
            };
        }
    }

    public class EditRecruitmentAndViabilityRowModel
    {
        public string MinimumViableNumber { get; set; }
        public string ApplicationsReceived { get; set; }
    }
}
