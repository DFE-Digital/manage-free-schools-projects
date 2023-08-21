using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard
{
    public interface IGetDashboardByUserService
    {
        Task<List<GetDashboardResponse>> Execute(string userId);
    }

    public class GetDashboardByUserService : IGetDashboardByUserService
    {
        private readonly MfspContext _context;

        public GetDashboardByUserService(MfspContext context)
        {
            _context = context;
        }

        public async Task<List<GetDashboardResponse>> Execute(string userId)
        {
            var matchingUser = await _context.Users
                .Include(u => u.Projects)
                .FirstOrDefaultAsync(u => u.Email == userId);

            if (matchingUser == null)
            {
                return new List<GetDashboardResponse>();
            }

            var result = matchingUser.Projects.Select(record =>
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
