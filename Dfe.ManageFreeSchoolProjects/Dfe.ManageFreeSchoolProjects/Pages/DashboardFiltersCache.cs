using Dfe.ManageFreeSchoolProjects.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace Dfe.BuildFreeSchools.Pages;


public interface IDashboardFiltersCache : ICookieCacheService<DashboardFiltersCacheItem>;


public class DashboardFiltersCache(
    IHttpContextAccessor httpContextAccessor,
    IDataProtectionProvider dataProtectionProvider) 
    : CookieCacheService<DashboardFiltersCacheItem>(httpContextAccessor, dataProtectionProvider, "DFC1"), IDashboardFiltersCache;

public class DashboardFiltersCacheItem
{
    public bool NavigatedAwayFromDashboard { get; set; }
}