using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases.BulkEdit
{
    internal record TestDto: IBulkEditDto
    {
        public string ProjectId { get; set; }
        public string TestData { get; set; }
        public string OtherTestData { get; set; }
        public string DependantTestData { get; set; }
        public string DataForValidation { get; set; }

        public string Identifier => ProjectId;
    }

    internal class TestDataRetrieval(Dictionary<string, TestDto> data) : IBulkEditDataRetrieval<TestDto>
    {
        public Task<Dictionary<string, TestDto>> Retrieve(List<string> projectIds)
        {
            return Task.FromResult(data.Where(x => projectIds.Contains(x.Key)).ToDictionary());
        }
    }

    internal class TestHeaderRegister : IHeaderRegister<TestDto>
    {
        internal const string HeaderOneName = "TestHeader";
        internal const string HeaderTwoName = "TestHeaderTwo";
        internal const string ProjectId = "ProjectId";
        internal const string HeaderData = "DependantTestData";

        public string IdentifingHeader => ProjectId;

        public List<HeaderType<TestDto>> GetHeaders()
        {
            return new()
            {
                new() { Name = ProjectId, Type = new TestProjectIdValidation(), GetFromDto = (x => x.ProjectId) },
                new() { Name = HeaderOneName, Type = new TestValidation(), GetFromDto = (x => x.TestData) },
                new() { Name = HeaderTwoName, Type = new TestValidation(), GetFromDto = (x => x.OtherTestData) },
                new() { Name = HeaderData, Type = new DataDependencyValidation(), GetFromDto = (x => x.DependantTestData)}
            };
        }
    }

    internal class TestProjectIdValidation : IValidationCommand<TestDto> 
    {
        internal const string ValidationMessage = "TriggeredValidation";
        internal const string ValidInput = "Valid";

        public ValidationResult Execute(TestDto data, string value)
        {
            return new ValidationResult()
            {
                IsValid = true
            };
        }
    }

    internal class TestValidation : IValidationCommand<TestDto>
    {
        internal const string ValidationMessage = "TriggeredValidation";
        internal const string ValidInput = "Valid";

        public ValidationResult Execute(TestDto data, string value)
        {
            return new ValidationResult()
            {
                IsValid = value == ValidInput,
                errorMessage = value == ValidInput ? null : ValidationMessage
            };
        }
    }


    internal class DataDependencyValidation : IValidationCommand<TestDto>
    {
        internal const string DataValidationMessage = "Triggered data validation";

        public ValidationResult Execute(TestDto data, string value)
        {
            return new ValidationResult()
            {
                IsValid = data.DataForValidation == value,
                errorMessage = data.DataForValidation == value ? null : DataValidationMessage
            };
        }
    }

    public class BulkEditValidationTest
    {
        private const string HeaderOneName = TestHeaderRegister.HeaderOneName;
        private const string HeaderTwoName = TestHeaderRegister.HeaderTwoName;
        private const string DataHeader = TestHeaderRegister.HeaderData;
        private const string ProjectId = TestHeaderRegister.ProjectId;
        private const string ValidationMessage = TestValidation.ValidationMessage;
        private const string DataValidationMessage = DataDependencyValidation.DataValidationMessage;
        private const string ValidInput = TestValidation.ValidInput;
        private const string InvalidInput = "Invalid";
        private const string Existing = "Existing";
        private const string MoreExisting = "MoreExisting";

        [Fact]
        public async Task ValidationFailsOnSingleItem()
        {
            var file = new BulkEditValidateRequest()
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
                        FileRowIndex = 5,
                        Columns = new()
                        {
                            new ColumnInfo() { ColumnIndex = 0, Value = "1" },
                            new ColumnInfo() { ColumnIndex = 1, Value = InvalidInput },
                        }
                    },
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

            var response = await RunTest(file, data);

            response.ValidationResultRows.Count.Should().Be(1);
            AssertCell(response.ValidationResultRows, 5, 1, Existing, InvalidInput, ValidationMessage);

        }

        [Fact]
        public async Task ValidationReturnValidOnSingleItem()
        {
            var file = new BulkEditValidateRequest()
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

            var response = await RunTest(file, data);
                
            response.ValidationResultRows.Count.Should().Be(1);

            AssertCell(response.ValidationResultRows, 4, 1, Existing, ValidInput);

        }

        [Fact]
        public async Task BasicValidationOfFile()
        {
            var file = new BulkEditValidateRequest()
            {
                Headers = new()
                {
                    new HeaderInfo() { Index = 0, Name = ProjectId },
                    new HeaderInfo() { Index = 1, Name = HeaderOneName },
                    new HeaderInfo() { Index = 2, Name = HeaderTwoName },

                },
                Rows = new()
                {
                    new RowInfo()
                    {
                        FileRowIndex = 1,
                        Columns = new()
                        {
                            new ColumnInfo() { ColumnIndex = 0, Value = "1" },
                            new ColumnInfo() { ColumnIndex = 1, Value = ValidInput },
                            new ColumnInfo() { ColumnIndex = 2, Value = ValidInput },
                        }
                    },
                    new RowInfo()
                    {
                        FileRowIndex = 2,
                        Columns = new()
                        {
                            new ColumnInfo() { ColumnIndex = 0, Value = "2" },
                            new ColumnInfo() { ColumnIndex = 1, Value = ValidInput },
                            new ColumnInfo() { ColumnIndex = 2, Value = InvalidInput },
                        }
                    },
                    new RowInfo()
                    {
                        FileRowIndex = 3,
                        Columns = new()
                        {
                            new ColumnInfo() { ColumnIndex = 0, Value = "3" },
                            new ColumnInfo() { ColumnIndex = 1, Value = InvalidInput },
                            new ColumnInfo() { ColumnIndex = 2, Value = InvalidInput },
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
                        TestData = Existing,
                        OtherTestData = MoreExisting,
                    }
                },
                {
                    "2", new TestDto()
                    {
                        ProjectId = "2",
                        TestData = Existing,
                        OtherTestData = MoreExisting,
                    }
                },
                {
                    "3", new TestDto()
                    {
                        ProjectId = "3",
                        TestData = Existing,
                        OtherTestData = MoreExisting,

                    }
                }
            };

            var response = await RunTest(file, data);
            response.ValidationResultRows.Count.Should().Be(3);

            AssertCell(response.ValidationResultRows, 1, 1, Existing, ValidInput);
            AssertCell(response.ValidationResultRows, 1, 2, MoreExisting, ValidInput);

            AssertCell(response.ValidationResultRows, 2, 1, Existing, ValidInput);
            AssertCell(response.ValidationResultRows, 2, 2, MoreExisting, InvalidInput, ValidationMessage);

            AssertCell(response.ValidationResultRows, 3, 1, Existing, InvalidInput, ValidationMessage);
            AssertCell(response.ValidationResultRows, 3, 2, MoreExisting, InvalidInput, ValidationMessage);
        }

        [Fact]
        public async Task ValidationWithOutOfOrderHeader()
        {
            var file = new BulkEditValidateRequest()
            {
                Headers = new()
                {

                    new HeaderInfo() { Index = 0, Name = HeaderOneName },
                    new HeaderInfo() { Index = 1, Name = ProjectId },
                },
                Rows = new()
                {
                    new RowInfo()
                    {
                        FileRowIndex = 4,
                        Columns = new()
                        {
                            new ColumnInfo() { ColumnIndex = 0, Value = ValidInput },
                            new ColumnInfo() { ColumnIndex = 1, Value = "5" },
                        }
                    },
                    new RowInfo()
                    {
                        FileRowIndex = 6,
                        Columns = new()
                        {
                            new ColumnInfo() { ColumnIndex = 0, Value = InvalidInput },
                            new ColumnInfo() { ColumnIndex = 1, Value = "7" },
                        }
                    }
                }
            };


            Dictionary<string, TestDto> data = new()
            {
                {
                    "5", new TestDto()
                    {
                        ProjectId = "5",
                        TestData = Existing
                    }
                },
                {
                    "7", new TestDto()
                    {
                        ProjectId = "7",
                        TestData = Existing
                    }
                }
            };

            var response = await RunTest(file, data);

            response.ValidationResultRows.Count.Should().Be(2);

            AssertCell(response.ValidationResultRows, 4, 0, Existing, ValidInput);
            AssertCell(response.ValidationResultRows, 6, 0, Existing, InvalidInput, ValidationMessage);

        }

        [Fact]
        public async Task ValidationOfFileUsingData()
        {
            var file = new BulkEditValidateRequest()
            {
                Headers = new()
                {
                    new HeaderInfo() { Index = 0, Name = ProjectId },
                    new HeaderInfo() { Index = 1, Name = DataHeader },
                },
                Rows = new()
                {
                    new RowInfo()
                    {
                        FileRowIndex = 1,
                        Columns = new()
                        {
                            new ColumnInfo() { ColumnIndex = 0, Value = "1" },
                            new ColumnInfo() { ColumnIndex = 1, Value = ValidInput },
                        }
                    },
                    new RowInfo()
                    {
                        FileRowIndex = 2,
                        Columns = new()
                        {
                            new ColumnInfo() { ColumnIndex = 0, Value = "2" },
                            new ColumnInfo() { ColumnIndex = 1, Value = ValidInput },
                        }
                    },
                }
            };

            Dictionary<string, TestDto> data = new()
            {
                {
                    "1", new TestDto()
                    {
                        ProjectId = "1",
                        DependantTestData = Existing,
                        DataForValidation = ValidInput,
                    }
                },
                {
                    "2", new TestDto()
                    {
                        ProjectId = "2",
                        DependantTestData = MoreExisting,
                        DataForValidation = InvalidInput,
                    }
                },
            };

            var response = await RunTest(file, data);
            response.ValidationResultRows.Count.Should().Be(2);
            AssertCell(response.ValidationResultRows, 1, 1, Existing, ValidInput);
            AssertCell(response.ValidationResultRows, 2, 1, MoreExisting, ValidInput, DataValidationMessage);
        }


        private void AssertCell(List<ValidationRowInfo> validrows, int rowIndex, int columnIndex, string currentValue, string newValue, string error = null)
        {
            var row = validrows.FirstOrDefault(x => x.FileRowIndex == rowIndex);
            row.Should().NotBeNull();
            var column = row.Columns.FirstOrDefault(x => x.ColumnIndex == columnIndex);
            column.Should().NotBeNull();
            column.ColumnIndex.Should().Be(columnIndex);
            column.CurrentValue.Should().Be(currentValue);
            column.NewValue.Should().Be(newValue);
            column.Error.Should().Be(error);
        }

        private async Task<BulkEditValidateResponse> RunTest(BulkEditValidateRequest file, Dictionary<string, TestDto> data)
        {
            using (var context = new MfspContext(GetContextOptions(), null))
            {
                var process = new BulkEditValidation<TestDto>(new TestHeaderRegister(), new TestDataRetrieval(data));

                return await process.Execute(file);
            }
        }

        private static DbContextOptions<MfspContext> GetContextOptions()
        {
            return new DbContextOptionsBuilder<MfspContext>()
                .UseInMemoryDatabase(databaseName: "mfsp")
                .Options;
        }
    }
}
