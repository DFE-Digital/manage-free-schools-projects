using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.AdmissionsArrangements;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ApplicationsEvidence;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ArticlesOfAssociation;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.CommissionedExternalExpert;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Constituency;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Dates;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.DraftGovernancePlan;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.EducationBrief;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FinancePlan;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Gias;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ImpactAssessment;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.KickOffMeeting;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ModelFundingAgreement;
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
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.FinalFinancePlan;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.MovingToOpen;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PupilNumbersChecks;

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
                                      Trust = TrustTaskBuilder.Build(kpi),
                                      RegionAndLocalAuthority = RegionAndLocalAuthorityTaskBuilder.Build(kpi),
                                      Constituency = ConstituencyTaskBuilder.Build(kpi),
                                      RiskAppraisalMeeting = RiskAppraisalMeetingTaskBuilder.Build(riskAppraisalMeetingTask),
                                      KickOffMeeting = KickOffMeetingTaskBuilder.Build(kpi, milestones),
                                      ModelFundingAgreement = ModelFundingAgreementTaskBuilder.Build(milestones),
                                      FundingAgreementHealthCheck = FundingAgreementHealthCheckTaskBuilder.Build(milestones),
                                      ArticlesOfAssociation = ArticlesOfAssociationTaskBuilder.Build(milestones),
                                      FinancePlan = FinancePlanTaskBuilder.Build(milestones, po),
                                      DraftGovernancePlan = DraftGovernancePlanTaskBuilder.Build(milestones),
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
                                      MovingToOpen = MovingToOpenTaskBuilder.Build(kpi,milestones)
                                  },
                                  ProjectReferenceData = new ProjectReferenceData()
                                  {
                                      ProjectId = kpi.ProjectStatusProjectId,
                                      ApplicationNumber = kpi.ProjectStatusFreeSchoolsApplicationNumber,
                                      Urn = kpi.ProjectStatusUrnWhenGivenOne,
                                      ApplicationWave = kpi.ProjectStatusFreeSchoolApplicationWave,
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
    }

    public class ProjectReferenceData
    {
        public string ProjectId { get; set; }
        public string Urn { get; set; }
        public string ApplicationNumber { get; set; }
        public string ApplicationWave { get; set; }

    }
}
