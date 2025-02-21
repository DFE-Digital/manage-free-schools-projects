using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.AdmissionsArrangements;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ApplicationsEvidence;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ArticlesOfAssociation;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.CommissionedExternalExpert;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Constituency;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Dates;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.GovernancePlan;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DueDiligenceChecks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.EducationBrief;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FinancePlan;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Gias;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ImpactAssessment;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.KickOffMeeting;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreement;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RegionAndLocalAuthority;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RiskAppraisalMeeting;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Trusts;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.EqualitiesAssessment;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.StatutoryConsultation;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.EvidenceOfAcceptedOffers;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.OfstedInspection;
using Dfe.ManageFreeSchoolProjects.Data;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreementHealthCheck;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FundingAgreementSubmission;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FinalFinancePlan;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.MovingToOpen;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PupilNumbersChecks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PDG;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PrincipalDesignate;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ReadinessToOpenMeeting;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ReferenceNumbers;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PreFundingAgreementCheckpointMeeting;


namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Reports
{
    public interface IAllProjectsReportService
    {
        public Task<MemoryStream> Execute();
        
        public Task<MemoryStream> ExecuteWithFilter(string projectIds);
    }

    public class AllProjectsReportService : IAllProjectsReportService
    {
        private readonly MfspContext _context;

        public AllProjectsReportService(MfspContext context)
        {
            _context = context;
        }

        public async Task<MemoryStream> Execute()
        {
            ProjectReport projectReport = await BuildProjectReport();

            return CreateMemoryStream(projectReport);
        }
        
        public async Task<MemoryStream> ExecuteWithFilter(string projectIds)
        {
            ProjectReport projectReport = await BuildFilteredProjectReport(projectIds);

            return CreateMemoryStream(projectReport);
        }

        private static MemoryStream CreateMemoryStream(ProjectReport projectReport)
        {
            MemoryStream memoryStream = new MemoryStream();
            using SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook);

            WorkbookPart workbookPart = BuildWorkbook(spreadsheetDocument);
            WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            var headerRows = BuildHeaderRows(projectReport);

            sheetData.Append(headerRows.Section);
            sheetData.Append(headerRows.TaskName);
            sheetData.Append(headerRows.ColumnName);

            foreach (var project in projectReport.Projects)
            {
                Row projectRow = BuildProjectRow(project);

                sheetData.Append(projectRow);
            }

            var sheetMergedCells = BuildMergedCells(projectReport);
            worksheetPart.Worksheet.InsertAfter(sheetMergedCells, sheetData);

            return memoryStream;
        }

        private async Task<ProjectReport> BuildProjectReport()
        {
            
           var result = ProjectReportBuilder.Build(await CreateReportList());

           return result;
        }
        
        private  async Task<ProjectReport> BuildFilteredProjectReport(String projectIds)
        {
            var data = await CreateReportList();
            
            IEnumerable<string> listOfIds = projectIds.Split(new char[] { ',' }).ToList();
            
            var filteredreport = data.Where(x => listOfIds.Contains(x.ProjectReferenceData.ProjectId)).ToList();

            var result = ProjectReportBuilder.Build(filteredreport);

            return result;
        }
        
        private async Task<List<ProjectReportSourceData>> CreateReportList()
        {
             var data = await (from kpi in _context.Kpi
                              join riskAppraisalMeetingTask in _context.RiskAppraisalMeetingTask on kpi.Rid equals riskAppraisalMeetingTask.RID into riskAppraisalMeetingTaskJoin
                              from riskAppraisalMeetingTask in riskAppraisalMeetingTaskJoin.DefaultIfEmpty()
                              join milestones in _context.Milestones on kpi.Rid equals milestones.Rid into joinedMilestones
                              from milestones in joinedMilestones.DefaultIfEmpty()
                              join po in _context.Po on kpi.Rid equals po.Rid into joinedPo
                              from po in joinedPo.DefaultIfEmpty()
                              select new ProjectReportSourceData()
                              {
                                  TaskInformation = new GetProjectByTaskResponse()
                                  {
                                      Dates = DatesTaskBuilder.Build(kpi),
                                      School = SchoolTaskBuilder.Build(kpi),
                                      ReferenceNumbers = ReferenceNumbersTaskBuilder.Build(kpi),
                                      Trust = TrustTaskBuilder.Build(kpi),
                                      RegionAndLocalAuthority = RegionAndLocalAuthorityTaskBuilder.Build(kpi),
                                      Constituency = ConstituencyTaskBuilder.Build(kpi),
                                      RiskAppraisalMeeting = RiskAppraisalMeetingTaskBuilder.Build(riskAppraisalMeetingTask),
                                      PDGDashboard = PDGDashboardBuilder.Build(po),
                                      KickOffMeeting = KickOffMeetingTaskBuilder.Build(kpi, milestones),
                                      PreFundingAgreementCheckpointMeetingTask = PreFundingAgreementCheckpointMeetingTaskBuilder.Build(milestones),
                                      FundingAgreement = FundingAgreementTaskBuilder.Build(milestones),
                                      FundingAgreementHealthCheck = FundingAgreementHealthCheckTaskBuilder.Build(milestones),
                                      FundingAgreementSubmission = FundingAgreementSubmissionTaskBuilder.Build(milestones),
                                      ArticlesOfAssociation = ArticlesOfAssociationTaskBuilder.Build(milestones),
                                      FinancePlan = FinancePlanTaskBuilder.Build(milestones, po),
                                      GovernancePlan = GovernancePlanTaskBuilder.Build(milestones),
                                      Gias = GiasTaskBuilder.Build(milestones),
                                      EducationBrief = EducationBriefTaskBuilder.Build(milestones),
                                      AdmissionsArrangements = AdmissionsArrangementsTaskBuilder.Build(milestones),
                                      ImpactAssessment = ImpactAssessmentTaskBuilder.Build(milestones),
                                      EqualitiesAssessment = EqualitiesAssessmentTaskBuilder.Build(milestones),
                                      StatutoryConsultation = StatutoryConsultationTaskBuilder.Build(milestones),
                                      EvidenceOfAcceptedOffers = EvidenceOfAcceptedOffersTaskBuilder.Build(milestones),
                                      OfstedInspection = OfstedInspectionTaskBuilder.Build(milestones),
                                      ApplicationsEvidence = ApplicationsEvidenceTaskBuilder.Build(milestones),
                                      FinalFinancePlan = FinalFinancePlanTaskBuilder.Build(milestones),
                                      PupilNumbersChecks = PupilNumbersChecksTaskBuilder.Build(milestones),
                                      CommissionedExternalExpert = CommissionedExternalExpertTaskBuilder.Build(milestones),
                                      MovingToOpen = MovingToOpenTaskBuilder.Build(kpi,milestones),
                                      PrincipalDesignate = PrincipalDesignateTaskBuilder.Build(milestones),
                                      DueDiligenceChecks = DueDiligenceChecksTaskBuilder.Build(milestones), 
                                      ReadinessToOpenMeetingTask = ROMTaskBuilder.Build(milestones)
                                  },
                                  ProjectReferenceData = new ProjectReferenceData()
                                  {
                                      ProjectId = kpi.ProjectStatusProjectId,
                                      ProjectStatus = kpi.ProjectStatusProjectStatus,
                                      ApplicationNumber = kpi.ProjectStatusFreeSchoolsApplicationNumber,
                                      Urn = kpi.ProjectStatusUrnWhenGivenOne,
                                      ApplicationWave = kpi.ProjectStatusFreeSchoolApplicationWave,
                                  },
                                  Contacts = new ContactsData()
                                  {
                                      ProjectAssignedTo = new ProjectAssignedToContact() { ProjectAssignedToName = kpi.KeyContactsFsgLeadContact, ProjectAssignedToEmail = kpi.KeyContactsFsgLeadContactEmail },
                                      Grade6 = new Grade6Contact() { Grade6Name = kpi.KeyContactsFsgGrade6, Grade6Email = kpi.KeyContactsFsgGrade6Email },
                                  },
                                  Payments = new PaymentData()
                                  {
                                      DateOf1stPaymentDue = po.ProjectDevelopmentGrantFundingDateOf1stPaymentDue,
                                      AmountOf1stPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf1stPaymentDue,
                                      DateOf2ndPaymentDue = po.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue,
                                      AmountOf2ndPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf2ndPaymentDue,
                                      DateOf3rdPaymentDue = po.ProjectDevelopmentGrantFundingDateOf3rdPaymentDue,
                                      AmountOf3rdPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf3rdPaymentDue,
                                      DateOf4thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf4thPaymentDue,
                                      AmountOf4thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf4thPaymentDue,
                                      DateOf5thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf5thPaymentDue,
                                      AmountOf5thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf5thPaymentDue,
                                      DateOf6thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf6thPaymentDue,
                                      AmountOf6thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf6thPaymentDue,
                                      DateOf7thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf7thPaymentDue,
                                      AmountOf7thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf7thPaymentDue,
                                      DateOf8thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf8thPaymentDue,
                                      AmountOf8thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf8thPaymentDue,
                                      DateOf9thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf9thPaymentDue,
                                      AmountOf9thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf9thPaymentDue,
                                      DateOf10thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf10thPaymentDue,
                                      AmountOf10thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf10thPaymentDue,
                                      DateOf11thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue,
                                      AmountOf11thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue,
                                      DateOf12thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf12thPaymentDue,
                                      AmountOf12thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf12thPaymentDue,
                                      DateOf1stActualPayment = po.ProjectDevelopmentGrantFundingDateOf1stActualPayment,
                                      AmountOf1stPayment = po.ProjectDevelopmentGrantFundingAmountOf1stPayment,
                                      DateOf2ndActualPayment = po.ProjectDevelopmentGrantFundingDateOf2ndActualPayment,
                                      AmountOf2ndPayment = po.ProjectDevelopmentGrantFundingAmountOf2ndPayment,
                                      DateOf3rdActualPayment = po.ProjectDevelopmentGrantFundingDateOf3rdActualPayment,
                                      AmountOf3rdPayment = po.ProjectDevelopmentGrantFundingAmountOf3rdPayment,
                                      DateOf4thActualPayment = po.ProjectDevelopmentGrantFundingDateOf4thActualPayment,
                                      AmountOf4thPayment = po.ProjectDevelopmentGrantFundingAmountOf4thPayment,
                                      DateOf5thActualPayment = po.ProjectDevelopmentGrantFundingDateOf5thActualPayment,
                                      AmountOf5thPayment = po.ProjectDevelopmentGrantFundingAmountOf5thPayment,
                                      DateOf6thActualPayment = po.ProjectDevelopmentGrantFundingDateOf6thActualPayment,
                                      AmountOf6thPayment = po.ProjectDevelopmentGrantFundingAmountOf6thPayment,
                                      DateOf7thActualPayment = po.ProjectDevelopmentGrantFundingDateOf7thActualPayment,
                                      AmountOf7thPayment = po.ProjectDevelopmentGrantFundingAmountOf7thPayment,
                                      DateOf8thActualPayment = po.ProjectDevelopmentGrantFundingDateOf8thActualPayment,
                                      AmountOf8thPayment = po.ProjectDevelopmentGrantFundingAmountOf8thPayment,
                                      DateOf9thActualPayment = po.ProjectDevelopmentGrantFundingDateOf9thActualPayment,
                                      AmountOf9thPayment = po.ProjectDevelopmentGrantFundingAmountOf9thPayment,
                                      DateOf10thActualPayment = po.ProjectDevelopmentGrantFundingDateOf10thActualPayment,
                                      AmountOf10thPayment = po.ProjectDevelopmentGrantFundingAmountOf10thPayment,
                                      DateOf11thActualPayment = po.ProjectDevelopmentGrantFundingDateOf11thActualPayment,
                                      AmountOf11thPayment = po.ProjectDevelopmentGrantFundingAmountOf11thPayment,
                                      DateOf12thActualPayment = po.ProjectDevelopmentGrantFundingDateOf12thActualPayment,
                                      AmountOf12thPayment = po.ProjectDevelopmentGrantFundingAmountOf12thPayment,
                                  }
                              }).ToListAsync();
             return data;
        }
        private static WorkbookPart BuildWorkbook(SpreadsheetDocument spreadsheetDocument)
        {
            WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());

            Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Projects" };
            sheets.Append(sheet);

            return workbookPart;
        }

        private static ProjectHeaderRows BuildHeaderRows(ProjectReport projectReport)
        {
            Row sectionRow = new Row();
            Row taskRow = new Row();
            Row columnRow = new Row();

            foreach (var header in projectReport.Headers)
            {
                sectionRow.Append(new Cell() { CellValue = new CellValue(header.Section), DataType = CellValues.String });
                taskRow.Append(new Cell() { CellValue = new CellValue(header.TaskName), DataType = CellValues.String });
                columnRow.Append(new Cell() { CellValue = new CellValue(header.ColumnName), DataType = CellValues.String });
            }

            var result = new ProjectHeaderRows()
            {
                Section = sectionRow,
                TaskName = taskRow,
                ColumnName = columnRow
            };

            return result;
        }

        private static Row BuildProjectRow(ProjectDataRow project)
        {
            Row result = new Row();

            foreach (var column in project.Values)
            {
                result.Append(new Cell() { CellValue = new CellValue(column.Value), DataType = CellValues.String });
            }

            return result;
        }

        private static MergeCells BuildMergedCells(ProjectReport projectReport)
        {
            var groupedSections = projectReport.Headers.GroupBy(h => h.Section).ToDictionary(h => h.Key, h => h.ToList());
            var groupedTasks = projectReport.Headers.GroupBy(h => h.TaskName).ToDictionary(h => h.Key, h => h.ToList());

            var sectionMergedCells = MergedCellBuilder.Build(1, groupedSections);
            var taskMergedCells = MergedCellBuilder.Build(2, groupedTasks);

            var result = new MergeCells();
            result.Append(sectionMergedCells);
            result.Append(taskMergedCells);

            return result;
        }

        private sealed record ProjectHeaderRows
        {
            public Row Section { get; set; }
            public Row TaskName { get; set; }
            public Row ColumnName { get; set; }
        }
    }

    public class ProjectReportSourceData
    {
        public ProjectReferenceData ProjectReferenceData { get; set; }
        public GetProjectByTaskResponse TaskInformation { get; set; }
        public ContactsData Contacts { get; set; }
        public PaymentData Payments { get; set; }

    }

    public class ProjectReferenceData
    {
        public string ProjectId { get; set; }
        public string Urn { get; set; }
        
        public string ProjectStatus  { get; set; }
        public string ApplicationNumber { get; set; }
        public string ApplicationWave { get; set; }
    }

    public class ContactsData
    {
        public ProjectAssignedToContact ProjectAssignedTo { get; set; }

        public Grade6Contact Grade6 { get; set; }
    }

    public record ProjectAssignedToContact
    {
        public string ProjectAssignedToName { get; set; }
        public string ProjectAssignedToEmail { get; set; }

    }

    public record Grade6Contact
    {
        public string Grade6Name { get; set; }
        public string Grade6Email { get; set; }

    }

    public class PaymentData
    {
        public DateTime? DateOf1stPaymentDue { get; set; }
        public string AmountOf1stPaymentDue { get; set; }
        public DateTime? DateOf2ndPaymentDue { get; set; }
        public string AmountOf2ndPaymentDue { get; set; }
        public DateTime? DateOf3rdPaymentDue { get; set; }
        public string AmountOf3rdPaymentDue { get; set; }
        public DateTime? DateOf4thPaymentDue { get; set; }
        public string AmountOf4thPaymentDue { get; set; }
        public DateTime? DateOf5thPaymentDue { get; set; }
        public string AmountOf5thPaymentDue { get; set; }
        public DateTime? DateOf6thPaymentDue { get; set; }
        public string AmountOf6thPaymentDue { get; set; }
        public DateTime? DateOf7thPaymentDue { get; set; }
        public string AmountOf7thPaymentDue { get; set; }
        public DateTime? DateOf8thPaymentDue { get; set; }
        public string AmountOf8thPaymentDue { get; set; }
        public DateTime? DateOf9thPaymentDue { get; set; }
        public string AmountOf9thPaymentDue { get; set; }
        public DateTime? DateOf10thPaymentDue { get; set; }
        public string AmountOf10thPaymentDue { get; set; }
        public DateTime? DateOf11thPaymentDue { get; set; }
        public string AmountOf11thPaymentDue { get; set; }
        public DateTime? DateOf12thPaymentDue { get; set; }
        public string AmountOf12thPaymentDue { get; set; }
        public DateTime? DateOf1stActualPayment { get; set; }
        public string AmountOf1stPayment { get; set; }
        public DateTime? DateOf2ndActualPayment { get; set; }
        public string AmountOf2ndPayment { get; set; }
        public DateTime? DateOf3rdActualPayment { get; set; }
        public string AmountOf3rdPayment { get; set; }
        public DateTime? DateOf4thActualPayment { get; set; }
        public string AmountOf4thPayment { get; set; }
        public DateTime? DateOf5thActualPayment { get; set; }
        public string AmountOf5thPayment { get; set; }
        public DateTime? DateOf6thActualPayment { get; set; }
        public string AmountOf6thPayment { get; set; }
        public DateTime? DateOf7thActualPayment { get; set; }
        public string AmountOf7thPayment { get; set; }
        public DateTime? DateOf8thActualPayment { get; set; }
        public string AmountOf8thPayment { get; set; }
        public DateTime? DateOf9thActualPayment { get; set; }
        public string AmountOf9thPayment { get; set; }
        public DateTime? DateOf10thActualPayment { get; set; }
        public string AmountOf10thPayment { get; set; }
        public DateTime? DateOf11thActualPayment { get; set; }
        public string AmountOf11thPayment { get; set; }
        public DateTime? DateOf12thActualPayment { get; set; }
        public string AmountOf12thPayment { get; set; }
    }

}
