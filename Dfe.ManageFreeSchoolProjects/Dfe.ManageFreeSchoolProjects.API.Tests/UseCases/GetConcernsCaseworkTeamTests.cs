using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Dfe.ManageFreeSchoolProjects.Data.Gateways;
using Dfe.ManageFreeSchoolProjects.Data.Models.Concerns.TeamCasework;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases
{
    public class GetManageFreeSchoolProjectsTeamTests
    {
        [Fact]
        public async Task GetManageFreeSchoolProjectsTeam_Implements_IGetManageFreeSchoolProjectsTeam()
        {
            typeof(GetManageFreeSchoolProjectsTeam).Should().BeAssignableTo<GetManageFreeSchoolProjectsTeam>();
        }

        [Fact]
        public async Task Execute_When_Team_Found_Returns_ManageFreeSchoolProjectsTeam()
        {
            var ownerId = "john.doe";
            var mockGateway = new Mock<IConcernsTeamCaseworkGateway>();
            var useCase = new GetManageFreeSchoolProjectsTeam(mockGateway.Object);

            mockGateway
            .Setup(g => g.GetByOwnerId(ownerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ManageFreeSchoolProjectsTeam
            {
                Id = ownerId,
                TeamMembers = new List<ManageFreeSchoolProjectsTeamMember>
                {
                    new ManageFreeSchoolProjectsTeamMember { TeamMember = "user.one" } ,
                    new ManageFreeSchoolProjectsTeamMember { TeamMember = "user.two" } ,
                    new ManageFreeSchoolProjectsTeamMember { TeamMember = "user.three" }
                }
            });

            var sut = new GetManageFreeSchoolProjectsTeam(mockGateway.Object);
            var result = await sut.Execute(ownerId, CancellationToken.None);

            result.Should().NotBeNull();
            result.OwnerId.Should().Be(ownerId);
            result.TeamMembers.Length.Should().Be(3);
            result.TeamMembers.Should().Contain("user.one");
            result.TeamMembers.Should().Contain("user.two");
            result.TeamMembers.Should().Contain("user.three");
        }

        [Fact]
        public async Task Execute_When_Team_NotFound_Returns_Null()
        {
            var ownerId = "john.doe";
            var mockGateway = new Mock<IConcernsTeamCaseworkGateway>();
            var useCase = new GetManageFreeSchoolProjectsTeam(mockGateway.Object);

            mockGateway
                .Setup(g => g.GetByOwnerId(ownerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(ManageFreeSchoolProjectsTeam));

            var sut = new GetManageFreeSchoolProjectsTeam(mockGateway.Object);
            var result = await sut.Execute(ownerId, CancellationToken.None);

            result.Should().BeNull();
        }
    }
}
