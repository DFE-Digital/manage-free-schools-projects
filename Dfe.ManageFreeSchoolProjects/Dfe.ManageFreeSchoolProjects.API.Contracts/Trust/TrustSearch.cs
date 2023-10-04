namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Trust
{
    public sealed class TrustSearch : PageSearch
    {
        public string GroupName { get; set; }
        public string Ukprn { get; set; }
        public string CompaniesHouseNumber { get; set; }

        public TrustSearch()
        {

        }

        public TrustSearch(string groupName, string ukprn, string companiesHouseNumber) =>
            (GroupName, Ukprn, CompaniesHouseNumber) = (groupName, ukprn, companiesHouseNumber);
    }

    public class PageSearch
    {
        private int _page = 1;
        public int Page { get { return _page; } }

        public int PageIncrement()
        {
            return Interlocked.Increment(ref _page);
        }
    }
}