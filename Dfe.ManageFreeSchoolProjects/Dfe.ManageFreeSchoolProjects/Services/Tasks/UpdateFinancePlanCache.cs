using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.Tasks
{
    public interface IUpdateFinancePlanCache : ICookieCacheService<UpdateFinancePlanCacheItem>;
    public class UpdateFinancePlanCache : CookieCacheService<UpdateFinancePlanCacheItem>, IUpdateFinancePlanCache
    {
        public UpdateFinancePlanCache(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtectionProvider) :
            base(httpContextAccessor, dataProtectionProvider, "UFP1")
        {
        }
    }

    public record UpdateFinancePlanCacheItem
    {
        public FinancePlanTask FinancePlan { get; set; }
    }
}
