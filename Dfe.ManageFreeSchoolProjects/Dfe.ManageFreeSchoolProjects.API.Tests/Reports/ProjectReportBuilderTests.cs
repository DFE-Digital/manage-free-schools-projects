using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG;
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
            taskHeaders.Should().Contain("Reference numbers");
            taskHeaders.Should().Contain("Trust");
            taskHeaders.Should().Contain("Region and local authority");
            taskHeaders.Should().Contain("Constituency");
            taskHeaders.Should().Contain("Risk appraisal meeting");
            taskHeaders.Should().Contain("Project development grant (PDG)");
            taskHeaders.Should().Contain("Kick-off meeting");
            taskHeaders.Should().Contain("Funding agreement");
            taskHeaders.Should().Contain("Funding agreement health check");
            taskHeaders.Should().Contain("Funding agreement submission");
            taskHeaders.Should().Contain("Articles of association");
            taskHeaders.Should().Contain("Governance plan");
            taskHeaders.Should().Contain("Finance plan");
            taskHeaders.Should().Contain("Final finance plan");
            taskHeaders.Should().Contain("Gias");
            taskHeaders.Should().Contain("Education brief");
            taskHeaders.Should().Contain("Impact assessment");
            taskHeaders.Should().Contain("Equalities assessment");
            taskHeaders.Should().Contain("Statutory consultation");
            taskHeaders.Should().Contain("Accepted offers evidence");
            taskHeaders.Should().Contain("Due diligence checks");
            taskHeaders.Should().Contain("Project development grant");
            
            sectionHeaders.Should().Contain("About the project");
            sectionHeaders.Should().Contain("Setting-up");
            sectionHeaders.Should().Contain("Reference numbers");
            sectionHeaders.Should().Contain("Project development grant (PDG)");
            sectionHeaders.Should().Contain("Pre-opening");
            sectionHeaders.Should().Contain("Sign-off preparation");
            sectionHeaders.Should().Contain("Getting ready to open");
            sectionHeaders.Should().Contain("After opening");
            sectionHeaders.Should().Contain("Project development grant schedule");

            result.Projects.Count.Should().Be(1);

            var project = result.Projects.First();

            AssertEntry(nameof(ProjectReferenceData.ProjectId), "123", project, columnHeaders);
            AssertEntry(nameof(ProjectReferenceData.ProjectStatus), "Pre-opening", project, columnHeaders);
            AssertEntry(nameof(DatesTask.DateOfEntryIntoPreopening), "01/01/2021", project, columnHeaders);
            AssertEntry(nameof(DatesTask.ProvisionalOpeningDateAgreedWithTrust), "EMPTY", project, columnHeaders);
            AssertEntry(nameof(SchoolTask.Gender), "Boys only", project, columnHeaders);
            AssertEntry(nameof(SchoolTask.FaithType), "EMPTY", project, columnHeaders);
            AssertEntry(nameof(ReferenceNumbersTask.ProjectId), "123", project, columnHeaders);
            AssertEntry(nameof(TrustTask.TrustName), "Test school", project, columnHeaders);
            AssertEntry(nameof(RegionAndLocalAuthorityTask.LocalAuthorityCode), "123", project, columnHeaders);
            AssertEntry(nameof(ConstituencyTask.Name), "Sheffield", project, columnHeaders);
            AssertEntry(nameof(RiskAppraisalMeetingTask.InitialRiskAppraisalMeetingCompleted), "No", project, columnHeaders);
            AssertEntry(nameof(PDGDashboard.PaymentActualDate), "01/02/2024", project, columnHeaders);
            AssertEntry(nameof(KickOffMeetingTask.FundingArrangementAgreed), "Yes", project, columnHeaders);
            AssertEntry(nameof(ArticlesOfAssociationTask.ChairHaveSubmittedConfirmation), "No", project, columnHeaders);
            AssertEntry(nameof(FinancePlanTask.RpaCoverType), "Cover", project, columnHeaders);
            AssertEntry(nameof(FinalFinancePlanTask.Grade6SignedOffFinalPlanDate), "01/01/2023", project, columnHeaders);
            AssertEntry(nameof(GovernancePlanTask.PlanFedBackToTrust), "No", project, columnHeaders);
			AssertEntry(nameof(FundingAgreementTask.SharedFAWithTheTrust), "Yes", project, columnHeaders);
            AssertEntry(nameof(FundingAgreementHealthCheckTask.DraftedFundingAgreementHealthCheck), "Yes", project, columnHeaders);
            AssertEntry(nameof(FundingAgreementSubmissionTask.DraftedFundingAgreementSubmission), "Yes", project, columnHeaders);
            AssertEntry(nameof(GiasTask.CheckedTrustInformation), "Yes", project, columnHeaders);
            AssertEntry(nameof(EducationBriefTask.EducationPlanInEducationBrief), "Yes", project, columnHeaders);
            AssertEntry(nameof(AdmissionsArrangementsTask.TrustConfirmedAdmissionsArrangementsTemplate), "Yes", project, columnHeaders);
            AssertEntry(nameof(ImpactAssessmentTask.ImpactAssessment), "Yes", project, columnHeaders);
            AssertEntry(nameof(EqualitiesAssessmentTask.CompletedEqualitiesProcessRecord), "Yes", project, columnHeaders);
            AssertEntry(nameof(EvidenceOfAcceptedOffersTask.EvidenceOfAcceptedOffers), "Yes" , project, columnHeaders);
            AssertEntry(nameof(OfstedInspectionTask.InspectionBlockDecided), "Yes" , project, columnHeaders);
            AssertEntry(nameof(ApplicationsEvidenceTask.ConfirmedPupilNumbers), "Yes" , project, columnHeaders);
            AssertEntry(nameof(PupilNumbersChecksTask.SchoolReceivedEnoughApplications), "Yes" , project, columnHeaders);
            AssertEntry(nameof(CommissionedExternalExpertTask.CommissionedExternalExpertVisit), "Yes" , project, columnHeaders);
            AssertEntry(nameof(MovingToOpenTask.SentEmailsToRelevantContacts), "Yes" , project, columnHeaders);
            AssertEntry(nameof(PrincipalDesignateTask.CommissionedExternalExpertVisitToSchool), "Yes" , project, columnHeaders);
            
            AssertEntry(nameof(PaymentData.DateOf1stPaymentDue), "01/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf2ndPaymentDue), "02/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf3rdPaymentDue), "03/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf4thPaymentDue), "04/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf5thPaymentDue), "05/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf6thPaymentDue), "06/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf7thPaymentDue), "07/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf8thPaymentDue), "08/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf9thPaymentDue), "09/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf10thPaymentDue), "10/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf11thPaymentDue), "11/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf12thPaymentDue), "12/01/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf1stActualPayment), "01/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf2ndActualPayment), "02/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf3rdActualPayment), "03/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf4thActualPayment), "04/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf5thActualPayment), "05/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf6thActualPayment), "06/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf7thActualPayment), "07/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf8thActualPayment), "08/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf9thActualPayment), "09/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf10thActualPayment), "10/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf11thActualPayment), "11/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.DateOf12thActualPayment), "12/02/2021", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf1stPaymentDue), "1", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf2ndPaymentDue), "2", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf3rdPaymentDue), "3", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf4thPaymentDue), "4", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf5thPaymentDue), "5", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf6thPaymentDue), "6", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf7thPaymentDue), "7", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf8thPaymentDue), "8", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf9thPaymentDue), "9", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf10thPaymentDue), "10", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf11thPaymentDue), "11", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf12thPaymentDue), "12", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf1stPayment), "10", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf2ndPayment), "20", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf3rdPayment), "30", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf4thPayment), "40", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf5thPayment), "50", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf6thPayment), "60", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf7thPayment), "70", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf8thPayment), "80", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf9thPayment), "90", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf10thPayment), "100", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf11thPayment), "110", project, columnHeaders);
            AssertEntry(nameof(PaymentData.AmountOf12thPayment), "120", project, columnHeaders);
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
                        ProjectStatus = "Pre-opening"
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
                        ReferenceNumbers = new ReferenceNumbersTask()
                        {
                            ProjectId = "lkjhgfdsa"
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
                        PDGDashboard = new PDGDashboard()
                        {
                            PaymentActualDate = new DateTime(2024, 2, 1)
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
                        GovernancePlan = new GovernancePlanTask()
                        {
							PlanFedBackToTrust = false,
						},
                        FundingAgreement = new FundingAgreementTask()
                        {
							SharedFAWithTheTrust = true
						},
                        FundingAgreementHealthCheck = new FundingAgreementHealthCheckTask()
                        {
                            DraftedFundingAgreementHealthCheck = true
                        },
                        FundingAgreementSubmission = new FundingAgreementSubmissionTask()
                        {
                            DraftedFundingAgreementSubmission = true
                        },
                        Gias = new GiasTask()
                        {
                            CheckedTrustInformation = true
                        },
                        EducationBrief = new EducationBriefTask()
                        {
                            EducationPlanInEducationBrief = true
                        },
                        AdmissionsArrangements = new AdmissionsArrangementsTask()
                        {
                            TrustConfirmedAdmissionsArrangementsTemplate = true
                        },
                        ImpactAssessment = new ImpactAssessmentTask()
                        {
                            ImpactAssessment = true
                        },
                        EqualitiesAssessment = new EqualitiesAssessmentTask()
                        {
                            CompletedEqualitiesProcessRecord = true
                        },
                        StatutoryConsultation = new StatutoryConsultationTask()
                        {
                            ExpectedDateForReceivingFindingsFromTrust = new DateTime(2024, 1, 1),
                        },
                        EvidenceOfAcceptedOffers = new EvidenceOfAcceptedOffersTask()
                        {
                            EvidenceOfAcceptedOffers = true,
                        },
                        OfstedInspection = new OfstedInspectionTask()
                        {
                            InspectionBlockDecided = true,
                        },
                        ApplicationsEvidence = new ApplicationsEvidenceTask()
                        {
                            ConfirmedPupilNumbers = true,
                        },
                        FinalFinancePlan = new FinalFinancePlanTask()
                        {
                            Grade6SignedOffFinalPlanDate = new DateTime(2023, 1, 1),
                        },
                        PupilNumbersChecks = new PupilNumbersChecksTask()
                        {
                            SchoolReceivedEnoughApplications = true,
                        },
                        CommissionedExternalExpert = new CommissionedExternalExpertTask()
                        {
                        CommissionedExternalExpertVisit = true,
                        },
                        MovingToOpen = new MovingToOpenTask()
                        {
                            SentEmailsToRelevantContacts = true
                        },
                        PrincipalDesignate = new PrincipalDesignateTask()
                        {
                            CommissionedExternalExpertVisitToSchool = YesNoNotApplicable.Yes
                        }, 
                        DueDiligenceChecks = new DueDiligenceChecks
                        {
                            RequestedCounterExtremismChecks = true
                        }
                    },
                    Payments = new PaymentData()
                    {
                        DateOf1stPaymentDue = new DateTime(2021, 1, 1),
                        AmountOf1stPaymentDue = "1",
                        DateOf2ndPaymentDue = new DateTime(2021, 1, 2),
                        AmountOf2ndPaymentDue = "2",
                        DateOf3rdPaymentDue = new DateTime(2021, 1, 3),
                        AmountOf3rdPaymentDue = "3",
                        DateOf4thPaymentDue = new DateTime(2021, 1, 4),
                        AmountOf4thPaymentDue = "4",
                        DateOf5thPaymentDue = new DateTime(2021, 1, 5),
                        AmountOf5thPaymentDue = "5",
                        DateOf6thPaymentDue = new DateTime(2021, 1, 6),
                        AmountOf6thPaymentDue = "6",
                        DateOf7thPaymentDue = new DateTime(2021, 1, 7),
                        AmountOf7thPaymentDue = "7",
                        DateOf8thPaymentDue = new DateTime(2021, 1, 8),
                        AmountOf8thPaymentDue = "8",
                        DateOf9thPaymentDue = new DateTime(2021, 1, 9),
                        AmountOf9thPaymentDue = "9",
                        DateOf10thPaymentDue = new DateTime(2021, 1, 10),
                        AmountOf10thPaymentDue = "10",
                        DateOf11thPaymentDue = new DateTime(2021, 1, 11),
                        AmountOf11thPaymentDue = "11",
                        DateOf12thPaymentDue = new DateTime(2021, 1, 12),
                        AmountOf12thPaymentDue = "12",
                        DateOf1stActualPayment = new DateTime(2021, 2, 1),
                        AmountOf1stPayment = "10",
                        DateOf2ndActualPayment = new DateTime(2021, 2, 2),
                        AmountOf2ndPayment = "20",
                        DateOf3rdActualPayment = new DateTime(2021, 2, 3),
                        AmountOf3rdPayment = "30",
                        DateOf4thActualPayment = new DateTime(2021, 2, 4),
                        AmountOf4thPayment = "40",
                        DateOf5thActualPayment = new DateTime(2021, 2, 5),
                        AmountOf5thPayment = "50",
                        DateOf6thActualPayment = new DateTime(2021, 2, 6),
                        AmountOf6thPayment = "60",
                        DateOf7thActualPayment = new DateTime(2021, 2, 7),
                        AmountOf7thPayment = "70",
                        DateOf8thActualPayment = new DateTime(2021, 2, 8),
                        AmountOf8thPayment = "80",
                        DateOf9thActualPayment = new DateTime(2021, 2, 9),
                        AmountOf9thPayment = "90",
                        DateOf10thActualPayment = new DateTime(2021, 2, 10),
                        AmountOf10thPayment = "100",
                        DateOf11thActualPayment = new DateTime(2021, 2, 11),
                        AmountOf11thPayment = "110",
                        DateOf12thActualPayment = new DateTime(2021, 2, 12),
                        AmountOf12thPayment = "120",
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
