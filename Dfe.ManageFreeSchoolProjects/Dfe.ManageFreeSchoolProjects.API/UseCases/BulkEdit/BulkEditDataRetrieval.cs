using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public class BulkEditDataRetrieval(MfspContext context) : IBulkEditDataRetrieval<BulkEditDto>
    {
        public async Task<Dictionary<string, BulkEditDto>> Retrieve(List<string> projectIds)
        {
            return await context.Kpi.Where(x => projectIds.Contains(x.ProjectStatusProjectId))
                .Select(x => new BulkEditDto
                {
                    ProjectId = x.ProjectStatusProjectId,
                    SchoolName = x.ProjectStatusCurrentFreeSchoolName
                })
                .ToDictionaryAsync(k => k.ProjectId);
        }
    }
}
