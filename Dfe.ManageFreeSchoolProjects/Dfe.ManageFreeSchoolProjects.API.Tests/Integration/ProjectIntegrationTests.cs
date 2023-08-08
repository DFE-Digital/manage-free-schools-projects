//using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net;
//using System.Threading.Tasks;
//using Xunit;
//using AutoFixture;
//using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
//using FizzWare.NBuilder;
//using Dfe.ManageFreeSchoolProjects.Data.Models.Projects;
//using Dfe.ManageFreeSchoolProjects.Data;

//namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
//{
//    public class ProjectIntegrationTests
//    {
//        private readonly Fixture _autoFixture;
//        private readonly HttpClient _client;
//        private readonly RandomGenerator _randomGenerator;
//        private readonly ApiTestFixture _testFixture;

//        public ProjectIntegrationTests(ApiTestFixture fixture)
//        {
//            _autoFixture = new Fixture();
//            _randomGenerator = new RandomGenerator();
//            _testFixture = fixture;
//            _client = fixture.Client;
//        }

//        private List<Project> ProjectsToBeDisposedAtEndOfTests { get; } = new();

//        //[Fact]
//        //public async Task CanGetConcernCaseById()
//        //{
//        //    await using ProjectsDbContext context = _testFixture.GetContext();

//        //    SetupProjectTestData("FS2014");
//        //    Project concernsCase = context.Projects.First();

//        //    HttpRequestMessage httpRequestMessage = new() { Method = HttpMethod.Get, RequestUri = new Uri($"https://notarealdomain.com/v2/concerns-cases/urn/{concernsCase.ProjectId}") };

//        //    //ConcernsCaseResponse expectedConcernsCaseResponse = ConcernsCaseResponseFactory.Create(concernsCase);

//        //    //ApiSingleResponseV2<ConcernsCaseResponse> expected = new(expectedConcernsCaseResponse);

//        //    //HttpResponseMessage response = await _client.SendAsync(httpRequestMessage);

//        //    //response.StatusCode.Should().Be(HttpStatusCode.OK);
//        //    //ApiSingleResponseV2<ConcernsCaseResponse> result = await response.Content.ReadFromJsonAsync<ApiSingleResponseV2<ConcernsCaseResponse>>();

//        //    //result.Should().BeEquivalentTo(expected);
//        //    //result.Data.Urn.Should().Be(concernsCase.Urn);
//        //}
//    }
//}
