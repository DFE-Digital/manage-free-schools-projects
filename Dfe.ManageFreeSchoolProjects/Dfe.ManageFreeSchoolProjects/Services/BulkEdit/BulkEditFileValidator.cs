using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using System;
using System.Data;
using System.Linq;

namespace Dfe.ManageFreeSchoolProjects.Services.BulkEdit
{

    public record FileValidationResult
    {
        public bool IsValid { get; init; }
        public string ErrorMessage { get; init; }
    }
    public interface IBulkEditFileValidator
    {
        FileValidationResult Validate(DataTable table);
    }

    public class BulkEditFileValidator : IBulkEditFileValidator
    {
        public FileValidationResult Validate(DataTable table)
        {

            foreach (DataColumn column in table.Columns)
            {
                if (column.ColumnName.StartsWith("Column"))
                {
                    return new FileValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "File has an empty column header"
                    };
                }

                if (!HeaderNames.AllHeaders.Contains(column.ColumnName, StringComparer.CurrentCultureIgnoreCase))
                {
                    return new FileValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"File has a invalid column header: {column.ColumnName}"
                    };
                }
            }

            return new FileValidationResult
            {
                IsValid = true
            };
        }
    }
}
