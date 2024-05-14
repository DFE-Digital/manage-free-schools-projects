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
using Dfe.ManageFreeSchoolProjects.Extensions;
using System.Reflection;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Contacts;

public class EditContactModel : PageModel
{
    private readonly IGetContactsService _getContactsService;
    
    private readonly IAddContactsService _addContactsService;
    
    private readonly IGetProjectOverviewService _getProjectOverviewService;
    
    private readonly ILogger<EditContactModel> _logger;
    
    private readonly ErrorService _errorService;
    
    [BindProperty(Name = "projectId")]
    public string ProjectId { get; set; }

    [BindProperty(Name = "contactType")]
    public string ContactType { get; set; }

    [BindProperty(Name = "contact-name")]
    [ValidText(100)]
    [DisplayName("Contact name")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string ContactName { get; set; }

    [BindProperty(Name = "contact-email")]
    [DisplayName("Contact email")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string ContactEmail { get; set; }
    
    [BindProperty]
    public GetContactsResponse PageContacts { get; set; }
    
    public string SchoolName { get; set; }
    
    public string GetNextPage()
    {
        return string.Format(RouteConstants.ViewContacts, ProjectId);
    }

    public EditContactModel(IGetContactsService getContactsService,IGetProjectOverviewService projectOverviewService,IAddContactsService addContactsService,ErrorService errorService, ILogger<EditContactModel> logger )
    {
        _getContactsService = getContactsService;
        _errorService = errorService;
        _addContactsService = addContactsService;
        _getProjectOverviewService = projectOverviewService;
        _logger = logger;
    }
    
    public async Task<IActionResult> OnGet()
    {
        _logger.LogMethodEntered();

        try
        {
            var contactType = RouteData.Values["contactType"] as string;
            ContactType = contactType;
            var projectId = RouteData.Values["projectId"] as string;
            ProjectId = projectId;
            PageContacts = await _getContactsService.Execute(projectId);
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

        var contactPropertyName = StringExtensions.KebabToPascalCase(ContactType);
        PropertyInfo contactPropertyInfo = typeof(ContactsTask).GetProperty(contactPropertyName);

        ContactsTask Contacts = new ContactsTask();
        Contact contact = new Contact()
        {
            Name = ContactName,
            Email = ContactEmail
        };

        contactPropertyInfo.SetValue(Contacts, contact);
        PageContacts.Contacts = Contacts;

        var projectId = RouteData.Values["projectId"] as string;
        var project = await _getProjectOverviewService.Execute(projectId);
        ProjectId = projectId;
        SchoolName = project.ProjectStatus.CurrentFreeSchoolName;
        
        if (ContactEmail?.Length > 100)
        {
            ModelState.AddModelError("contact-email", "The contact email must be 100 characters or less");
        }

        string[] internalContactTypes = { "team-lead", "grade-6" };

        if (!IsInternalEmailValid(ContactEmail) && internalContactTypes.Contains(ContactType))
        {
            ModelState.AddModelError("contact-email", "Enter an email address in the correct format. For example, firstname.surname@education.gov.uk");
        }

        if (!IsEmailValid(ContactEmail) && !internalContactTypes.Contains(ContactType))
        {
            ModelState.AddModelError("contact-email", "Enter an email address in the correct format.");
        }
        
        if (ContactName != null && ContactName.Any(char.IsDigit))
        {
            ModelState.AddModelError("contact-name", "The contact name cannot contain numbers");
        }
        
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        UpdateContactsRequest updateContactsRequest = new UpdateContactsRequest();
        updateContactsRequest.Contacts = Contacts;

        await _addContactsService.Execute(ProjectId, updateContactsRequest);

        return Redirect(GetNextPage());
    }
    
    private static bool IsEmailValid(string email)
    {
        return string.IsNullOrEmpty(email) || new EmailAddressAttribute().IsValid(email);
    }

    private static bool IsInternalEmailValid(string email)
    {
        return string.IsNullOrEmpty(email) || (email.Contains("@education.gov.uk") && new EmailAddressAttribute().IsValid(email));
    }
}