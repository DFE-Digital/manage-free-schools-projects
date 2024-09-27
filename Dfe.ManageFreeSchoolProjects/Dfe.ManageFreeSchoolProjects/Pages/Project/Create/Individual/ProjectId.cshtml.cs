using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    public class ProjectIdModel(
        ErrorService errorService,
        ICreateProjectCache createProjectCache,
        IGetProjectOverviewService getProjectOverviewService)
        : CreateProjectBaseModel(createProjectCache)
    {
        [BindProperty(Name = "projectid")]
        [Display(Name = "Temporary project ID")]
        [Required(ErrorMessage = "Enter the temporary project ID")]
        [StringLength(25, ErrorMessage = ValidationConstants.TextValidationMessage)]
        public string ProjectId { get; set; }

        public IActionResult OnGet()
        {
            if (!IsUserAuthorised())
            {
                return new UnauthorizedResult();
            }


            var project = CreateProjectCache.Get();
            ProjectId = project.ProjectId;
            
            BackLink = GetPreviousPage(CreateProjectPageName.ProjectId);
           
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var project = CreateProjectCache.Get();
            BackLink = GetPreviousPage(CreateProjectPageName.ProjectId);

            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            if (ProjectId.Contains(' '))
            {
                ModelState.AddModelError("projectid", "Temporary project ID must not include spaces");
                errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            if (Regex.Match(ProjectId,  "[^a-zA-Z\\d\\s:]", RegexOptions.None, TimeSpan.FromSeconds(5)).Success)
            {
                ModelState.AddModelError("projectid", "Temporary project ID must only include numbers and letters");
                errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }

            try
            {
                //Attempt to get project, will throw an exception when 404 is returned
                await getProjectOverviewService.Execute(ProjectId);
                ModelState.AddModelError("projectid", "This temporary project ID already exists. Enter a different ID");
                errorService.AddErrors(ModelState.Keys, ModelState);
                return Page();
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    throw;
                }
            }

            project.ProjectId = ProjectId;
            CreateProjectCache.Update(project);

            return Redirect(GetNextPage(CreateProjectPageName.ProjectId));
        }
    }
}
