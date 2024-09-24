namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IValidationCommand
    {
        ValidationResult Execute(string value);
    }

    public record ValidationResult()
    {
        public bool IsValid { get; set; }
        public string errorMessage { get; set; }
    }
}