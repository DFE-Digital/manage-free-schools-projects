using AutoFixture;
using Dfe.ManageFreeSchoolProjects.API.Factories.CaseActionFactories;
using Dfe.ManageFreeSchoolProjects.Data.Models;
using FluentAssertions;
using Xunit;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Factories;

public class TrustFinancialForecastFactoryTests
{
	private readonly IFixture _fixture = new Fixture();

	[Fact]
	public void ToResponseModel_MapsResponseModelCorrectly()
	{
		// arrange
		var trustFinancialForecast = _fixture.Create<TrustFinancialForecast>();

		// act
		var result = trustFinancialForecast.ToResponseModel();

		// assert
		result.Should().BeEquivalentTo(trustFinancialForecast, options => options.Excluding(x => x.Id));
		result.TrustFinancialForecastId.Should().Be(trustFinancialForecast.Id);
	}
}