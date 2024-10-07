using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                    SchoolName = x.ProjectStatusCurrentFreeSchoolName,
                    OpeningDate = x.ProjectStatusActualOpeningDate.Value.ToString("dd/MM/yyyy"),
                    LocalAuthorityCode = x.SchoolDetailsLocalAuthority,
                    ApplicationWave = x.ProjectStatusFreeSchoolApplicationWave,
                })
                .ToDictionaryAsync(k => k.ProjectId);
        }
    }
}
