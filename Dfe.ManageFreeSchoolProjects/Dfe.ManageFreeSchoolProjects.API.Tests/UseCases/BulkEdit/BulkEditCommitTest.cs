using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases.BulkEdit
{
    internal class TestDataCommit: IBulkDataCommit<TestDto>
    {
        public Task Save(IEnumerable<TestDto> data)
        {
            SavedDto = data.ToList();

            return Task.CompletedTask;
        }

        public List<TestDto> SavedDto {get; private set;}
    }

    public class BulkEditCommitTest
    {
        private const string HeaderOneName = TestHeaderRegister.HeaderOneName;
        private const string ProjectId = TestHeaderRegister.ProjectId;
        private const string ValidInput = TestValidation.ValidInput;
        private const string Existing = "Existing";
        
        [Fact]
        public async Task CommitSingleItem()
        {
            var file = new BulkEditRequest()
            {
                Headers = new()
                {
                    new HeaderInfo() { Index = 0, Name = ProjectId },
                    new HeaderInfo() { Index = 1, Name = HeaderOneName },
                },
                Rows = new()
                {
                    new RowInfo()
                    {
                        FileRowIndex = 4,
                        Columns = new()
                        {
                            new ColumnInfo() { ColumnIndex = 0, Value = "1" },
                            new ColumnInfo() { ColumnIndex = 1, Value = ValidInput },
                        }
                    }
                }
            };


            Dictionary<string, TestDto> data = new()
            {
                {
                    "1", new TestDto()
                    {
                        ProjectId = "1",
                        TestData = Existing
                    }
                }
            };

            var dto = await RunTest(file, data);

            dto.First().TestData.Should().Be(ValidInput);

        }
        private async Task<List<TestDto>> RunTest(BulkEditRequest file, Dictionary<string, TestDto> data)
        {
            var dataCommit = new TestDataCommit();
            var process = new BulkEditCommit<TestDto>(new TestHeaderRegister(), new TestDataRetrieval(data), dataCommit);

            await process.Execute(file);

            return Task.FromResult(dataCommit.SavedDto).Result;
        }
    }
}
