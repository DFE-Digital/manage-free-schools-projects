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

public class EditProjectDirectorContactModel : PageModel
{
    private readonly IGetContactsService _getContactsService;
    
    private readonly IAddContactsService _addContactsService;
    
    private readonly IGetProjectOverviewService _getProjectOverviewService;
    
    private readonly ILogger<EditProjectDirectorContactModel> _logger;
    
    private readonly ErrorService _errorService;
    
    [BindProperty(Name = "projectId")]
    public string ProjectId { get; set; }
    

    [BindProperty(Name = "project-director-name")]
    [ValidText(100)]
    [DisplayName("Project director name")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string ProjectDirectorName { get; set; }

    [BindProperty(Name = "project-director-email")]
    [DisplayName("Project director email")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    
    public string ProjectDirectorEmail { get; set; }
    
    [BindProperty]
    public GetContactsResponse PageContacts { get; set; }
    
    public string SchoolName { get; set; }
    
    public string GetNextPage()
    {
        return string.Format(RouteConstants.Contacts, ProjectId);
    }

    public EditProjectDirectorContactModel(IGetContactsService getContactsService,IGetProjectOverviewService projectOverviewService,IAddContactsService addContactsService,ErrorService errorService, ILogger<EditProjectDirectorContactModel> logger )
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
                ProjectDirector = new Contact()
                {
                    Name = ProjectDirectorName,
                    Email = ProjectDirectorEmail
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

        if (!IsNamePopulated(ProjectDirectorName))
        {
            ModelState.AddModelError("project-director-name", "Enter the full name, for example John Smith");
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        if (ProjectDirectorName.Any(char.IsDigit))
        {
            ModelState.AddModelError("project-director-name", "The project director name cannot contain numbers");
        }

        if (ProjectDirectorEmail?.Length > 100)
        {
            ModelState.AddModelError("project-director-email", "The project director email must be 100 characters or less");
        }

        if (!IsEmailValid(ProjectDirectorEmail))
        {
            ModelState.AddModelError("project-director-email", "Enter an email address in the correct format.");
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
                ProjectDirector = new Contact()
                {
                    Name = ProjectDirectorName,
                    Email = ProjectDirectorEmail
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

    private static bool IsEmailValid(string email)
    {
        return string.IsNullOrEmpty(email) || new EmailAddressAttribute().IsValid(email);
    }
}