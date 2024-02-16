using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DraftGovernancePlan;
using NSubstitute;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project.DraftGovernancePlan
{
    public class GetDraftGovernancePlanTaskSummaryServiceTests
    {
        [Fact]
        public async Task Execute_When_TaskStarted_IsHidden_False()
        {
            var taskDetails = new TaskSummaryResponse
            {
                Name = "Draft Governance Plan",
                Status = ProjectTaskStatus.InProgress
            };

            var service = new GetDraftGovernancePlanTaskSummaryService(CreateProjectRiskService(new GetProjectRiskResponse()));

            // Act
            var result = await service.Execute("1", taskDetails);

            result.Status.Should().Be(ProjectTaskStatus.InProgress);
            result.Name.Should().Be("Draft Governance Plan");
            result.IsHidden.Should().BeFalse();
        }

        [Theory]
        [InlineData(ProjectRiskRating.Red)]
        [InlineData(ProjectRiskRating.AmberRed)]
        public async Task Execute_When_OverallRiskIsSet_IsHidden_False(ProjectRiskRating riskRating)
        {
            var taskDetails = new TaskSummaryResponse
            {
                Name = "Draft Governance Plan",
                Status = ProjectTaskStatus.NotStarted
            };

            var createProjectRiskService = CreateProjectRiskService(new GetProjectRiskResponse
            {
                Overall = new()
                {
                    RiskRating = riskRating,
                }
            });

            var service = new GetDraftGovernancePlanTaskSummaryService(createProjectRiskService);

            var result = await service.Execute("1", taskDetails);

            result.IsHidden.Should().BeFalse();
        }

        [Theory]
        [InlineData(ProjectRiskRating.Red)]
        [InlineData(ProjectRiskRating.AmberRed)]
        public async Task Execute_When_GovernanceRiskIsSet_IsHidden_False(ProjectRiskRating riskRating)
        {
            var taskDetails = new TaskSummaryResponse
            {
                Name = "Draft Governance Plan",
                Status = ProjectTaskStatus.NotStarted
            };

            var createProjectRiskService = CreateProjectRiskService(new GetProjectRiskResponse
            {
                GovernanceAndSuitability = new()
                {
                    RiskRating = riskRating,
                }
            });

            var service = new GetDraftGovernancePlanTaskSummaryService(createProjectRiskService);

            var result = await service.Execute("1", taskDetails);

            result.IsHidden.Should().BeFalse();
        }

        [Fact]
        public async Task Execute_When_NoRisk_TaskNotStarted_IsHidden_True()
        {
            var taskDetails = new TaskSummaryResponse
            {
                Name = "Draft Governance Plan",
                Status = ProjectTaskStatus.NotStarted
            };

            var createProjectRiskService = CreateProjectRiskService(new GetProjectRiskResponse());

            var service = new GetDraftGovernancePlanTaskSummaryService(createProjectRiskService);

            var result = await service.Execute("1", taskDetails);

            result.IsHidden.Should().BeTrue();
        }

        [Fact]
        public async Task Execute_When_RiskNotFound_IsHidden_True()
        {
            var taskDetails = new TaskSummaryResponse
            {
                Name = "Draft Governance Plan",
                Status = ProjectTaskStatus.NotStarted
            };

            var createProjectRiskService = CreateProjectRiskService(null);

            var service = new GetDraftGovernancePlanTaskSummaryService(createProjectRiskService);

            var result = await service.Execute("1", taskDetails);

            result.IsHidden.Should().BeTrue();
        }

        private static IGetProjectRiskService CreateProjectRiskService(GetProjectRiskResponse response)
        {
            var riskService = Substitute.For<IGetProjectRiskService>();
            riskService.Execute(Arg.Any<string>(), Arg.Any<int>()).Returns(response);

            return riskService;
        }
    }
}
