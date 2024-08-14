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

public class EditTrustContactModel : PageModel
{
    private readonly IGetContactsService _getContactsService;
    
    private readonly IAddContactsService _addContactsService;
    
    private readonly IGetProjectOverviewService _getProjectOverviewService;
    
    private readonly ILogger<EditTrustContactModel> _logger;
    
    private readonly ErrorService _errorService;
    
    [BindProperty(Name = "projectId")]
    public string ProjectId { get; set; }
    

    [BindProperty(Name = "trust-contact-name")]
    [ValidText(100)]
    [DisplayName("Trust contact name")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string TrustContactName { get; set; }

    [BindProperty(Name = "trust-contact-email")]
    [DisplayName("Trust contact email")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string TrustContactEmail { get; set; }

    [BindProperty(Name = "trust-contact-phone-number")]
    [ValidPhoneNumber]
    [DisplayName("Trust contact phone number")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string TrustContactPhoneNumber { get; set; }

    [BindProperty(Name = "trust-contact-role")]
    [ValidText(100)]
    [DisplayName("Trust contact role")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]

    public string TrustContactRole { get; set; }
    
    [BindProperty]
    public GetContactsResponse PageContacts { get; set; }
    
    public string SchoolName { get; set; }
    
    public string GetNextPage()
    {
        return string.Format(RouteConstants.Contacts, ProjectId);
    }

    public EditTrustContactModel(IGetContactsService getContactsService,IGetProjectOverviewService projectOverviewService,IAddContactsService addContactsService,ErrorService errorService, ILogger<EditTrustContactModel> logger )
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
                TrustContact = new Contact()
                {
                    Name = TrustContactName,
                    Email = TrustContactEmail,
                    PhoneNumber = TrustContactPhoneNumber,
                    Role = TrustContactRole
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

        if (!IsNamePopulated(TrustContactName))
        {
            ModelState.AddModelError("trust-contact-name", "Enter the full name, for example John Smith");
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        if (TrustContactName.Any(char.IsDigit))
        {
            ModelState.AddModelError("trust-contact-name", "Trust contact name must not include numbers");
        }

        if (TrustContactEmail?.Length > 100)
        {
            ModelState.AddModelError("trust-contact-email", "Trust contact email must be 100 characters or less");
        }

        if (!IsEmailValid(TrustContactEmail))
        {
            ModelState.AddModelError("trust-contact-email", "Enter an email address in the correct format, like firstname.surname@outlook.com");
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
                TrustContact = new Contact()
                {
                    Name = TrustContactName,
                    Email = TrustContactEmail,
                    PhoneNumber = TrustContactPhoneNumber,
                    Role = TrustContactRole
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