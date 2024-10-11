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
            if(table.Columns.Count == 0)
            {
                return new FileValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "The selected file must have project data in it"
                };
            }

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
                        ErrorMessage = $"File has an invalid column header: {column.ColumnName}"
                    };
                }

                if (table.Rows.Count == 0)
                {
                    return new FileValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "The selected file must have project data in it"
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
