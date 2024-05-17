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

public class EditTeamLeadContactModel : PageModel
{
    private readonly IGetContactsService _getContactsService;
    
    private readonly IAddContactsService _addContactsService;
    
    private readonly IGetProjectOverviewService _getProjectOverviewService;
    
    private readonly ILogger<EditTeamLeadContactModel> _logger;
    
    private readonly ErrorService _errorService;
    
    [BindProperty(Name = "projectId")]
    public string ProjectId { get; set; }
    

    [BindProperty(Name = "team-lead-name")]
    [ValidText(100)]
    [DisplayName("Team lead name")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string TeamLeadName { get; set; }

    [BindProperty(Name = "team-lead-email")]
    [DisplayName("Team lead email")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    
    public string TeamLeadEmail { get; set; }
    
    [BindProperty]
    public GetContactsResponse PageContacts { get; set; }
    
    public string SchoolName { get; set; }
    
    public string GetNextPage()
    {
        return string.Format(RouteConstants.Contacts, ProjectId);
    }

    public EditTeamLeadContactModel(IGetContactsService getContactsService,IGetProjectOverviewService projectOverviewService,IAddContactsService addContactsService,ErrorService errorService, ILogger<EditTeamLeadContactModel> logger )
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
                TeamLead = new Contact()
                {
                    Name = TeamLeadName,
                    Email = TeamLeadEmail
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

        if (!IsNamePopulated(TeamLeadName))
        {
            ModelState.AddModelError("team-lead-name", "Enter the full name, for example John Smith");
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        if (TeamLeadName.Any(char.IsDigit))
        {
            ModelState.AddModelError("team-lead-name", "The team lead name cannot contain numbers");
        }

        if (!IsEducationEmailValid(TeamLeadEmail))
        {
            ModelState.AddModelError("team-lead-email", "Enter an email address in the correct format. For example, firstname.surname@education.gov.uk");
        }

        if (TeamLeadEmail?.Length > 100)
        {
            ModelState.AddModelError("team-lead-email", "The team lead email must be 100 characters or less");
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
                TeamLead = new Contact()
                {
                    Name = TeamLeadName,
                    Email = TeamLeadEmail
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