using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using System.Net.Http;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    public class ApiTestsBase(ApiTestFixture apiTestFixture)
    {
        protected readonly HttpClient _client = apiTestFixture.Client;
        protected readonly Fixture _autoFixture = new();
        protected readonly ApiTestFixture _testFixture = apiTestFixture;
    }
}
