using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Dates;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RegionAndLocalAuthority;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Trusts;
using Dfe.ManageFreeSchoolProjects.Data;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Reports
{
    public interface IAllProjectsReportService
    {
        public Task<MemoryStream> Execute();
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
            var data = await _context.Kpi.Select(kpi => new GetProjectByTaskResponse()
            {
                Dates = DatesTaskBuilder.Build(kpi),
                School = SchoolTaskBuilder.Build(kpi),
                Trust = TrustTaskBuilder.Build(kpi),
                RegionAndLocalAuthority = RegionAndLocalAuthorityTaskBuilder.Build(kpi)
            }).ToListAsync();

            var result = ProjectReportBuilder.Build(new ProjectReportBuilderParameters()
            {
                Projects = data
            });

            return result;
        }

        private WorkbookPart BuildWorkbook(SpreadsheetDocument spreadsheetDocument)
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

        private MergeCells BuildMergedCells(ProjectReport projectReport)
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

        private record ProjectHeaderRows
        {
            public Row Section { get; set; }
            public Row TaskName { get; set; }
            public Row ColumnName { get; set; }
        }
    }
}
