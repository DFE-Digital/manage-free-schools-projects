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

public class EditSchoolChairContactModel : PageModel
{
    private readonly IGetContactsService _getContactsService;
    
    private readonly IAddContactsService _addContactsService;
    
    private readonly IGetProjectOverviewService _getProjectOverviewService;
    
    private readonly ILogger<EditSchoolChairContactModel> _logger;
    
    private readonly ErrorService _errorService;
    
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    
    [BindProperty(SupportsGet = true, Name = "school-chair-name")]
    [ValidText(100)]
    [DisplayName("School chair name")]
    public string SchoolChairName { get; set; }

    [BindProperty(SupportsGet = true, Name = "school-chair-email")]
    [DisplayName("School chair email")]
    
    public string SchoolChairEmail { get; set; }
    
    [BindProperty]
    public GetContactsResponse PageContacts { get; set; }
    
    public string SchoolName { get; set; }
    
    public string GetNextPage()
    {
        return string.Format(RouteConstants.ViewContacts, ProjectId);
    }

    public EditSchoolChairContactModel(IGetContactsService getContactsService,IGetProjectOverviewService projectOverviewService,IAddContactsService addContactsService,ErrorService errorService, ILogger<EditSchoolChairContactModel> logger )
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
            var projectId = RouteData.Values["projectId"] as string;

            PageContacts = await _getContactsService.Execute(projectId);
            
            var project = await _getProjectOverviewService.Execute(projectId);
            SchoolName = project.SchoolDetails.TrustName;
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
                SchoolChairOfGovernorsEmail = SchoolChairEmail,
                SchoolChairOfGovernorsName = SchoolChairName
            }
        };
        
        var projectId = RouteData.Values["projectId"] as string;
        var project = await _getProjectOverviewService.Execute(projectId);
        SchoolName = project.SchoolDetails.TrustName;

        if (SchoolChairEmail == null)
        {
            ModelState.AddModelError("school-chair-email", "Enter a valid email.");
        }
        
        if (SchoolChairEmail?.Length > 100)
        {
            ModelState.AddModelError("school-chair-email", "The school chair email must be 100 characters or less.");
        }
        
        if (!IsEmailValid(SchoolChairEmail))
        {
            ModelState.AddModelError("school-chair-email", "Enter an email address in the correct format.");
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }

        if (SchoolChairName == null)
        {
            ModelState.AddModelError("school-chair-name", "Enter a school chair name.");
        }
        
        if (SchoolChairName != null && SchoolChairName.Any(char.IsDigit))
        {
            ModelState.AddModelError("school-chair-name", "school chair name cannot contain numbers.");
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
                SchoolChairOfGovernorsName = SchoolChairName,
                SchoolChairOfGovernorsEmail = SchoolChairEmail
            }
            
        };

        await _addContactsService.Execute(ProjectId, updateContactsRequest);

        return Redirect(GetNextPage());
    }
    
    private static bool IsEmailValid(string email)
    {
        return email != null && new EmailAddressAttribute().IsValid(email);
    }
}