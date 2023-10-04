namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Trust
{
    public class TrustSearchResponseDto
    {
        public virtual IList<TrustSearchDto> Trusts { get; set; }
        public virtual int NumberOfMatches { get; set; }
    }
}
