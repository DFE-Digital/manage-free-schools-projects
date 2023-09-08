using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard
{
    public interface IGetLocalAuthoritiesService
    {
        public Task<GetLocalAuthoritiesResponse> Execute(string region);
    }

    public class GetLocalAuthoritiesService : IGetLocalAuthoritiesService
    {
        private readonly MfspContext _context;

        public GetLocalAuthoritiesService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetLocalAuthoritiesResponse> Execute(string region)
        {
            var records = await _context.LaData.Where(e => e.LocalAuthoritiesGeographicalRegion == region).ToListAsync();

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
