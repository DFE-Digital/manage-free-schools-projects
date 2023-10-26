using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.Models;

public class StringLengthValidator : ValidationAttribute
{
    private readonly int _maxLength;

    public StringLengthValidator(int maxLength)
    {
        _maxLength = maxLength;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        var valueAsString = (string)value;
        
        return valueAsString.Trim().Length > _maxLength ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
    }
}