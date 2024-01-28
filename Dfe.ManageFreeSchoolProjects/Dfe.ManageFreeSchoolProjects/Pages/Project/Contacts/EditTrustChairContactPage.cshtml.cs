using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Contacts;
using Dfe.ManageFreeSchoolProjects.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Contacts;

public class EditTrustChairContactModel : PageModel
{
    private readonly IGetContactsService _getContactsService;
    
    private readonly IAddContactsService _addContactsService;
    
    private readonly ILogger<EditTrustChairContactModel> _logger;
    
    private readonly ErrorService _errorService;
    
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    
    [BindProperty(SupportsGet = true, Name = "trust-chair-name")]
    [ValidText(100)]
    [DisplayName("Trust chair name")]
    public string TrustChairName { get; set; }

    [BindProperty(SupportsGet = true, Name = "trust-chair-email")]
    [DisplayName("Trust chair email")]
    
    public string TrustChairEmail { get; set; }
    
    [BindProperty]
    public GetContactsResponse PageContacts { get; set; }
    
    public string GetNextPage()
    {
        return string.Format(RouteConstants.ViewContacts, ProjectId);
    }

    public EditTrustChairContactModel(IGetContactsService getContactsService,IAddContactsService addContactsService,ErrorService errorService, ILogger<EditTrustChairContactModel> logger )
    {
        _getContactsService = getContactsService;
        _errorService = errorService;
        _addContactsService = addContactsService;
    }
    
    public async Task<IActionResult> OnGet()
    {
        //_logger.LogMethodEntered();

        try
        {
            var projectId = RouteData.Values["projectId"] as string;

            PageContacts = await _getContactsService.Execute(projectId);
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
                ChairOfGovernorsEmail = TrustChairEmail,
                ChairOfGovernorsName = TrustChairName
            }
        };
        

        if (TrustChairEmail == null)
        {
            ModelState.AddModelError("trust-chair-email", "Enter a valid email.");
        }
        
        if (TrustChairEmail?.Length > 100)
        {
            ModelState.AddModelError("trust-chair-email", "The trust chair email must be 100 characters or less.");
        }
        
        if (!IsEmailValid(TrustChairEmail))
        {
            ModelState.AddModelError("email", "Enter an email address in the correct format.");
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        if (TrustChairName == null)
        {
            ModelState.AddModelError("trust-chair-name", "Enter a trust chair name.");
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
                ChairOfGovernorsName = TrustChairName,
                ChairOfGovernorsEmail = TrustChairEmail
            }
            
        };

        _addContactsService.Execute(ProjectId, updateContactsRequest);

        return Redirect(GetNextPage());
    }
    
    private static bool IsEmailValid(string email)
    {
        return email != null && new EmailAddressAttribute().IsValid(email);
    }
}