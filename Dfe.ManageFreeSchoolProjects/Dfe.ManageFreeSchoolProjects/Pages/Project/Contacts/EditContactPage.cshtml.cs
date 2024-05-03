using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Contacts;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Validators;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Dfe.ManageFreeSchoolProjects.Extensions;
using System.Reflection;

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

    [BindProperty(Name = "contact")]
    public string Contact { get; set; }

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
            var contact = RouteData.Values["contact"] as string;
            Contact = contact;
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

        var contactNamePropertyName = StringExtensions.KebabToPascalCase(Contact) + "Name";
        var contactEmailPropertyName = StringExtensions.KebabToPascalCase(Contact) + "Email";
        PropertyInfo contactNameProperty = typeof(ContactsTask).GetProperty(contactNamePropertyName);
        PropertyInfo contactEmailProperty = typeof(ContactsTask).GetProperty(contactEmailPropertyName);

        GetContactsResponse PageContacts = new GetContactsResponse();
        ContactsTask Contacts = new ContactsTask();
        contactNameProperty.SetValue(Contacts, ContactName);
        contactEmailProperty.SetValue(Contacts, ContactEmail);
        PageContacts.Contacts = Contacts;
        
        var projectId = RouteData.Values["projectId"] as string;
        var project = await _getProjectOverviewService.Execute(projectId);
        ProjectId = projectId;
        SchoolName = project.ProjectStatus.CurrentFreeSchoolName;
        
        if (ContactEmail?.Length > 100)
        {
            ModelState.AddModelError("contact-email", "Email must be 100 characters or less");
        }
        
        if (!IsEmailValid(ContactEmail))
        {
            ModelState.AddModelError("contact-email", "Enter an email address in the correct format");
        }
        
        if (ContactName != null && ContactName.Any(char.IsDigit))
        {
            ModelState.AddModelError("contact-name", "Name cannot contain numbers");
        }
        
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        UpdateContactsRequest updateContactsRequest = new UpdateContactsRequest();
        ContactsTask UpdateContacts = new ContactsTask();
        contactNameProperty.SetValue(UpdateContacts, ContactName);
        contactEmailProperty.SetValue(UpdateContacts, ContactEmail);
        updateContactsRequest.Contacts = UpdateContacts;


        await _addContactsService.Execute(ProjectId, updateContactsRequest);

        return Redirect(GetNextPage());
    }
    
    private static bool IsEmailValid(string email)
    {
        return string.IsNullOrEmpty(email) || new EmailAddressAttribute().IsValid(email);
    }
}