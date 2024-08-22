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
    [ValidMoney(0, int.MaxValue)] //TODO: find out what the max is, if any
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

            var initialGrant = project.PDGDashboard.InitialGrant;
            var revisedGrant = project.PDGDashboard.RevisedGrant;

            TempData["InitialGrant"] = initialGrant?.ToString(CultureInfo.InvariantCulture);
            TempData["RevisedGrant"] = revisedGrant?.ToString(CultureInfo.InvariantCulture);

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
        if (!ModelState.IsValid)
        {
            _errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }
        
        var pdgGrantTask = new PDGGrantTask();
        
        var initialGrant = SafeStringToNullableDecimal(TempData["InitialGrant"]?.ToString());
        var revisedGrant = SafeStringToNullableDecimal(TempData["RevisedGrant"]?.ToString());


        if (GrantTotalAmount == null)
        {
            pdgGrantTask.InitialGrant = initialGrant;
            pdgGrantTask.RevisedGrant = initialGrant ?? revisedGrant;
        }
        else
        {
            pdgGrantTask.RevisedGrant = GrantTotalAmount;
        }
        
        var request = new UpdateProjectByTaskRequest { PDGGrantTask = pdgGrantTask };

        await _updateProjectTaskService.Execute(ProjectId, request);

        return Redirect(string.Format(RouteConstants.ViewPDGCentral, ProjectId));
    }
    
    private static decimal? SafeStringToNullableDecimal(string input)
    {
        if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
        {
            return result;
        }
        return null;
    }
}