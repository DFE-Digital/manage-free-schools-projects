using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.PupilNumbers
{
    public class ViewPupilNumbersModel : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public GetPupilNumbersResponse PupilNumbers { get; set; }

        [BindProperty(SupportsGet = true, Name = "fromSaveForm")]
        public bool ShowBanner { get; set; }

        private readonly IGetPupilNumbersService _getPupilNumbersService;

        public ViewPupilNumbersModel(IGetPupilNumbersService getPupilNumbersService)
        {
            _getPupilNumbersService = getPupilNumbersService;
        }

        public async Task<IActionResult> OnGet()
        {
            PupilNumbers = await _getPupilNumbersService.Execute(ProjectId);

            return Page();
        }
    }
}
