using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Contacts;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Contacts;

public class EditProjectAssignedToContactModel : PageModel
{
    private readonly IGetContactsService _getContactsService;
    
    private readonly IAddContactsService _addContactsService;
    
    private readonly IGetProjectOverviewService _getProjectOverviewService;
    
    private readonly ILogger<EditProjectAssignedToContactModel> _logger;
    
    private readonly ErrorService _errorService;
    
    [BindProperty(Name = "projectId")]
    public string ProjectId { get; set; }

    [Required(ErrorMessage = "Please enter the name.")]
    [BindProperty(Name = "project-assigned-to-name")]
    [ValidText(100)]
    [DisplayName("Project assigned to name")]
    public string ProjectAssignedToName { get; set; }

    [Required(ErrorMessage = "Please enter an email.")]
    [BindProperty(Name = "project-assigned-to-email")]
    [DisplayName("Project assigned to email")]
    
    public string ProjectAssignedToEmail { get; set; }
    
    [BindProperty]
    public GetContactsResponse PageContacts { get; set; }
    
    public string SchoolName { get; set; }
    
    public string GetNextPage()
    {
        return string.Format(RouteConstants.Contacts, ProjectId);
    }

    public EditProjectAssignedToContactModel(IGetContactsService getContactsService,IGetProjectOverviewService projectOverviewService,IAddContactsService addContactsService,ErrorService errorService, ILogger<EditProjectAssignedToContactModel> logger )
    {
        _getContactsService = getContactsService;
        _getProjectOverviewService = projectOverviewService;
        _errorService = errorService;
        _addContactsService = addContactsService;
        _logger = logger;
    }
    
    public async Task<IActionResult> OnGet()
    {
        _logger.LogMethodEntered();

        try
        {
            var projectId = RouteData.Values["projectId"] as string;
            PageContacts = await _getContactsService.Execute(projectId);
            ProjectId = projectId;
            var project = await _getProjectOverviewService.Execute(projectId);
            SchoolName = project.ProjectStatus.CurrentFreeSchoolName;
        }
        catch (Exception ex)
        {
            _logger.LogErrorMsg(ex);
        }

        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        PageContacts = new GetContactsResponse()
        {
            Contacts = new ContactsTask()
            {
                ProjectAssignedTo = new Contact()
                {
                    Name = ProjectAssignedToName,
                    Email = ProjectAssignedToEmail
                }
            }
        };

        var projectId = RouteData.Values["projectId"] as string;
        var project = await _getProjectOverviewService.Execute(projectId);
        ProjectId = projectId;
        SchoolName = project.ProjectStatus.CurrentFreeSchoolName;

        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        if (!IsNamePopulated(ProjectAssignedToName))
        {
            ModelState.AddModelError("project-assigned-to-name", "Enter the full name, for example John Smith");
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        if (ProjectAssignedToName.Any(char.IsDigit))
        {
            ModelState.AddModelError("project-assigned-to-name", "The project assigned to name cannot contain numbers");
        }

        if (ProjectAssignedToEmail?.Length > 100)
        {
            ModelState.AddModelError("project-assigned-to-email", "The project assigned to email must be 100 characters or less");
        }

        if (!IsEducationEmailValid(ProjectAssignedToEmail))
        {
            ModelState.AddModelError("project-assigned-to-email", "Enter an email address in the correct format. For example, firstname.surname@education.gov.uk");
        }

        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        var updateContactsRequest = new UpdateContactsRequest()
        {
            Contacts = new ContactsTask()
            {
                ProjectAssignedTo = new Contact()
                {
                    Name = ProjectAssignedToName,
                    Email = ProjectAssignedToEmail
                }
            }
            
        };

        await _addContactsService.Execute(ProjectId, updateContactsRequest);

        return Redirect(GetNextPage());
    }
    private static bool IsNamePopulated(string name)
    {
        return name != null && name.Contains(' ');
    }

    private static bool IsEducationEmailValid(string email)
    {
        return string.IsNullOrEmpty(email) || (email.Contains("@education.gov.uk") && new EmailAddressAttribute().IsValid(email));
    }
}