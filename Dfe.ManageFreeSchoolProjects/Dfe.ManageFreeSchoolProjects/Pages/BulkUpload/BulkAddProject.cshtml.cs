using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Pages.BulkUpload
{
    public class BulkAddProjectModel : PageModel
    {
        public BulkAddProjectModel()
        {
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public ProjectTable ProjectTable { get; set; }

        public async Task OnPostAsync()
        {
            using MemoryStream stream = new MemoryStream();

            await Upload.CopyToAsync(stream);

            using var reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                    // Possibly could allow the user to tell us this information in the form
                    UseHeaderRow = true,
                }
            });

            var table = dataSet.Tables[0];

            ProjectTable projectTable = new ProjectTable();

            foreach (DataRow row in table.Rows)
            {
                if (row.ItemArray.Length != 7)
                {
                    throw new Exception("Each row must have length 7");
                }

                var projectRow = new ProjectRow()
                {
                    ProjectTitle = ParseColumn<string>(row.ItemArray[0]),
                    ProjectId = ParseColumn<string>(row.ItemArray[1]),
                    TrustName = ParseColumn<string>(row.ItemArray[2]),
                    Region = ParseColumn<string>(row.ItemArray[3]),
                    LocalAuthority = ParseColumn<string>(row.ItemArray[4]),
                    RealisticOpeningDate = ParseColumn<DateTime>(row.ItemArray[5]),
                    Status = ParseColumn<string>(row.ItemArray[6]),
                };

                projectTable.Rows.Add(projectRow);
            }

            ProjectTable = projectTable;
        }

        private static T ParseColumn<T>(object column)
        {
            return column != DBNull.Value ? (T)column : default(T);
        }
    }

    public record ProjectTable
    {
        public List<ProjectRow> Rows { get; set; } = new();
    }

    public record ProjectRow
    {
        public string ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string TrustName { get; set; }
        public string Region { get; set; }
        public string LocalAuthority { get; set; }
        public DateTime RealisticOpeningDate { get; set; }
        public string Status { get; set; }
    }
}
