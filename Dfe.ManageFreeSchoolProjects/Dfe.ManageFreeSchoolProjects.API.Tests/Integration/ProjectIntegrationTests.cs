using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using FizzWare.NBuilder;
using Dfe.ManageFreeSchoolProjects.Data.Models.Projects;
using Dfe.ManageFreeSchoolProjects.Data;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    public class ProjectIntegrationTests
    {
        private readonly Fixture _autoFixture;
        private readonly HttpClient _client;
        private readonly RandomGenerator _randomGenerator;
        private readonly ApiTestFixture _testFixture;

        public ProjectIntegrationTests(ApiTestFixture fixture)
        {
            _autoFixture = new Fixture();
            _randomGenerator = new RandomGenerator();
            _testFixture = fixture;
            _client = fixture.Client;
        }

        private List<Project> ProjectsToBeDisposedAtEndOfTests { get; } = new();

        public void Dispose()
        {
            using ProjectsDbContext context = _testFixture.GetContext();

            if (ProjectsToBeDisposedAtEndOfTests.Any())
            {
                context.Projects.RemoveRange(ProjectsToBeDisposedAtEndOfTests);
                context.SaveChanges();
                ProjectsToBeDisposedAtEndOfTests.Clear();
            }
        }

        //[Fact]
        //public async Task CanGetConcernCaseById()
        //{
        //    await using ProjectsDbContext context = _testFixture.GetContext();

        //    SetupProjectTestData("FS2014");
        //    Project concernsCase = context.Projects.First();

        //    HttpRequestMessage httpRequestMessage = new() { Method = HttpMethod.Get, RequestUri = new Uri($"https://notarealdomain.com/v2/concerns-cases/urn/{concernsCase.ProjectId}") };

        //    //ConcernsCaseResponse expectedConcernsCaseResponse = ConcernsCaseResponseFactory.Create(concernsCase);

        //    //ApiSingleResponseV2<ConcernsCaseResponse> expected = new(expectedConcernsCaseResponse);

        //    //HttpResponseMessage response = await _client.SendAsync(httpRequestMessage);

        //    //response.StatusCode.Should().Be(HttpStatusCode.OK);
        //    //ApiSingleResponseV2<ConcernsCaseResponse> result = await response.Content.ReadFromJsonAsync<ApiSingleResponseV2<ConcernsCaseResponse>>();

        //    //result.Should().BeEquivalentTo(expected);
        //    //result.Data.Urn.Should().Be(concernsCase.Urn);
        //}


        private List<Project> SetupProjectTestData(string projectId, int count = 1)
        {
            List<Project> listOfProjects = new();
            for (int i = 0; i < count; i++)
            {
                Project project = Builder<Project>
                .CreateNew()
                .With(pr =>
                {
                    return pr.ProjectId = projectId;
                })
                .Build();

                AddProjectToDatabase(project);

                listOfProjects.Add(project);
            }

            return listOfProjects;
        }

        private void AddProjectToDatabase(Project project)
        {
            using ProjectsDbContext context = _testFixture.GetContext();

            try
            {
                context.Projects.Add(project);
                context.SaveChanges();
                ProjectsToBeDisposedAtEndOfTests.Add(project);
            }
            catch (Exception)
            {
                context.Projects.Remove(project);
                context.SaveChanges();
                throw;
            }
        }

    }
}
