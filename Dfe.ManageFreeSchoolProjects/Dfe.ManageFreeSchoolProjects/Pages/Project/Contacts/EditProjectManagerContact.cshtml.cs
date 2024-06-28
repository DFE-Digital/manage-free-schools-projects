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

public class EditProjectManagerContactModel : PageModel
{
    private readonly IGetContactsService _getContactsService;
    
    private readonly IAddContactsService _addContactsService;
    
    private readonly IGetProjectOverviewService _getProjectOverviewService;
    
    private readonly ILogger<EditProjectManagerContactModel> _logger;
    
    private readonly ErrorService _errorService;
    
    [BindProperty(Name = "projectId")]
    public string ProjectId { get; set; }
    

    [BindProperty(Name = "project-manager-name")]
    [ValidText(100)]
    [DisplayName("Project manager name")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string ProjectManagerName { get; set; }

    [BindProperty(Name = "project-manager-email")]
    [DisplayName("Project manager email")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    
    public string ProjectManagerEmail { get; set; }
    
    [BindProperty]
    public GetContactsResponse PageContacts { get; set; }
    
    public string SchoolName { get; set; }
    
    public string GetNextPage()
    {
        return string.Format(RouteConstants.Contacts, ProjectId);
    }

    public EditProjectManagerContactModel(IGetContactsService getContactsService,IGetProjectOverviewService projectOverviewService,IAddContactsService addContactsService,ErrorService errorService, ILogger<EditProjectManagerContactModel> logger )
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
                ProjectManager = new Contact()
                {
                    Name = ProjectManagerName,
                    Email = ProjectManagerEmail
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

        if (!IsNamePopulated(ProjectManagerName))
        {
            ModelState.AddModelError("project-manager-name", "Enter the full name, for example John Smith");
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        if (ProjectManagerName.Any(char.IsDigit))
        {
            ModelState.AddModelError("project-manager-name", "Project manager name must not include numbers");
        }

        if (ProjectManagerEmail?.Length > 100)
        {
            ModelState.AddModelError("project-manager-email", "Project manager email must be 100 characters or less");
        }

        if (!IsEmailValid(ProjectManagerEmail))
        {
            ModelState.AddModelError("project-manager-email", "Enter an email address in the correct format, like firstname.surname@outlook.com");
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
                ProjectManager = new Contact()
                {
                    Name = ProjectManagerName,
                    Email = ProjectManagerEmail
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