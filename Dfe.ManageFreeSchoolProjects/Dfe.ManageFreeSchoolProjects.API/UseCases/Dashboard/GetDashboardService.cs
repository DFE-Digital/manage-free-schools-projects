using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard
{
    public interface IGetDashboardService
    {
        Task<(List<GetDashboardResponse>, int,IQueryable<string>)> Execute(GetDashboardParameters parameters);
        Task<IEnumerable<string>> ExecuteProjectIds(GetDashboardParameters parameters);
    }

    public record GetDashboardParameters
    {
        public string Project { get; set; }
        public string UserId { get; set; }
        public List<string> Regions { get; set; }
        public List<string> LocalAuthority { get; set; }
        public List<string> ProjectManagedBy { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
        
    }

    public class GetDashboardService : IGetDashboardService
    {
        private readonly MfspContext _context;

        public GetDashboardService(MfspContext context)
        {
            _context = context;
        }

        
        public async Task<(List<GetDashboardResponse>, int, IQueryable<string>)> Execute(GetDashboardParameters parameters)
        {
            var query = _context.Kpi.AsQueryable();

            query = ApplyFilters(query, parameters);

            var count = query.Count();
            
            var projectRecordTotal = await 
                query
                    .OrderByDescending(kpi => kpi.ProjectStatusProvisionalOpeningDateAgreedWithTrust)
                    .ThenBy(kpi => kpi.ProjectStatusCurrentFreeSchoolName)
                    .ToListAsync();

            var totalListOfIds = query.Select(x => x.ProjectStatusCurrentFreeSchoolName).Distinct();

            var projectRecords = await 
                query
                    .OrderByDescending(kpi => kpi.ProjectStatusProvisionalOpeningDateAgreedWithTrust)
                    .ThenBy(kpi => kpi.ProjectStatusCurrentFreeSchoolName)
                    .Paginate(parameters.Page, parameters.Count)
                    .ToListAsync();

            var result = projectRecords.Select(record => new GetDashboardResponse
            {
                ProjectId = record.ProjectStatusProjectId,
                ProjectTitle = record.ProjectStatusCurrentFreeSchoolName,
                TrustName = record.TrustName,
                LocalAuthority = record.LocalAuthority,
                RealisticOpeningDate = record.ProjectStatusProvisionalOpeningDateAgreedWithTrust,
                Region = record.SchoolDetailsGeographicalRegion,
                ProjectManagedBy = record.KeyContactsFsgLeadContact, 
                Status = "1"
            }).ToList();

            return (result, count, totalListOfIds);
        }

        private static IQueryable<Kpi> ApplyFilters(IQueryable<Kpi> query, GetDashboardParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.UserId))
            {
                query = query.Where(kpi => kpi.User.Email == parameters.UserId);
            }

            if (parameters.Regions.Any())
            {
                query = query.Where(kpi => parameters.Regions.Any(region => kpi.SchoolDetailsGeographicalRegion == region));
            }

            if (!string.IsNullOrEmpty(parameters.Project))
            {
                query = query.Where(kpi => 
                kpi.ProjectStatusCurrentFreeSchoolName.Contains(parameters.Project)
                || kpi.ProjectStatusProjectId == parameters.Project);
            }

            if (parameters.LocalAuthority.Any())
            {
                query = query.Where(kpi => parameters.LocalAuthority.Any(localAuthority => kpi.LocalAuthority == localAuthority));
            }

            if (parameters.ProjectManagedBy.Count > 0)
            {
                query = query.Where(kpi => parameters.ProjectManagedBy.Any(projectManagedBy => kpi.KeyContactsFsgLeadContact == projectManagedBy));
            }

            query = query.Where(kpi =>
                kpi.ProjectStatusFreeSchoolApplicationWave == "FS - Presumption"
                    && kpi.Wave == "FS - Presumption"
            );

            return query;
        }
        
        
        public async Task<IEnumerable<string>> ExecuteProjectIds(GetDashboardParameters parameters)
        {
            var query = _context.Kpi.AsQueryable();

            query = ApplyFilters(query, parameters);

            var count = query.Count();
            
            var projectRecordTotal = await 
                query
                    .OrderByDescending(kpi => kpi.ProjectStatusProvisionalOpeningDateAgreedWithTrust)
                    .ThenBy(kpi => kpi.ProjectStatusCurrentFreeSchoolName)
                    .ToListAsync();

            var totalListOfIds = query.Select(x => x.ProjectStatusProjectId).Distinct();

            return totalListOfIds.ToList();
        }
    }
}
