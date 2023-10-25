using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dfe.ManageFreeSchoolProjects.Models;

public class FreeSchoolNameValidator : ValidationAttribute
{
    private const string AToZSpecialCharactersPattern = @"^[a-zA-Z0-9'(),\s]*$";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        var regex = new Regex(AToZSpecialCharactersPattern);
        var match = regex.Match((string)value);

        return match.Success
            ? ValidationResult.Success
            : new ValidationResult("Please use valid characters. Valid character are: A-Z, apostrophes, parentheses and commas.");
    }
}