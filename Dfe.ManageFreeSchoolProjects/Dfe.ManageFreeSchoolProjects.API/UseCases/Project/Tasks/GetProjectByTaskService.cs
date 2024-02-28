using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ArticlesOfAssociation;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Constituency;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Dates;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DraftGovernancePlan;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FinancePlan;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.KickOffMeeting;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ModelFundingAgreement;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RegionAndLocalAuthority;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RiskAppraisalMeeting;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Trusts;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Section10Consultation;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks
{
    public interface IGetProjectByTaskService
    {
        public Task<GetProjectByTaskResponse> Execute(string projectId, TaskName taskName);
    }

    public class GetProjectByTaskService : IGetProjectByTaskService
    {
        private readonly MfspContext _context;

        public GetProjectByTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetProjectByTaskResponse> Execute(string projectId, TaskName taskName)
        {
            var query = _context.Kpi.Where(kpi => kpi.ProjectStatusProjectId == projectId);

            var parameters = new GetTaskServiceParameters()
            {
                ProjectId = projectId,
                BaseQuery = query
            };

            GetProjectByTaskResponse result = null;

            switch (taskName)
            {
                case TaskName.School:
                    result = await new GetSchoolTaskService().Get(parameters);
                    break;
                case TaskName.Dates:
                    result = await new GetDatesTaskService().Get(parameters);
                    break;
                case TaskName.RiskAppraisalMeeting:
                    result = await new GetRiskAppraisalMeetingTaskService(_context).Get(parameters);
                    break;
                case TaskName.Trust:
                    result = await new GetTrustTaskService().Get(parameters);
                    break;
                case TaskName.RegionAndLocalAuthority:
                    result = await new GetRegionAndLocalAuthorityTaskService().Get(parameters);
                    break;
                case TaskName.Constituency:
                    result = await new GetConstituencyTaskService().Get(parameters);
                    break;
                case TaskName.KickOffMeeting:
                    result = await new GetKickOffMeetingTaskService(_context).Get(parameters);
                    break;
                case TaskName.ModelFundingAgreement:
                    result = await new GetModelFundingAgreementTaskService(_context).Get(parameters);
                    break;
                case TaskName.Section10Consultation:
                    result = await new GetSection10ConsultationTaskService(_context).Get(parameters);
                    break;
                case TaskName.ArticlesOfAssociation:
                    result = await new GetArticlesOfAssociationTaskService(_context).Get(parameters);
                    break;
                case TaskName.FinancePlan:
                    result = await new GetFinancePlanTaskService(_context).Get(parameters);
                    break;
                case TaskName.DraftGovernancePlan:
                    result = await new GetDraftGovernancePlanTaskService(_context).Get(parameters);
                    break;
                default:
                    throw new ArgumentException($"Unknown task name {taskName}");
            }

            if (result != null)
            {
                var schoolName = await query.Select(kpi => kpi.ProjectStatusCurrentFreeSchoolName).FirstOrDefaultAsync();
                result.SchoolName = schoolName;
            }

            return result;
        }
    }
}