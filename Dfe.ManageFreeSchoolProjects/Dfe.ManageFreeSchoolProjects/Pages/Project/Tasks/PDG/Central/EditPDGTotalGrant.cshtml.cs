using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class EditPDGTotalGrant : PageModel
{
    public string CurrentFreeSchoolName { get; set; }
    
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }
    public decimal? TotalGrantAmount { get; set; }

    private string InitialGrant { get; set; }
    private string RevisedGrant { get; set; }
    private readonly IGetProjectByTaskService _getProjectService;
    private readonly ILogger<EditPDGPaymentScheduleModel> _logger;


    public EditPDGTotalGrant(IGetProjectByTaskService getProjectService, ILogger<EditPDGPaymentScheduleModel> logger)
    {
        _getProjectService = getProjectService;
        _logger = logger;
    }


    public void OnGet()
    {
    }
}