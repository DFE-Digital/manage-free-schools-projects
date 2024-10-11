using Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases.BulkEdit
{
    internal record TestDto : IBulkEditDto
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

    internal class TestInteraction(Func<TestDto, string> getDto, Action<string, TestDto> setDto) : IHeaderDataInteraction<TestDto>
    {
        public string GetFromDto(TestDto dto)
        {
            return getDto(dto);
        }

        public TestDto ApplyToDto(string value, TestDto dto)
        {
            setDto(value, dto);
            return dto;
        }

        public string FormatValue(string value)
        {
            return value;
        }
    }

    internal class TestFormattedInteraction(Func<TestDto, string> getDto, Action<string, TestDto> setDto) : IHeaderDataInteraction<TestDto>
    {
        public const string Format = "format";

        public string GetFromDto(TestDto dto)
        {
            return getDto(dto);
        }

        public TestDto ApplyToDto(string value, TestDto dto)
        {
            setDto(value, dto);
            return dto;
        }

        public string FormatValue(string value)
        {
            return value + Format;
        }
    }

    internal class TestHeaderRegister : IHeaderRegister<TestDto>
    {
        internal const string HeaderOneName = "TestHeader";
        internal const string HeaderTwoName = "TestHeaderTwo";
        internal const string ProjectId = "ProjectId";
        internal const string HeaderData = "DependantTestData";
        internal const string FormattedName = "Formatted";

        public string IdentifingHeader => ProjectId;

        public List<HeaderType<TestDto>> GetHeaders()
        {
            return new()
            {
                new() { Name = ProjectId, Validation = new TestProjectIdValidation(), DataInteraction = new TestInteraction(x => x.ProjectId, (x, t) => t.ProjectId = x) },
                new() { Name = HeaderOneName, Validation = new TestValidation(), DataInteraction = new TestInteraction(x => x.TestData, (x, t) => t.TestData = x) },
                new() { Name = HeaderTwoName, Validation = new TestValidation(), DataInteraction = new TestInteraction(x => x.OtherTestData, (x, t) => t.OtherTestData = x) },
                new() { Name = HeaderData, Validation = new DataDependencyValidation(), DataInteraction = new TestInteraction(x => x.DependantTestData, (x, t) => t.DependantTestData = x) },
                new() { Name = FormattedName, Validation = new TestValidation(), DataInteraction = new TestFormattedInteraction(x => x.TestData, (x, t) => t.TestData = x) },
            };
        }
    }

    internal class TestProjectIdValidation : IValidationCommand<TestDto>
    {
        internal const string ValidationMessage = "TriggeredValidation";
        internal const string ValidInput = "Valid";

        public ValidationResult Execute(ValidationCommandParameters<TestDto> parameters)
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

        public ValidationResult Execute(ValidationCommandParameters<TestDto> parameters)
        {
            return new ValidationResult()
            {
                IsValid = parameters.Value == ValidInput,
                ErrorMessage = parameters.Value == ValidInput ? null : ValidationMessage
            };
        }
    }


    internal class DataDependencyValidation : IValidationCommand<TestDto>
    {
        internal const string DataValidationMessage = "Triggered data validation";

        public ValidationResult Execute(ValidationCommandParameters<TestDto> parameters)
        {
            return new ValidationResult()
            {
                IsValid = parameters.Data.DataForValidation == parameters.Value,
                ErrorMessage = parameters.Data.DataForValidation == parameters.Value ? null : DataValidationMessage
            };
        }
    }
}
