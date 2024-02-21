using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Reports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Reports
{
    public class ProjectReportBuilderTests
    {
        [Fact]
        public void Build_ReturnsProjectReport()
        {
            // Arrange
            var parameters = BuildParameters();

            // Act
            var result = ProjectReportBuilder.Build(parameters);

            var taskHeaders = result.Headers.Select(h => h.TaskName).ToList();
            var sectionHeaders = result.Headers.Select(h => h.Section).ToList();
            var columnHeaders = result.Headers.Select(h => h.ColumnName).ToList();

            taskHeaders.Should().Contain("Dates");
            taskHeaders.Should().Contain("School");
            taskHeaders.Should().Contain("Trust");
            taskHeaders.Should().Contain("Region and local authority");
            taskHeaders.Should().Contain("Constituency");
            taskHeaders.Should().Contain("Risk appraisal meeting");
            taskHeaders.Should().Contain("Kick-off meeting");
            taskHeaders.Should().Contain("Articles of association");
            taskHeaders.Should().Contain("Finance plan");

            sectionHeaders.Should().Contain("Setting-up");
            sectionHeaders.Should().Contain("Pre-opening");

            result.Projects.Count.Should().Be(1);

            var project = result.Projects.First();

            AssertEntry(nameof(DatesTask.DateOfEntryIntoPreopening), "01/01/2021", project, columnHeaders);
            AssertEntry(nameof(DatesTask.ProvisionalOpeningDateAgreedWithTrust), "EMPTY", project, columnHeaders);
            AssertEntry(nameof(SchoolTask.Gender), "Boys only", project, columnHeaders);
            AssertEntry(nameof(SchoolTask.FaithType), "EMPTY", project, columnHeaders);
            AssertEntry(nameof(TrustTask.TrustName), "Test school", project, columnHeaders);
            AssertEntry(nameof(RegionAndLocalAuthorityTask.LocalAuthorityCode), "123", project, columnHeaders);
            AssertEntry(nameof(ConstituencyTask.Name), "Sheffield", project, columnHeaders);
            AssertEntry(nameof(RiskAppraisalMeetingTask.InitialRiskAppraisalMeetingCompleted), "No", project, columnHeaders);
            AssertEntry(nameof(KickOffMeetingTask.FundingArrangementAgreed), "Yes", project, columnHeaders);
            AssertEntry(nameof(ArticlesOfAssociationTask.ChairHaveSubmittedConfirmation), "No", project, columnHeaders);
            AssertEntry(nameof(FinancePlanTask.RpaCoverType), "Cover", project, columnHeaders);
        }

        private static ProjectReportBuilderParameters BuildParameters()
        {
            var result = new ProjectReportBuilderParameters
            {
                Projects = new List<GetProjectByTaskResponse>
                {
                    new GetProjectByTaskResponse
                    {
                        Dates = new DatesTask()
                        {
                            DateOfEntryIntoPreopening = new DateTime(2021, 1, 1),
                        },
                        School = new SchoolTask()
                        {
                            Gender = Gender.BoysOnly,
                            FaithType = FaithType.NotSet
                        },
                        Trust = new TrustTask()
                        {
                            TrustName = "Test school"
                        },
                        RegionAndLocalAuthority = new RegionAndLocalAuthorityTask()
                        {
                            LocalAuthorityCode = "123",
                        },
                        Constituency = new ConstituencyTask()
                        {
                            Name = "Sheffield",
                        },
                        RiskAppraisalMeeting = new RiskAppraisalMeetingTask()
                        {
                            InitialRiskAppraisalMeetingCompleted = false,
                        },
                        KickOffMeeting = new KickOffMeetingTask()
                        {
                            FundingArrangementAgreed = true,
                        },
                        ArticlesOfAssociation = new ArticlesOfAssociationTask()
                        {
                            ChairHaveSubmittedConfirmation = false,
                        },
                        FinancePlan = new FinancePlanTask()
                        {
                            RpaCoverType = "Cover",
                        }
                    }
                }
            };

            return result;
        }

        private static void AssertEntry(
            string propertyName, 
            string expectedValue,
            ProjectDataRow project,
            List<string> columnHeaders)
        {
            columnHeaders.Should().Contain(propertyName);
            var columnIndex = columnHeaders.IndexOf(propertyName);
            project.Values[columnIndex].Value.Should().Be(expectedValue);
        }
    }
}
