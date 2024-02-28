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
            var sourceData = BuildSourceData();

            // Act
            var result = ProjectReportBuilder.Build(sourceData);

            var taskHeaders = result.Headers.Select(h => h.TaskName).ToList();
            var sectionHeaders = result.Headers.Select(h => h.Section).ToList();
            var columnHeaders = result.Headers.Select(h => h.ColumnName).ToList();

			taskHeaders.Should().Contain("Reference data");
			taskHeaders.Should().Contain("Dates");
            taskHeaders.Should().Contain("School");
            taskHeaders.Should().Contain("Trust");
            taskHeaders.Should().Contain("Region and local authority");
            taskHeaders.Should().Contain("Constituency");
            taskHeaders.Should().Contain("Risk appraisal meeting");
            taskHeaders.Should().Contain("Kick-off meeting");
            taskHeaders.Should().Contain("Model funding agreement");
            taskHeaders.Should().Contain("Articles of association");
            taskHeaders.Should().Contain("Draft governance plan");
            taskHeaders.Should().Contain("Finance plan");
            taskHeaders.Should().Contain("Gias");

            sectionHeaders.Should().Contain("About the project");
            sectionHeaders.Should().Contain("Setting-up");
            sectionHeaders.Should().Contain("Pre-opening");
            sectionHeaders.Should().Contain("Sign-off preparation");

            result.Projects.Count.Should().Be(1);

            var project = result.Projects.First();

            AssertEntry(nameof(ProjectReferenceData.ProjectId), "123", project, columnHeaders);
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
            AssertEntry(nameof(DraftGovernancePlanTask.PlanFedBackToTrust), "No", project, columnHeaders);
			AssertEntry(nameof(ModelFundingAgreementTask.SharedFAWithTheTrust), "Yes", project, columnHeaders);
            AssertEntry(nameof(GiasTask.CheckedTrustInformation), "Yes", project, columnHeaders);
		}

        private static List<ProjectReportSourceData> BuildSourceData()
        {
            var result = new List<ProjectReportSourceData>()
            {
                new ProjectReportSourceData()
                {
                    ProjectReferenceData = new ProjectReferenceData()
                    {
                        ProjectId = "123",
                    },
                    TaskInformation = new GetProjectByTaskResponse()
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
                        },
                        DraftGovernancePlan = new DraftGovernancePlanTask()
                        {
							PlanFedBackToTrust = false,
						},
                        ModelFundingAgreement = new ModelFundingAgreementTask()
                        {
							SharedFAWithTheTrust = true
						},
                        Gias = new GiasTask()
                        {
                            CheckedTrustInformation = true
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
