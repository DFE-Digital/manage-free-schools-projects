using Dfe.ManageFreeSchoolProjects.API.Contracts.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class LocalAuthorityApiTests : ApiTestsBase
    {
        public LocalAuthorityApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task When_Get_FilterByRegion_Returns_LocalAuthoritiesForRegion_200()
        {
            var localAuthorities = await CreateLocalAuthorityData();
            var firstLa = localAuthorities[0];
            var secondLa = localAuthorities[1];
            var thirdLa = localAuthorities[2];
            var firstRegion = firstLa.LocalAuthoritiesGeographicalRegion;
            var secondRegion = thirdLa.LocalAuthoritiesGeographicalRegion;

            var firstLaResponse = await _client.GetAsync($"/api/v1/client/local-authorities?region={firstRegion}");
            firstLaResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var firstResponseContent = await firstLaResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetLocalAuthoritiesResponse>>();
            var firstRegionLocalAuthorities = firstResponseContent.Data.LocalAuthorities;

            var expectedFirstRegionLocalAuthorities = new List<LocalAuthorityResponse>()
            {
                new LocalAuthorityResponse{ Name = firstLa.LocalAuthoritiesLaName },
                new LocalAuthorityResponse{ Name = secondLa.LocalAuthoritiesLaName }
            };

            firstRegionLocalAuthorities.Should().BeEquivalentTo(expectedFirstRegionLocalAuthorities);

            var secondLaResponse = await _client.GetAsync($"/api/v1/client/local-authorities?region={secondRegion}");
            secondLaResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var secondResponseContent = await secondLaResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetLocalAuthoritiesResponse>>();
            var secondRegionLocalAuthorities = secondResponseContent.Data.LocalAuthorities;

            var expectedSecondRegionLocalAuthorities = new List<LocalAuthorityResponse>()
            {
                new LocalAuthorityResponse{ Name = thirdLa.LocalAuthoritiesLaName }
            };

            secondRegionLocalAuthorities.Should().BeEquivalentTo(expectedSecondRegionLocalAuthorities);
        }

        [Fact]
        public async Task When_Get_FilterByRegion_NoMatch_Returns_Empty_200()
        {
            var response = await _client.GetAsync($"/api/v1/client/local-authorities?region=NotExist");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetLocalAuthoritiesResponse>>();
            content.Data.LocalAuthorities.Should().HaveCount(0);
        }

        private async Task<List<LaData>> CreateLocalAuthorityData()
        {
            using var context = _testFixture.GetContext();
            var firstRegion = _autoFixture.Create<string>();
            var secondRegion = _autoFixture.Create<string>();

            var firstLa = new LaData()
            {
                LocalAuthoritiesGeographicalRegion = firstRegion,
                LocalAuthoritiesLaName = _autoFixture.Create<string>(),
                LocalAuthoritiesLaCode = _autoFixture.Create<string>(),
            };

            var secondLa = new LaData()
            {
                LocalAuthoritiesGeographicalRegion = firstRegion,
                LocalAuthoritiesLaName = _autoFixture.Create<string>(),
                LocalAuthoritiesLaCode = _autoFixture.Create<string>()
            };

            var thirdLa = new LaData()
            {
                LocalAuthoritiesGeographicalRegion = secondRegion,
                LocalAuthoritiesLaName = _autoFixture.Create<string>(),
                LocalAuthoritiesLaCode = _autoFixture.Create<string>()
            };

            context.LaData.AddRange(firstLa, secondLa, thirdLa);
            await context.SaveChangesAsync();

            var result = new List<LaData>()
            {
                firstLa,
                secondLa,
                thirdLa
            };

            return result;
        }
    }
}
