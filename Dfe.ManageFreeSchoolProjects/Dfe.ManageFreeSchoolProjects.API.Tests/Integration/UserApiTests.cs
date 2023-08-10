using Dfe.ManageFreeSchoolProjects.API.Contracts.Users;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using FluentAssertions;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class UserApiTests
    {
        private readonly HttpClient _client;
        private readonly Fixture _autoFixture;
        private readonly ApiTestFixture _testFixture;

        public UserApiTests(ApiTestFixture apiTestFixture)
        {
            _client = apiTestFixture.Client;
            _autoFixture = new();
            _testFixture = apiTestFixture;
        }

        [Fact]
        public async Task When_Post_UserDoesNotExist_CreatesUser_Returns_201Response()
        {
            var request = _autoFixture.Create<CreateUserRequest>();
            request.Email = request.Email.ToUpper();

            var result = await _client.PostAsync($"/api/v1/client/users", request.ConvertToJson());

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            using var context = _testFixture.GetContext();

            var lowerCaseEmail = request.Email.ToLower();

            var isEqual = request.Email == lowerCaseEmail;

            var createdUser = context.Users.First(u => u.Email == request.Email);

            createdUser.Email.Should().Be(request.Email.ToLower());
        }

        [Fact]
        public async Task When_Post_UserExists_Returns_ExistingUserLocation_200Response()
        {
            var request = _autoFixture.Create<CreateUserRequest>();

            var firstPost = await _client.PostAsync($"/api/v1/client/users", request.ConvertToJson());

            var secondPost = await _client.PostAsync($"/api/v1/client/users", request.ConvertToJson());

            using var context = _testFixture.GetContext();

            var entries = context.Users.Count(u => u.Email == request.Email);

            entries.Should().Be(1);
        }
    }
}
