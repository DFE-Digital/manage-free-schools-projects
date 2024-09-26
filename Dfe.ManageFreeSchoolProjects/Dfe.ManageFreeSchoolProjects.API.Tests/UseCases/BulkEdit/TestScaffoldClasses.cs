using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit;
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
                new() { Name = ProjectId, Type = new TestProjectIdValidation(), GetFromDto = (x => x.ProjectId), },
                new() { Name = HeaderOneName, Type = new TestValidation(), GetFromDto = (x => x.TestData), SetToDto = (v,t) => { t.TestData = v; return t; } },
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
}
