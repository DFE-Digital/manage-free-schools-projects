using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using Dfe.ManageFreeSchoolProjects.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dfe.BuildFreeSchools.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

		private readonly IGetDashboardByUserService _getDashboardByUserService;
        private readonly IGetDashboardAllService _getDashboardAllService;
		private readonly ICreateUserService _createUserService;

        public List<GetDashboardResponse> Projects { get; set; }
        public List<GetDashboardResponse> AllProjects { get; set; }

        public IndexModel(IGetDashboardByUserService getDashboardByUserService, IGetDashboardAllService getDashboardAllService, ICreateUserService createUserService)
        {
			_getDashboardByUserService = getDashboardByUserService;
			_getDashboardAllService = getDashboardAllService;
			_createUserService = createUserService;
        }

		public async Task OnGetAsync()
		{
			await LoadPage();
		}

		private async Task<ActionResult> LoadPage()
		{
			try
			{
                Username = User.Identity.Name.ToString();
				await _createUserService.Execute(Username);

                Projects = await _getDashboardByUserService.Execute(Username);
				AllProjects = await _getDashboardAllService.Execute();
				
				return Page();
			}
			catch (Exception ex)
			{
				//_logger.LogError("Case::DetailsPageModel::LoadPage::Exception - {Message}", ex.Message);

				//TempData["Error.Message"] = ErrorOnGetPage;
				return Page();
			}
		}
	}
}