using Dfe.ManageFreeSchoolProjects.API.UseCases.LocalAuthority;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit.Validations
{
    public class LACodeValidationCommand(ILocalAuthorityCache localAuthorityCache) : IValidationCommand<BulkEditDto>
    {
        public ValidationResult Execute(BulkEditDto data, string value)
        {
            var isValid = localAuthorityCache.GetLocalAuthorities().Any(x => x.LACode == value);
            
            return new ValidationResult
            {
                IsValid = isValid,
                ErrorMessage = isValid ? null : "Local Authority code does not exist"
            };
        }
    }
}
