using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class EditGrantTotal : PageModel
{
    public string CurrentFreeSchoolName { get; set; }

    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    [BindProperty(Name = "total-grant-amount", BinderType = typeof(DecimalInputModelBinder))]
    [ValidMoney(0, 640000)]
    [Display(Name = "Total amount")]
    public decimal? GrantTotalAmount { get; set; }
    
    private readonly IGetProjectByTaskService _getProjectService;
    private readonly ILogger<EditPDGPaymentScheduleModel> _logger;
    private readonly IUpdateProjectByTaskService _updateProjectTaskService;
    private readonly ErrorService _errorService;

    public EditGrantTotal(IGetProjectByTaskService getProjectService, ILogger<EditPDGPaymentScheduleModel> logger,
        IUpdateProjectByTaskService updateProjectTaskService, ErrorService errorService)
    {
        _getProjectService = getProjectService;
        _logger = logger;
        _updateProjectTaskService = updateProjectTaskService;
        _errorService = errorService;
    }

    public async Task<IActionResult> OnGet()
    {
        _logger.LogMethodEntered();

        try
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.PDG);

            CurrentFreeSchoolName = project.SchoolName;

            var initialGrant = project.PDGDashboard.InitialGrant;
            var revisedGrant = project.PDGDashboard.RevisedGrant;

            GrantTotalAmount = revisedGrant ?? initialGrant;
        }
        catch (Exception e)
        {
            _logger.LogErrorMsg(e);
            throw;
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var project = await _getProjectService.Execute(ProjectId, TaskName.PDG);

        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            CurrentFreeSchoolName = project.SchoolName;
            return Page();
        }

        var pdgGrantTask = new PDGGrantTask();
        
        var initialGrant = project.PDGDashboard.InitialGrant;
        
        if (initialGrant == null)
        {
            pdgGrantTask.InitialGrant = GrantTotalAmount;
        }
        else
        {
            if (GrantTotalAmount == null)
            {
                _errorService.AddError("total-grant-amount", "Total amount cannot be empty once a number has been saved");
                return Page();
            }

            pdgGrantTask.InitialGrant = initialGrant;
            pdgGrantTask.RevisedGrant = GrantTotalAmount;
        }
        
        var request = new UpdateProjectByTaskRequest { PDGGrantTask = pdgGrantTask };

        await _updateProjectTaskService.Execute(ProjectId, request);

        return Redirect(string.Format(RouteConstants.ViewPDGCentral, ProjectId));
    }
}