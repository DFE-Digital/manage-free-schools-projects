using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Dfe.ManageFreeSchoolProjects.Services.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PreFundingAgreementCheckpointMeeting;

public class ViewPreFundingAgreementCheckpointMeetingModel(
    IGetProjectByTaskService getProjectService,
    IGetTaskStatusService getTaskStatusService,
    IUpdateTaskStatusService updateTaskStatusService,
    IGetProjectRiskService getProjectRiskService,
    ILogger<ViewPreFundingAgreementCheckpointMeetingModel> logger)
    : ViewTaskBaseModel(getProjectService, getTaskStatusService, updateTaskStatusService)
{

    public GetProjectRiskResponse ProjectRisk { get; set; }

    public async Task<ActionResult> OnGet()
    {
        logger.LogMethodEntered();

        await GetTask(TaskName.PreFundingAgreementCheckpointMeeting);

        ProjectRisk = await getProjectRiskService.Execute(ProjectId, 1);

        return Page();
    }

    public async Task<ActionResult> OnPost()
    {
        logger.LogMethodEntered();

        await PostTask(TaskName.PreFundingAgreementCheckpointMeeting);

        return Redirect(string.Format(RouteConstants.TaskList, ProjectId));
    }
}   

