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

		public IGetDashboardByUserService _getDashboardByUserService;
		public ICreateUserService _createUserService;

		public List<GetDashboardByUserResponse> Projects { get; set; }

        public IndexModel(
			IGetDashboardByUserService getDashboardByUserService,
			ICreateUserService createUserService)
        {
			_getDashboardByUserService = getDashboardByUserService;
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