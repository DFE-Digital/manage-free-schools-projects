using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PDG;

public class UpdatePDGGrant(MfspContext _context) : IUpdateTaskService
{
    public async Task Update(UpdateTaskServiceParameters parameters)
    {
        var task = parameters.Request.PDGGrantTask;
        var dbKpi = parameters.Kpi;

        if (task is null)
            return;

        var db = await _context.Po.FirstOrDefaultAsync(r => r.Rid == dbKpi.Rid);

        if (db == null)
        {
            db = new Po { Rid = dbKpi.Rid };
            _context.Add(db);
        }

        db.ProjectDevelopmentGrantFundingInitialGrantAllocation = task.InitialGrant?.ToString("0.00");
        db.ProjectDevelopmentGrantFundingRevisedGrantAllocation = task.RevisedGrant?.ToString("0.00");
    }
}