using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;

public class FaithStatusModel : CreateProjectBaseModel
{
    [BindProperty(Name = "faith-status")]
    [Required(ErrorMessage = "Select the faith status of the free school")]
    public FaithStatus FaithStatus { get; set; }

    private readonly ICreateProjectCache _createProjectCache;
    private readonly ErrorService _errorService;

    public FaithStatusModel(ICreateProjectCache createProjectCache, ErrorService errorService)
    {
        _createProjectCache = createProjectCache;
        _errorService = errorService;
    }

    public IActionResult OnGet()
    {
        if (!IsUserAuthorised())
        {
            return new UnauthorizedResult();
        }
        
        var project = _createProjectCache.Get();
        FaithStatus = project.FaithStatus;

        BackLink = GetPreviousPage(CreateProjectPageName.FaithStatus, project.Navigation);

        return Page();
    }

    public IActionResult OnPost()
    {
        var project = _createProjectCache.Get();
        BackLink = GetPreviousPage(CreateProjectPageName.FaithStatus, project.Navigation);

        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        if (FaithStatus is FaithStatus.None)
        {
            project.FaithType = FaithType.NotSet;
            project.Navigation = CreateProjectNavigation.Default;
        }
        else
        {
            project.Navigation = CreateProjectNavigation.GoToFaithType;
        }


        project.FaithStatus = FaithStatus;

        _createProjectCache.Update(project);

        return Redirect(GetNextPage(CreateProjectPageName.FaithStatus, project.Navigation, string.Empty ));
    }
}