using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IValidationCommand<TDto> where TDto : IBulkEditDto
    {
        ValidationResult Execute(ValidationCommandParameters<TDto> parameters);
    }

    public record ValidationCommandParameters<TDto>() where TDto : IBulkEditDto
    {
        public string Value { get; set; }
        public BulkEditRequest Request { get; set; }
        public int CurrentRowIndex { get; set; }
        public TDto Data { get; set; }
    }

    public record ValidationResult()
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}