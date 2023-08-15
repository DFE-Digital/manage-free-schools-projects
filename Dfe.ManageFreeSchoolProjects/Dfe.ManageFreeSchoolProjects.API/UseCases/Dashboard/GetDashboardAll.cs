using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard
{
    public interface IGetDashboardAll
    {
        Task<List<GetDashboardResponse>> Execute();
    }

    public class GetDashboardAll : IGetDashboardAll
    {
        private MfspContext _context;

        public GetDashboardAll(MfspContext context)
        {
            _context = context;
        }

        public async Task<List<GetDashboardResponse>> Execute()
        {
            var projectRecords = await _context.Kpis.Take(10).ToListAsync();

            var result = projectRecords.Select(record =>
            {
                return new GetDashboardResponse()
                {
                    ProjectId = record.ProjectStatusProjectId,
                    ProjectTitle = record.ProjectStatusCurrentFreeSchoolName,
                    TrustName = record.TrustName,
                    LocalAuthority = record.LocalAuthority,
                    RealisticOpeningDate = record.RatProvisionalOpeningDateAgreedWithTrust != null ? record.RatProvisionalOpeningDateAgreedWithTrust.Value.ToLongDateString() : null,
                    Region = record.SchoolDetailsGeographicalRegion,
                    Status = "1"
                };
            }).ToList();

            return result;
        }
    }
}
