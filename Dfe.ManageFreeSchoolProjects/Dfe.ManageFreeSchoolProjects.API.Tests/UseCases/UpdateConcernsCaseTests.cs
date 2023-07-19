using Dfe.ManageFreeSchoolProjects.API.Factories;
using Dfe.ManageFreeSchoolProjects.API.RequestModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Dfe.ManageFreeSchoolProjects.Data.Gateways;
using Dfe.ManageFreeSchoolProjects.Data.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases
{
    public class UpdateConcernsCaseTests
    {
        [Fact]
        public void ShouldSaveAConcernsCase_WhenGivenAModelToUpdate()
        {
            var urn = 1234;
            var gateway = new Mock<IConcernsCaseGateway>();
            var concernsCase = Builder<ConcernsCase>.CreateNew().With(c => c.Urn = urn).Build();
            var updateRequest = Builder<ConcernCaseRequest>.CreateNew().Build();

            var concernsToUpdate = ConcernsCaseFactory.Update(concernsCase, updateRequest);

            gateway.Setup(g => g.GetConcernsCaseByUrn(urn, false)).Returns(concernsCase);
            gateway.Setup(g => g.Update(It.IsAny<ConcernsCase>())).Returns(concernsToUpdate);

            var expected = ConcernsCaseResponseFactory.Create(concernsToUpdate);

            var useCase = new UpdateConcernsCase(gateway.Object);
            var result = useCase.Execute(urn, updateRequest);

            result.Should().BeEquivalentTo(expected);
        }
    }
}