using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.BulkEdit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Dfe.ManageFreeSchoolProjects.Services.Project;

namespace Dfe.ManageFreeSchoolProjects.Pages.BulkEdit
{
    public class BulkEditFileUploadModel(
                    IBulkEditValidateService bulkEditValidateService,
                    IBulkEditFileReader bulkEditFileReader,
                    IBulkEditCache bulkEditCache,
                    ILogger<BulkEditFileUploadModel> logger) : PageModel
    {


        [BindProperty]
        public IFormFile Upload { get; set; }

        public IEnumerable<RowViewModel> Rows { get; set; }

        public bool HasErrors { get; set; }

        public IActionResult OnGet()
        {
            if (!User.IsInRole(RolesConstants.ProjectRecordCreator))
            {
                return new UnauthorizedResult();
            }

            bulkEditCache.Delete();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            logger.LogMethodEntered();

            try
            {
                using MemoryStream stream = new MemoryStream();

                await Upload.CopyToAsync(stream);

                var dataset = ExcelToDataSetBuilder.Build(stream, Upload.ContentType);

                var request = bulkEditFileReader.Read(dataset.Tables[0]);

                var response = (await bulkEditValidateService.Execute(request)).Data;

                var headerLookup = response.Headers.ToDictionary(x => x.Index, x => x.Name);

                Rows = response.ValidationResultRows.Select(x => new RowViewModel
                {
                    RowNumber = x.FileRowIndex,
                    Cells = x.Columns.Select(y => new CellViewModel
                    {
                        Value = y.NewValue,
                        ColumnName = headerLookup[y.ColumnIndex],
                        Error = y.Error
                    })
                });

                HasErrors = Rows.Any(x => x.Cells.Any(y => !string.IsNullOrEmpty(y.Error)));

                if(!HasErrors)
                {
                    bulkEditCache.Update(request);
                }
            }

            catch (Exception ex)
            {
                logger.LogErrorMsg(ex);
            }

            return Page();

        }

        public record RowViewModel
        {
            public int RowNumber { get; set; }
            public IEnumerable<CellViewModel> Cells { get; set; }
        }

        public record CellViewModel
        {
            public string ColumnName { get; set; }
            public string Value { get; set; }
            public string Error { get; set; }
        }
    }
}
