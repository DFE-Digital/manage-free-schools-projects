using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.DueDiligenceChecks;

public class EditDueDiligenceChecks(
    IGetProjectByTaskService getProjectService,
    IUpdateProjectByTaskService updateProjectByTaskService,
    ILogger<EditDueDiligenceChecks> logger,
    ErrorService errorService) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "projectId")]
    public string ProjectId { get; set; }

    public string SchoolName { get; set; }

    [BindProperty(Name = "received-chair-of-trustees-countersigned-certificate")]
    public bool? ReceivedChairOfTrusteesCountersignedCertificate { get; set; }

    [BindProperty(Name = "non-specialist-checks-done-on-all-trust-members-and-trustees")]
    public bool? NonSpecialistChecksDoneOnAllTrustMembersAndTrustees { get; set; }

    [BindProperty(Name = "request-counter-extremism-checks")]
    public bool? RequestedCounterExtremismChecks { get; set; }

    [BindProperty(Name = "date-when-all-checks-completed", BinderType = typeof(DateInputModelBinder))]
    [DisplayName("Date when all checks completed")]
    public DateTime? DateWhenAllChecksWereCompleted { get; set; }

    [BindProperty(Name = "saved-the-non-specialist-check-spreadsheet-in-workplaces")]
    public bool? SavedNonSpecialistChecksSpreadsheetInWorkplaces { get; set; }

    [BindProperty(Name = "deleted-any-copies-of-chairs-dbs-certificate")]
    public bool? DeletedAnyCopiesOfChairsDBSCertificate { get; set; }

    [BindProperty(Name = "deleted-emails-containing-suitability-and-declaration-forms")]
    public bool? DeletedEmailsContainingSuitabilityAndDeclarationForms { get; set; }

    public async Task<IActionResult> OnGet()
    {
        logger.LogMethodEntered();
        
        await LoadProject();

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        logger.LogMethodEntered();

        if (!ModelState.IsValid)
        {
            errorService.AddErrors(ModelState.Keys, ModelState);
            return Page();
        }
        
        try
        {
            await updateProjectByTaskService.Execute(ProjectId, CreateUpdateProjReq());

            return Redirect(string.Format(RouteConstants.ViewDueDiligenceChecks, ProjectId));
        }
        catch (Exception e)
        {
            logger.LogErrorMsg(e);
            throw;
        }
    }

    private async Task LoadProject()
    {
        var project = await getProjectService.Execute(ProjectId, TaskName.DueDiligenceChecks);

        ReceivedChairOfTrusteesCountersignedCertificate =
            project.DueDiligenceChecks.ReceivedChairOfTrusteesCountersignedCertificate;

        NonSpecialistChecksDoneOnAllTrustMembersAndTrustees =
            project.DueDiligenceChecks.NonSpecialistChecksDoneOnAllTrustMembersAndTrustees;

        RequestedCounterExtremismChecks = project.DueDiligenceChecks.RequestedCounterExtremismChecks;

        DateWhenAllChecksWereCompleted = project.DueDiligenceChecks.DateWhenAllChecksWereCompleted;

        SavedNonSpecialistChecksSpreadsheetInWorkplaces =
            project.DueDiligenceChecks.SavedNonSpecialistChecksSpreadsheetInWorkplaces;

        DeletedEmailsContainingSuitabilityAndDeclarationForms =
            project.DueDiligenceChecks.DeletedEmailsContainingSuitabilityAndDeclarationForms;

        SchoolName = project.SchoolName;
    }

    private UpdateProjectByTaskRequest CreateUpdateProjReq()
    {
        return new UpdateProjectByTaskRequest
        {
            DueDiligenceChecks = new API.Contracts.Project.Tasks.DueDiligenceChecks
            {
                ReceivedChairOfTrusteesCountersignedCertificate = ReceivedChairOfTrusteesCountersignedCertificate,
                NonSpecialistChecksDoneOnAllTrustMembersAndTrustees =
                    NonSpecialistChecksDoneOnAllTrustMembersAndTrustees,
                RequestedCounterExtremismChecks = RequestedCounterExtremismChecks,
                DateWhenAllChecksWereCompleted = DateWhenAllChecksWereCompleted,
                SavedNonSpecialistChecksSpreadsheetInWorkplaces = SavedNonSpecialistChecksSpreadsheetInWorkplaces,
                DeletedAnyCopiesOfChairsDBSCertificate = SavedNonSpecialistChecksSpreadsheetInWorkplaces,
                DeletedEmailsContainingSuitabilityAndDeclarationForms =
                    DeletedEmailsContainingSuitabilityAndDeclarationForms
            }
        };
    }
}