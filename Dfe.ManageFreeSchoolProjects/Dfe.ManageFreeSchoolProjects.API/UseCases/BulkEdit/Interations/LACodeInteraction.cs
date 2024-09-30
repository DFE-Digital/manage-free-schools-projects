using Dfe.ManageFreeSchoolProjects.API.UseCases.LocalAuthority;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Interations
{
    internal class LACodeInteraction(ILocalAuthorityCache localAuthorityCache) : IHeaderDataInteration<BulkEditDto>
    {
        public BulkEditDto ApplyToDto(string value, BulkEditDto dto)
        {
            var localAuthority = localAuthorityCache.GetLocalAuthorities().FirstOrDefault(x => x.LACode == value);

            dto.LocalAuthorityCode = localAuthority.LACode;
            dto.LocalAuthorityName = localAuthority.Name;
            dto.Region = localAuthority.GeographicRegion;

            return dto;
        }

        public string GetFromDto(BulkEditDto dto)
        {
            return dto.LocalAuthorityCode;
        }
    }
}