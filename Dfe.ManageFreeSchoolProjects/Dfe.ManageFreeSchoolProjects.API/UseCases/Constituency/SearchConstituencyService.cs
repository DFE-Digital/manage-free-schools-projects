using Dfe.ManageFreeSchoolProjects.API.Contracts.Constituency;
using Newtonsoft.Json.Linq;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Constituency
{
    public interface ISearchConstituencyService
    {
        Task<GetSearchConstituencyResponse> Execute(string searchTerm);
    }
    public class SearchConstituencyService : ISearchConstituencyService
    {
        private readonly IHttpClientFactory _clientFactory;

        public SearchConstituencyService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<GetSearchConstituencyResponse> Execute(string searchTerm)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync(
                $"https://members-api.parliament.uk/api/Location/Constituency/Search?searchText={searchTerm}&skip=0&take=10");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JObject.Parse(content);

            var constituencies = new List<SearchConstituencyResponse>();
            foreach (var item in result.items)
            {
                var name = item.value.name;
                var id = item.value.id;
                var mpName = item.value.currentRepresentation.member.value.nameFullTitle;
                var party = item.value.currentRepresentation.member.value.latestParty.name;

                constituencies.Add(new() { Name = name, Id = id, MPName = mpName, Party = party });
            }

            return new GetSearchConstituencyResponse
            {
                Constituencies = constituencies
            };
        }
    }


}
