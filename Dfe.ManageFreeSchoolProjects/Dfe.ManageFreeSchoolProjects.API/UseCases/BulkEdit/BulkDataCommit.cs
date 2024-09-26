using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public class BulkDataCommit : IBulkDataCommit<BulkEditDto>
    {
        private readonly MfspContext _context;

        public BulkDataCommit(MfspContext context)
        {
            _context = context;
        }

        public async Task Save(IEnumerable<BulkEditDto> data)
        {
            foreach (var dto in data)
            {
                var entity = await _context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == dto.ProjectId);

                entity.ProjectStatusCurrentFreeSchoolName = dto.SchoolName;
            }

            await _context.SaveChangesAsync();


        }
    }
}
