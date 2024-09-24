using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit
{
    public record BulkEditValidateRequest
    {
        public List<HeaderInfo> Headers { get; set; }
        public List<RowInfo> Rows { get; set; }
        public Options Options { get; set; }

    }

    public record HeaderInfo
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }

    public record RowInfo
    {
        public int FileRowIndex { get; set; }
        public List<ColumnInfo> Columns { get; set; }
    }

    public record ColumnInfo
    {
        public int ColumnIndex { get; set; }
        public string Value { get; set; }
    }

    public record Options
    {
        public bool SkipBlankEntry { get; set; }
    }

}
