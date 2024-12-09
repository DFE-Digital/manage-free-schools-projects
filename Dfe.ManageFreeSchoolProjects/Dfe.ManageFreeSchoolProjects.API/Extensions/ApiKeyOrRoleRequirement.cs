using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace Dfe.ManageFreeSchoolProjects.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public class ApiKeyOrRoleRequirement(string rolePolicy) : IAuthorizationRequirement
    {
        public string RolePolicy { get; } = rolePolicy;
    }
}
