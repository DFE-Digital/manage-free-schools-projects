using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dfe.ManageFreeSchoolProjects.Extensions;
using Dfe.ManageFreeSchoolProjects.Services.Dashboard;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Utils;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create
{
    public class LocalAuthorityModel : PageModel
    {
        [BindProperty(Name = "local-authority")]
        [Display(Name = "Local Authority")]
        [Required(ErrorMessage = "Select the local authority of the free school.")]
        public string LocalAuthority { get; set; }

        [BindProperty(Name = "local-authorities")]
        public List<string> LocalAuthorities { get; set; }
        public string BackLink { get; set; }
        
        private readonly ErrorService _errorService;
        private readonly ICreateProjectCache _createProjectCache;
        private readonly IGetLocalAuthoritiesService _getLocalAuthoritiesService;

        public LocalAuthorityModel(ErrorService errorService, ICreateProjectCache createProjectCache,
            IGetLocalAuthoritiesService getLocalAuthoritiesService)
        {
            _errorService = errorService;
            _createProjectCache = createProjectCache;
            _getLocalAuthoritiesService = getLocalAuthoritiesService;
        }

        public async Task<ActionResult> OnGet()
        {
            if (!User.IsInRole(RolesConstants.ProjectRecordCreator))
            {
                return new UnauthorizedResult();
            }

            var project = _createProjectCache.Get();
            
            var localAuthorities = await GetLocalAuthoritiesByRegion();
            LocalAuthorities = localAuthorities.Values.ToList();
            project.LocalAuthorities = localAuthorities;

            if (!string.IsNullOrEmpty(project.LocalAuthority))
                LocalAuthority = project.LocalAuthority;
            
            _createProjectCache.Update(project);

            BackLink = CreateProjectBackLinkHelper.GetBackLink(project.Navigation, RouteConstants.CreateProjectRegion);
            
            return Page();
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LocalAuthorities = _createProjectCache.Get().LocalAuthorities.Values.ToList();
                _errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            var project = _createProjectCache.Get();
            
            project.LocalAuthority = LocalAuthority;
            project.LocalAuthorityCode = project.LocalAuthorities.SingleOrDefault(x => x.Value == LocalAuthority).Key;
            
            _createProjectCache.Update(project);

            return Redirect(RouteConstants.CreateNotifyUser);
        }

        private async Task<Dictionary<string, string>> GetLocalAuthoritiesByRegion()
        {
            var region = _createProjectCache.Get().Region.ToDescription();
            var response = await _getLocalAuthoritiesService.Execute(new List<string> { region });

            var authorities = new Dictionary<string, string>();
            
            response.LocalAuthorities.ForEach(authority =>
            {
                authorities.Add(authority.LACode, authority.Name);
            });

            return authorities;
        }
    }
}