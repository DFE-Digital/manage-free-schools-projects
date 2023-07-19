using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Dfe.ManageFreeSchoolProjects.Data.Gateways;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases
{
    public class GetManageFreeSchoolProjectsTeamOwnersTests
    {
        [Fact]
        public async Task Execute_Calls_Gateway()
        {
            var expectedData = new[] { "user.1", "user.2", "user.3" };
            var mockGateway = new Mock<IConcernsTeamCaseworkGateway>();
            mockGateway.Setup(x => x.GetTeamOwners(It.IsAny<CancellationToken>())).ReturnsAsync(expectedData);
            var sut = new GetManageFreeSchoolProjectsTeamOwners(mockGateway.Object);

            var result = await sut.Execute(CancellationToken.None);
            result.Should().BeEquivalentTo(expectedData);
        }
    }
}
