using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard
{
    public interface IGetLocalAuthoritiesService
    {
        public Task<GetLocalAuthoritiesResponse> Execute(List<string> regions);
    }

    public class GetLocalAuthoritiesService : IGetLocalAuthoritiesService
    {
        private readonly MfspContext _context;

        public GetLocalAuthoritiesService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetLocalAuthoritiesResponse> Execute(List<string> regions)
        {
            GetLocalAuthoritiesResponse localAuthorities = new();

            foreach (string region in regions)
            {
                var recordsForRegion = await _context.LaData.Where(e => regions.Contains(e.LocalAuthoritiesGeographicalRegion))
                    .ToListAsync();

                var localAuthoritiesForRegion = recordsForRegion.Select(r => new LocalAuthorityResponse
                {
                    Name = r.LocalAuthoritiesLaName,
                    LACode = r.LocalAuthoritiesLaCode
                }).ToList();

                RegionResponse regionResponse = new RegionResponse { RegionName = region, LocalAuthorities = localAuthoritiesForRegion };

                localAuthorities.Regions.Add(regionResponse);
            }

            return localAuthorities;
        }
    }
}