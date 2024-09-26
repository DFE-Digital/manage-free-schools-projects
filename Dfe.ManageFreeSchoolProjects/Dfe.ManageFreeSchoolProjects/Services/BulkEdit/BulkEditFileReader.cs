using System.Data;
using System.IO;
using System;
using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using System.Collections.Generic;

namespace Dfe.ManageFreeSchoolProjects.Services.BulkEdit
{
    public interface IBulkEditFileReader
    {
        BulkEditRequest Read(DataTable table);
    }

    public class BulkEditFileReader : IBulkEditFileReader
    {
        public BulkEditRequest Read(DataTable table)
        {

            BulkEditRequest projectTable = ReadProjectTable(table);

            return projectTable;
        }


        private static BulkEditRequest ReadProjectTable(DataTable table)
        {
            BulkEditRequest request = new BulkEditRequest();

            request.Headers = new List<HeaderInfo>();
            request.Rows = new List<RowInfo>();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                var col = table.Columns[i];
                request.Headers.Add(new() { Index = i, Name = col.ColumnName });
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var info = new RowInfo { FileRowIndex = i + 1 };

                info.Columns = new List<ColumnInfo>();

                for (int j = 0; j < row.ItemArray.Length; j++)
                {
                    info.Columns.Add(new() { ColumnIndex = j, Value = ParseColumn(row.ItemArray[j]) });
                }

                request.Rows.Add(info);
            }

            return request;
        }

        private static string ParseColumn(object column)
        {
            return column != DBNull.Value ? column.ToString() : null;
        }
    }
}
