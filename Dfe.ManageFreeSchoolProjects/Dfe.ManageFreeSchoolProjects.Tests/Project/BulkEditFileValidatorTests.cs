using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.Services.BulkEdit;
using FluentAssertions;
using System.Data;

namespace Dfe.ManageFreeSchoolProjects.Tests.Project
{
    public class BulkEditFileValidatorTests
    {
        [Fact]
        public void ExecuteValidation()
        {
            
            var table = new DataTable();
            table.Columns.Add(HeaderNames.ProjectId);
            table.Columns.Add(HeaderNames.SchoolName);
            table.Rows.Add("123", "School 1");

            var validator = new BulkEditFileValidator();
            var result = validator.Validate(table);

            result.IsValid.Should().BeTrue();
            result.ErrorMessage.Should().BeNull();
        }

        [Fact]
        public void FailsWhenGivenEmptyColumn()
        {

            var table = new DataTable();
            table.Columns.Add(HeaderNames.ProjectId);
            table.Columns.Add("Column 1");
            table.Columns.Add(HeaderNames.SchoolName);
            table.Rows.Add("123", "", "School 1");

            var validator = new BulkEditFileValidator();
            var result = validator.Validate(table);

            result.IsValid.Should().BeFalse();
            result.ErrorMessage.Should().Be("File has an empty column header");
        }

        [Fact]
        public void FailsWhenGivenUnsupportedColumn()
        {

            var table = new DataTable();
            table.Columns.Add(HeaderNames.ProjectId);
            table.Columns.Add("LocalPlace");
            table.Columns.Add(HeaderNames.SchoolName);
            table.Rows.Add("123", "London", "School 1");

            var validator = new BulkEditFileValidator();
            var result = validator.Validate(table);

            result.IsValid.Should().BeFalse();
            result.ErrorMessage.Should().Be("File has a invalid column header: LocalPlace");
        }
    }
}
