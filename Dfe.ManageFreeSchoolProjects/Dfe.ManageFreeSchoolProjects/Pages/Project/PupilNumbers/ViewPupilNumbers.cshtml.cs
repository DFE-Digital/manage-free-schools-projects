using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.PupilNumbers
{
    public class ViewPupilNumbersModel(IGetPupilNumbersService getPupilNumbersService, IGetProjectByTaskSummaryService getProjectByTaskSummaryService) : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public GetPupilNumbersResponse PupilNumbers { get; set; }

        public ProjectByTaskSummaryResponse ProjectTaskListSummary { get; set; }

        [BindProperty(SupportsGet = true, Name = "fromSaveForm")]
        public bool ShowBanner { get; set; }

        public async Task<IActionResult> OnGet()
        {
            PupilNumbers = await getPupilNumbersService.Execute(ProjectId);

            PupilNumbers.CapacityWhenFull.Total = PupilNumbers.CapacityWhenFull.ReceptionToYear6 +
                                                  PupilNumbers.CapacityWhenFull.Year7ToYear11 +
                                                  PupilNumbers.CapacityWhenFull.Year12ToYear14;

            ProjectTaskListSummary = await getProjectByTaskSummaryService.Execute(ProjectId);

            return Page();
        }
    }
}