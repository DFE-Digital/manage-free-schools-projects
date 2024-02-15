using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Risk;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DraftGovernancePlan
{
    public interface IGetDraftGovernancePlanTaskSummaryService
    {
        Task<TaskSummaryResponse> Execute(string projectId, TaskSummaryResponse taskDetails);
    }

    public class GetDraftGovernancePlanTaskSummaryService : IGetDraftGovernancePlanTaskSummaryService
    {
        private readonly IGetProjectRiskService _getProjectRiskService;

        public GetDraftGovernancePlanTaskSummaryService(IGetProjectRiskService getProjectRiskService)
        {
            _getProjectRiskService = getProjectRiskService;
        }

        public async Task<TaskSummaryResponse> Execute(string projectId, TaskSummaryResponse taskDetails)
        {
            var result = taskDetails;
            result.IsHidden = true;

            if (taskDetails.Status == ProjectTaskStatus.InProgress)
            {
                result.IsHidden = false;

                return result;
            }

            var risk = await _getProjectRiskService.Execute(projectId, 1);

            if (risk != null) 
            {
                if (IsRedOrRedAmber(risk.Overall) || IsRedOrRedAmber(risk.GovernanceAndSuitability))
                {
                    taskDetails.IsHidden = false;
                }
            }

            return result;
        }

        private bool IsRedOrRedAmber(ProjectRiskEntryResponse riskEntry)
        {
            return riskEntry.RiskRating == ProjectRiskRating.Red 
                || riskEntry.RiskRating == ProjectRiskRating.AmberRed;
        }
    }
}
