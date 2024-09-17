using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PreFundingAgreementCheckpointMeeting
{
    public class UpdatePreFundingAgreementCheckpointMeetingTaskService : IUpdateTaskService
    {
        private readonly MfspContext _context;

        public UpdatePreFundingAgreementCheckpointMeetingTaskService(MfspContext context)
        {
            _context = context;
        }

        public async Task Update(UpdateTaskServiceParameters parameters)
        {
            var task = parameters.Request.PreFundingAgreementCheckpointMeeting;
            var dbKpi = parameters.Kpi;

            if (task is null)
            {
                return;
            }

            var db = await _context.Milestones.FirstOrDefaultAsync(r => r.Rid == dbKpi.Rid);

            if (db == null)
            {
                db = new Data.Entities.Existing.Milestones();
                db.Rid = dbKpi.Rid;
                _context.Add(db);
            }
        }
    }
}