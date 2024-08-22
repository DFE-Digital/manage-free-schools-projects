using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PDG.Central;

public class EditGrantTotal : PageModel
{
    public string CurrentFreeSchoolName { get; set; }

    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    [BindProperty(Name = "total-grant-amount")]
    public decimal? GrantTotalAmount { get; set; }

    [TempData] private decimal? InitialGrant { get; set; }

    [TempData] private decimal? RevisedGrant { get; set; }

    private readonly IGetProjectByTaskService _getProjectService;
    private readonly ILogger<EditPDGPaymentScheduleModel> _logger;
    private readonly IUpdateProjectByTaskService _updateProjectTaskService;

    public EditGrantTotal(IGetProjectByTaskService getProjectService, ILogger<EditPDGPaymentScheduleModel> logger,
        IUpdateProjectByTaskService updateProjectTaskService)
    {
        _getProjectService = getProjectService;
        _logger = logger;
        _updateProjectTaskService = updateProjectTaskService;
    }


    public async Task<IActionResult> OnGet()
    {
        _logger.LogMethodEntered();

        try
        {
            var project = await _getProjectService.Execute(ProjectId, TaskName.PDG);

            var initialGrant = project.PDGDashboard.InitialGrant;
            var revisedGrant = project.PDGDashboard.RevisedGrant;

            InitialGrant = initialGrant;
            RevisedGrant = revisedGrant;
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
        var pdgGrantTask = new PDGGrantTask();

        if (GrantTotalAmount == null)
        {
            pdgGrantTask.InitialGrant = InitialGrant;
            pdgGrantTask.RevisedGrant = InitialGrant ?? RevisedGrant;
        }
        else
        {
            pdgGrantTask.RevisedGrant = GrantTotalAmount;
        }

        var request = new UpdateProjectByTaskRequest { PDGGrantTask = pdgGrantTask };

        await _updateProjectTaskService.Execute(ProjectId, request);

        return Redirect(string.Format(RouteConstants.ViewPDGCentral, ProjectId));
    }
}