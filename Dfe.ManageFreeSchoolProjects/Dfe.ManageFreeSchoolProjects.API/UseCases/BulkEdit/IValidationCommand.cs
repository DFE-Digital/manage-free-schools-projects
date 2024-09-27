namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IValidationCommand<TDto> where TDto : IBulkEditDto
    {
        ValidationResult Execute(TDto data, string value);
    }

    public record ValidationResult()
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}