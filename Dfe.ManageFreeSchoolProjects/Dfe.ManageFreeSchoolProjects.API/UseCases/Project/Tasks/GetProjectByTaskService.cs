using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Constituency;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Dates;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RegionAndLocalAuthority;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RiskAppraisalMeeting;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Trusts;
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

            // Could use a factory to do this?
            switch (taskName)
            {
                case TaskName.School:
                    return await new GetSchoolTaskService().Get(parameters);
                case TaskName.Dates:
                    return await new GetDatesTaskService().Get(parameters);
                case TaskName.RiskAppraisalMeeting:
                    return await new GetRiskAppraisalMeetingTaskService(_context).Get(parameters);
                case TaskName.Trust:
                    return await new GetTrustTaskService().Get(parameters);
                case TaskName.RegionAndLocalAuthority:
                    return await new GetRegionAndLocalAuthorityTaskService().Get(parameters);
                case TaskName.Constituency:
                    return await new GetConstituencyTaskService().Get(parameters);
                default:
                    throw new ArgumentException($"Unknown task name {taskName}");
            }

            //var result = await
            //    (from kpi in _context.Kpi
            //        where kpi.ProjectStatusProjectId == projectId
            //        join riskAppraisalMeetingTask in _context.RiskAppraisalMeetingTask on kpi.Rid equals
            //            riskAppraisalMeetingTask.RID into riskAppraisalMeetingTaskJoin
            //        from riskAppraisalMeetingTask in riskAppraisalMeetingTaskJoin.DefaultIfEmpty()
            //        select new GetProjectByTaskResponse
            //        {
            //            School = SchoolTaskMapper.Map(kpi),
            //            Dates = DatesTaskMapper.Map(kpi),
            //            Trust = TrustTaskMapper.Map(kpi),
            //            RegionAndLocalAuthority = RegionAndLocalAuthorityTaskMapper.Map(kpi),
            //            RiskAppraisalMeeting = RiskAppraisalTaskMapper.Map(riskAppraisalMeetingTask),
            //            Constituency = ConstituencyTaskMapper.Map(kpi)
            //        }).FirstOrDefaultAsync();

            //return result;
        }
    }
}