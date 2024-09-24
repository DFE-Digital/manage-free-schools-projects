using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
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
        public string TestDataTwo { get; set; }

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


        public string IdentifingHeader => ProjectId;

        public List<HeaderType<TestDto>> GetHeaders()
        {
            return new()
            {
                new() { Name = ProjectId, Type = new TestProjectIdValidation(), GetFromDto = (x => x.ProjectId) },
                new() { Name = HeaderOneName, Type = new TestValidation(), GetFromDto = (x => x.TestData) },
                new() { Name = HeaderTwoName, Type = new TestValidation(), GetFromDto = (x => x.TestDataTwo) },
            };
        }
    }

    internal class TestProjectIdValidation : IValidationCommand
    {
        internal const string ValidationMessage = "TriggeredValidation";
        internal const string ValidInput = "Valid";

        public ValidationResult Execute(string value)
        {
            return new ValidationResult()
            {
                IsValid = true
            };
        }
    }

    internal class TestValidation : IValidationCommand
    {
        internal const string ValidationMessage = "TriggeredValidation";
        internal const string ValidInput = "Valid";

        public ValidationResult Execute(string value)
        {
            return new ValidationResult()
            {
                IsValid = value == ValidInput,
                errorMessage = value == ValidInput ? null : ValidationMessage
            };
        }
    }   

    public class BulkEditProcessTest
    {
        private const string HeaderOneName = TestHeaderRegister.HeaderOneName;
        private const string HeaderTwoName = TestHeaderRegister.HeaderTwoName;
        private const string ProjectId = TestHeaderRegister.ProjectId;
        private const string ValidationMessage = TestValidation.ValidationMessage;
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

            response.InvalidRows.Count.Should().Be(1);
            AssertInvalidCell(response.InvalidRows, 5, 1, ValidationMessage);

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
                
            response.ValidRows.Count.Should().Be(1);

            AssertValidCell(response.ValidRows, 4, 1, Existing, ValidInput);

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
                        TestDataTwo = MoreExisting,
                    }
                },
                {
                    "2", new TestDto()
                    {
                        ProjectId = "2",
                        TestData = Existing,
                        TestDataTwo = MoreExisting,
                    }
                },
                {
                    "3", new TestDto()
                    {
                        ProjectId = "3",
                        TestData = Existing,
                        TestDataTwo = MoreExisting,

                    }
                }
            };

            var response = await RunTest(file, data);
            response.ValidRows.Count.Should().Be(1);

            AssertValidCell(response.ValidRows, 1, 1, Existing, ValidInput);
            AssertValidCell(response.ValidRows, 1, 2, MoreExisting, ValidInput);

            response.InvalidRows.Count.Should().Be(2);
            AssertInvalidCell(response.InvalidRows, 2, 2, ValidationMessage);
            AssertInvalidCell(response.InvalidRows, 3, 1, ValidationMessage);
            AssertInvalidCell(response.InvalidRows, 3, 2, ValidationMessage);

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

            response.ValidRows.Count.Should().Be(1);
            AssertValidCell(response.ValidRows, 4, 0, Existing, ValidInput);

            response.InvalidRows.Count.Should().Be(1);
            AssertInvalidCell(response.InvalidRows, 6, 0, ValidationMessage);
        }

        private void AssertValidCell(List<ValidRowInfo> validrows, int rowIndex, int columnIndex, string currentValue, string newValue)
        {
            var row = validrows.FirstOrDefault(x => x.FileRowIndex == rowIndex);
            row.Should().NotBeNull();
            var column = row.Columns.FirstOrDefault(x => x.ColumnIndex == columnIndex);
            column.Should().NotBeNull();
            column.ColumnIndex.Should().Be(columnIndex);
            column.CurrentValue.Should().Be(currentValue);
            column.NewValue.Should().Be(newValue);
        }

        private void AssertInvalidCell(List<InvalidRowInfo> validrows, int rowIndex, int columnIndex, string error)
        {
            var row = validrows.FirstOrDefault(x => x.FileRowIndex == rowIndex);
            row.Should().NotBeNull();
            var column = row.Errors.FirstOrDefault(x => x.ColumnIndex == columnIndex);
            column.Should().NotBeNull();
            column.ColumnIndex.Should().Be(columnIndex);
            column.Error.Should().Be(error);
        }

        private async Task<BulkEditValidateResponse> RunTest(BulkEditValidateRequest file, Dictionary<string, TestDto> data)
        {
            //SetupTestContext();

            using (var context = new MfspContext(GetContextOptions(), null))
            {
                var process = new BulkEditProcess<TestDto>(new TestHeaderRegister(), new TestDataRetrieval(data), context);

                return await process.Execute(file);
            }
        }


        //private void SetupTestContext()
        //{
        //    using (var context = new MfspContext(GetContextOptions(), null))
        //    {
        //        context.Kpi.Add(new Kpi() { 
        //            Rid = "a", 
        //            ProjectStatusProjectStatus = "1",
        //            ProjectStatusCurrentFreeSchoolName = Existing,
        //            AprilIndicator = "",
        //            FsType = "",
        //            FsType1 = "",
        //            MatUnitProjects = "",
        //            SponsorUnitProjects = "",
        //            UpperStatus = "",
        //            Wave = ""
        //        });
        //        context.SaveChanges();
        //    }
        //}

        private static DbContextOptions<MfspContext> GetContextOptions()
        {
            return new DbContextOptionsBuilder<MfspContext>()
                .UseInMemoryDatabase(databaseName: "mfsp")
                .Options;
        }
    }
}
