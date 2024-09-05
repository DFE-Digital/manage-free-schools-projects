using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.DueDiligenceChecks;

public class EditDueDiligenceChecks(IGetProjectByTaskService getProjectService) : PageModel
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


    public void OnGet()
    {
    }

    private async Task LoadProject()
    {
        var project = await getProjectService.Execute(ProjectId, TaskName.DueDiligenceChecks);

        ReceivedChairOfTrusteesCountersignedCertificate =
            project.DueDiligenceChecks.ReceivedChairOfTrusteesCountersignedCertificate;

        NonSpecialistChecksDoneOnAllTrustMembersAndTrustees =
            project.DueDiligenceChecks.NonSpecialistChecksDoneOnAllTrustMembersAndTrustees;

        RequestedCounterExtremismChecks = project.DueDiligenceChecks.RequestedCounterExtremismChecks;

        SchoolName = project.SchoolName;
    }
}