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
using System.Data;

namespace Dfe.ManageFreeSchoolProjects.Pages.BulkEdit
{
    public class BulkEditFileUploadModel(
                    IBulkEditValidateService bulkEditValidateService,
                    IBulkEditCommitService bulkEditCommitService,
                    IBulkEditFileReader bulkEditFileReader,
                    IBulkEditCache bulkEditCache,
                    IBulkEditFileValidator bulkEditFileValidator,
                    ILogger<BulkEditFileUploadModel> logger) : PageModel
    {
        [BindProperty]
        public IFormFile Upload { get; set; }

        public IEnumerable<RowViewModel> Rows { get; set; }

        public bool HasErrors { get; set; }

        public string FileError { get; set; }

        public string BackLink { get; set; }

        public IActionResult OnGet()
        {
            if (!User.IsInRole(RolesConstants.ProjectRecordCreator))
            {
                return new UnauthorizedResult();
            }

            SetToUpdateMultipleFields();

            bulkEditCache.Delete();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            logger.LogMethodEntered();

            if(Upload == null)
            {
                FileError = "Select a file";
                SetToUpdateMultipleFields();
                return Page();
            }

            if(!Upload.FileName.EndsWith(".csv") && !Upload.FileName.EndsWith(".xlsx"))
            {
                FileError = "The selected file must be an Excel spreadsheet or CSV";
                SetToUpdateMultipleFields();
                return Page();
            }

            try
            {
                using MemoryStream stream = new MemoryStream();

                await Upload.CopyToAsync(stream);

                var dataset = ExcelToDataSetBuilder.Build(stream, Upload.ContentType);

                var table = GetRelevantDataTable(dataset);

                var fileValidation = bulkEditFileValidator.Validate(table);

                if (!fileValidation.IsValid)
                {
                    FileError = fileValidation.ErrorMessage;
                    SetToUpdateMultipleFields();
                    return Page();
                }

                var request = bulkEditFileReader.Read(table);

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
                    SetToCheckYourAnswers();
                }
                else
                {
                    var errorCount = Rows.Count(x => x.Cells.Any(y => !string.IsNullOrEmpty(y.Error)));
                    FileError = $"The upload tab has {errorCount} validation errors";
                    SetToUpdateMultipleFields();
                }
            }

            catch (Exception ex)
            {
                FileError = "File could not be read";
                SetToUpdateMultipleFields();
                logger.LogErrorMsg(ex);
            }

            return Page();

        }

        private void SetToCheckYourAnswers()
        {
            ViewData["Title"] = "Check your answers";
            BackLink = RouteConstants.BulkEditFileUpload;
        }

        private void SetToUpdateMultipleFields()
        {
            ViewData["Title"] = "Update multiple fields";
            BackLink = "/";
        }

        public async Task<IActionResult> OnPostCommit()
        {
            logger.LogMethodEntered();

            try
            {
                var request = bulkEditCache.Get();
                await bulkEditCommitService.Execute(request);
                var requestCount = request.Rows.Count();
                return Redirect(RouteConstants.BulkEditFileComplete + $"?count={requestCount}");
            }

            catch (Exception ex)
            {
                logger.LogErrorMsg(ex);
            }

            return Page();

        }

        private DataTable GetRelevantDataTable(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                if (table.TableName == "Upload")
                {
                    return table;
                }
            }

            return dataSet.Tables[0];
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
