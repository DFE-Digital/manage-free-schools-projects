namespace Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit
{
    public static class HeaderNames
    {
        public const string ProjectId = "Project Id";
        public const string SchoolName = "School Name";
        public const string LocalAuthority = "Local Authority";
        public const string OpeningDate = "Opening Date";
        public const string Region = "Region";

        public static string[] AllHeaders
        {
            get
            {
                return
                [
                    ProjectId,
                    SchoolName,
                    LocalAuthority,
                    Region,
                    OpeningDate,
                ];
            }
        }
    }

    public record BulkEditRequest
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
