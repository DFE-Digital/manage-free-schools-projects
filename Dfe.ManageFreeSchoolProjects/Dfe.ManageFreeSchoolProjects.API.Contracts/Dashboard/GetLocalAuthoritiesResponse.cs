namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard
{
    public record GetLocalAuthoritiesResponse
    {
        public List<LocalAuthorityResponse> LocalAuthorities { get; set; }
    }

    public record LocalAuthorityResponse
    {
        public string Name { get; set; }
        public string LACode { get; set; }
    }
}
