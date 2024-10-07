using Dfe.ManageFreeSchoolProjects.Data;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.LocalAuthority
{
    public record LocalAuthorityCacheItem
    {
        public string LACode { get; init; }
        public string Name { get; init; }
        public string GeographicRegion { get; init; }
    }
    
    public interface ILocalAuthorityCache
    {
        List<LocalAuthorityCacheItem> GetLocalAuthorities();
    }

    public class LocalAuthorityCache(MfspContext context) : ILocalAuthorityCache
    {
        private List<LocalAuthorityCacheItem> _localAuthorities;

        public List<LocalAuthorityCacheItem> GetLocalAuthorities()
        {       
            if (_localAuthorities == null)
                {
                    _localAuthorities = context.LaData.Select(x => new LocalAuthorityCacheItem
                    {
                        LACode = x.LocalAuthoritiesLaCode,
                        Name = x.LocalAuthoritiesLaName,
                        GeographicRegion = x.LocalAuthoritiesGeographicalRegion
                    }).ToList();
                }

                return _localAuthorities;
         
        }
    }
}
