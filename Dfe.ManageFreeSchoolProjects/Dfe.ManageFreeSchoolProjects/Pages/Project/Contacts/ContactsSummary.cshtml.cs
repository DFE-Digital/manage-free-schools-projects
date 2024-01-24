using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Contacts;

public class ContactsSummaryModel : PageModel
{
    private readonly IGetProjectContactsService _getProjectContactsService;
    
    private readonly ErrorService _errorService;
    
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    public ContactsSummaryModel(IGetProjectContactsService getProjectContactsService, ErrorService errorService)
    {
        _getProjectContactsService = getProjectContactsService;
        _errorService = errorService;
    }
    
    
}