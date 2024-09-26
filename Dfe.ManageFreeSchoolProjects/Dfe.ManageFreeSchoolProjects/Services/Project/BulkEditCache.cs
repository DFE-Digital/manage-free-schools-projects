using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface IBulkEditCache : ICookieCacheService<BulkEditRequest>
    {
    }

    public class BulkEditCache : CookieCacheService<BulkEditRequest>, IBulkEditCache
    {
        public BulkEditCache(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider DataProtectionProvider)
            : base(httpContextAccessor, DataProtectionProvider, "BE1")
        {
        }
    }
}