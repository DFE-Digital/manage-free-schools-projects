using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Http;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard
{
    public interface IGetDashboardService
    {
        Task<List<GetDashboardResponse>> Execute(GetDashboardParameters parameters);
    }

    public record GetDashboardParameters
    {
        public string Project { get; set; }
        public string UserId { get; set; }
        public string Region { get; set; }
    }

    public class GetDashboardService : IGetDashboardService
    {
        private readonly MfspContext _context;

        public GetDashboardService(MfspContext context)
        {
            _context = context;
        }

        public async Task<List<GetDashboardResponse>> Execute(GetDashboardParameters parameters)
        {
            var query = _context.Kpi.AsQueryable();

            query = ApplyFilters(query, parameters);

            var projectRecords = await query.Take(10).ToListAsync();

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

        private IQueryable<Kpi> ApplyFilters(IQueryable<Kpi> query, GetDashboardParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.UserId))
            {
                query = query.Where(kpi => kpi.User.Email == parameters.UserId);
            }

            return query;
        }
    }
}
