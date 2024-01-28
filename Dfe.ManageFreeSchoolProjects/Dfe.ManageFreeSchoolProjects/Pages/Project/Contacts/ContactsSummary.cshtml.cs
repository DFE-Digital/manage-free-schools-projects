using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Contacts;

public class ContactsSummaryModel : PageModel
{
    private readonly IGetContactsService _getContactsService;
    
    private readonly ILogger<ContactsSummaryModel> _logger;
    
    private readonly ErrorService _errorService;
    
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    
    public GetContactsResponse Contacts;
   public ContactsSummaryModel(IGetContactsService getContactsService,ErrorService errorService, ILogger<ContactsSummaryModel> logger )
    {
        _getContactsService = getContactsService;
        _errorService = errorService;
    }

   public async Task<IActionResult> OnGet()
   {
      // _logger.LogMethodEntered("ddsadsa");

       try
       {
           var projectId = RouteData.Values["projectId"] as string;

           Contacts = await _getContactsService.Execute(projectId);
       }
       catch (Exception ex)
       {
           _logger.LogErrorMsg(ex);
       }

       return Page();
   }


}