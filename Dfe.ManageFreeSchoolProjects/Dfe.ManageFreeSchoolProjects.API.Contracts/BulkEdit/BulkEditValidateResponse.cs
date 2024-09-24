using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit
{
    public record BulkEditValidateResponse
    {
       public List<HeaderInfo> Headers { get; set; }
        public List<InvalidRowInfo> InvalidRows { get; set; }
        public List<ValidRowInfo> ValidRows { get; set; }

    }

    public record InvalidRowInfo
    {
        public int FileRowIndex { get; set; }
        public List<ErrorInfo> Errors { get; set; }
    }

    public record ErrorInfo
    {
        public int ColumnIndex { get; set; }
        public string Error { get; set; }
    }

    public record ValidRowInfo
    {
        public int FileRowIndex { get; set; }
        public List<ValueChangeInfo> Columns { get; set; }
    }

    public record ValueChangeInfo
    {
        public int ColumnIndex { get; set; }
        public string CurrentValue { get; set; }
        public string NewValue { get; set; }
    }
}
