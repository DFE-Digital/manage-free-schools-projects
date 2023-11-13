﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;

public class NotifyUser : PageModel
{
    private readonly ErrorService _errorService;
    private readonly ICreateProjectCache _createProjectCache;

    //TODO: Error message TBD, talk to A
    [Required]
    [BindProperty(Name = "email")]
    public string Email { get; set; }

    public string BackLink { get; set; }

    public NotifyUser(ErrorService errorService, ICreateProjectCache createProjectCache)
    {
        _errorService = errorService;
        _createProjectCache = createProjectCache;
    }

    public IActionResult OnGet()
    {
        if (!User.IsInRole(RolesConstants.ProjectRecordCreator))
        {
            return new UnauthorizedResult();
        }

        var projectCache = _createProjectCache.Get();
        BackLink = CreateProjectBackLinkHelper.GetBackLink(projectCache.Navigation,
            RouteConstants.CreateProjectLocalAuthority);

        Email = projectCache.EmailToNotify;

        return Page();
    }


    public IActionResult OnPost()
    {
        if (!IsEmailValid(Email))
        {
            ModelState.AddModelError("email", "Email is not valid.");
        }

        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        var projectCache = _createProjectCache.Get();
        projectCache.EmailToNotify = Email;
        _createProjectCache.Update(projectCache);
        
        return Redirect(RouteConstants.CreateProjectCheckYourAnswers);
    }

    private static bool IsEmailValid(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}