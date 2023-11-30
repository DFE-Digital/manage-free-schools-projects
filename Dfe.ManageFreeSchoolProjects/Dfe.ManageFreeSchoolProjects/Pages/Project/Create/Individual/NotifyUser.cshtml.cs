using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual;

public class NotifyUser : CreateProjectBaseModel
{
    private readonly ErrorService _errorService;
    private readonly ICreateProjectCache _createProjectCache;

    [Required(ErrorMessage = "Please enter an email.")]
    [BindProperty(Name = "email")]
    public string Email { get; set; }
    
    public NotifyUser(ErrorService errorService, ICreateProjectCache createProjectCache)
    {
        _errorService = errorService;
        _createProjectCache = createProjectCache;
    }

    public IActionResult OnGet()
    {
        if (!IsUserAuthorised())
        {
            return new UnauthorizedResult();
        }


        var projectCache = _createProjectCache.Get();

        BackLink = GetPreviousPage(CreateProjectPageName.NotifyUser, projectCache.Navigation);

        Email = projectCache.EmailToNotify;

        return Page();
    }


    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }
        
        if (!IsEmailValid(Email))
        {
            ModelState.AddModelError("email", "Enter an email address in the correct format. For example, firstname.surname@education.gov.uk");
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
        return email != null && email.Contains("@education.gov.uk") && new EmailAddressAttribute().IsValid(email);
    }
}