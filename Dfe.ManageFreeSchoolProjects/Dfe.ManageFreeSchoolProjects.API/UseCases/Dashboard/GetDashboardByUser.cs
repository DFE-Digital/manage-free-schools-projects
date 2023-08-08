using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard
{
    public interface IGetDashboardByUser
    {
        Task<List<GetDashboardByUserResponse>> Execute(string userId);
    }

    public class GetDashboardByUser : IGetDashboardByUser
    {
        private MfspContext _context;

        public GetDashboardByUser(MfspContext context)
        {
            _context = context;
        }

        public async Task<List<GetDashboardByUserResponse>> Execute(string userId)
        {
            var projectRecords = await _context.Kpis.ToListAsync();

            var result = projectRecords.Select(record =>
            {
                return new GetDashboardByUserResponse()
                {
                    ProjectId = record.ProjectStatusProjectId,
                    ProjectTitle = record.ProjectStatusCurrentFreeSchoolName,
                    TrustName = record.TrustName,
                    LocalAuthority = record.LocalAuthority,
                    RealisticOpeningDate = record.RatProvisionalOpeningDateAgreedWithTrust?.ToLongDateString(),
                    Region = record.SchoolDetailsGeographicalRegion,
                    Status = "1"
                };
            }).ToList();

            return result;
        }
    }
}
