using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

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

                if(!string.IsNullOrEmpty(dto.SchoolName))
                {
                    entity.ProjectStatusCurrentFreeSchoolName = dto.SchoolName;
                }

                if(!string.IsNullOrEmpty(dto.OpeningDate))
                {
                    entity.ProjectStatusActualOpeningDate = DateTime.Parse(dto.OpeningDate, new CultureInfo("en-GB"));
                }

                if (!string.IsNullOrEmpty(dto.ProjectStatus))
                {
                    entity.ProjectStatusProjectStatus = ProjectMapper.FromProjectStatusType(ProjectMapper.ToProjectStatusType(dto.ProjectStatus));
                }

                if (!string.IsNullOrEmpty(dto.LocalAuthorityCode))
                {
                    entity.SchoolDetailsLocalAuthority = dto.LocalAuthorityCode;
                    entity.LocalAuthority = dto.LocalAuthorityName;
                    entity.SchoolDetailsGeographicalRegion = dto.Region;
                }

            }

            await _context.SaveChangesAsync();
        }
    }
}
