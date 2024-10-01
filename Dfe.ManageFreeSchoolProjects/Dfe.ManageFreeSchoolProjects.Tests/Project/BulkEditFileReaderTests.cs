using Dfe.ManageFreeSchoolProjects.Services.BulkEdit;
using FluentAssertions;
using System.Data;

namespace Dfe.ManageFreeSchoolProjects.Tests.Project
{
    public class BulkEditFileReaderTests
    {
        [Fact]
        public void BasicFileBuildsRequest()
        {
            var table = new DataTable();
            table.Columns.Add("ProjectId");
            table.Columns.Add("School");

            table.Rows.Add("123", "School 1");

            var bulkEditFileReader = new BulkEditFileReader();

            var result = bulkEditFileReader.Read(table);

            result.Headers.Should().HaveCount(2);
            result.Headers[0].Name.Should().Be("ProjectId");
            result.Headers[0].Index.Should().Be(0);
            result.Headers[1].Name.Should().Be("School");
            result.Headers[1].Index.Should().Be(1);

            result.Rows.Should().HaveCount(1);
            result.Rows[0].FileRowIndex.Should().Be(1);
            result.Rows[0].Columns[0].ColumnIndex.Should().Be(0);
            result.Rows[0].Columns[0].Value.Should().Be("123");
            result.Rows[0].Columns[1].ColumnIndex.Should().Be(1);
            result.Rows[0].Columns[1].Value.Should().Be("School 1");
        }

    }
}
