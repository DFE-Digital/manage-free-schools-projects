﻿using AutoFixture;
using Dfe.ManageFreeSchoolProjects.API.RequestModels.Concerns.TeamCasework;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
	[Collection(ApiTestCollection.ApiTestCollectionName)]
	public class TeamCaseworkIntegrationTests
	{
		private readonly Fixture _fixture;
		private readonly HttpClient _client;

		public TeamCaseworkIntegrationTests(ApiTestFixture apiTestFixture)
		{
			_client = apiTestFixture.Client;
			_fixture = new();
		}

		[Fact]
		public async Task When_Put_InvalidRequest_Returns_ValidationErrors()
		{
			var request = _fixture.Create<ManageFreeSchoolProjectsTeamUpdateRequest>();
			request.OwnerId = new string('a', 301);

			var result = await _client.PutAsync($"/v2/concerns-team-casework/owners/1", request.ConvertToJson());
			result.StatusCode.Should().Be(HttpStatusCode.BadRequest);


			var error = await result.Content.ReadAsStringAsync();
			error.Should().Contain("The field OwnerId must be a string with a maximum length of 300.");
		}

		[Fact]
		public async Task When_Put_InvalidQuery_Returns_ValidationErrors()
		{
			var request = _fixture.Create<ManageFreeSchoolProjectsTeamUpdateRequest>();
			var ownerId = new string('a', 301);

			var result = await _client.PutAsync($"/v2/concerns-team-casework/owners/{ownerId}", request.ConvertToJson());
			result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

			var error = await result.Content.ReadAsStringAsync();
			error.Should().Contain("The field ownerId must be a string with a maximum length of 300.");
		}

	}
}
