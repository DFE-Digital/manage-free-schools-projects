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
            var records = await _context.LaData.Where(e => regions.Contains(e.LocalAuthoritiesGeographicalRegion)).ToListAsync();

            var localAuthorities = records.Select(r =>
            {
                return new LocalAuthorityResponse()
                {
                    Name = r.LocalAuthoritiesLaName
                };
            }).ToList();

            var result = new GetLocalAuthoritiesResponse()
            {
                LocalAuthorities = localAuthorities
            };

            return result;
        }
    }
}
