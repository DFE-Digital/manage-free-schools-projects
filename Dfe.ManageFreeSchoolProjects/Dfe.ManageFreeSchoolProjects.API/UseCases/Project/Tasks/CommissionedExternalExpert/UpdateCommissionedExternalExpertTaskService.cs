
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.CommissionedExternalExpert
{
    public class UpdateCommissionedExternalExpertTaskService : IUpdateTaskService
    {
        
        private readonly MfspContext _context;

        public UpdateCommissionedExternalExpertTaskService(MfspContext context)
        {
            _context = context;
        }
        
        public async Task Update(UpdateTaskServiceParameters parameters)
        {
            var task = parameters.Request.CommissionedExternalExpert;
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

            db.FsgPreOpeningMilestonesCommissionedExternalExpertVisit =
                task.CommissionedExternalExpertVisit;
            db.FsgPreOpeningMilestonesExternalExpertVisitDate =
                task.ExternalExpertVisitDate;
            db.FsgPreOpeningMilestoneSavedExternalExpertSpecsToWorkplacesFolder = task.SavedExternalExpertSpecsToWorkplacesFolder;
            
        }
    }
}
