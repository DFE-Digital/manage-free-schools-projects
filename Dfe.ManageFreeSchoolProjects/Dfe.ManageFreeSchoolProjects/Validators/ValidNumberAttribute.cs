using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Dfe.ManageFreeSchoolProjects.Constants;

namespace Dfe.ManageFreeSchoolProjects.Models;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
public class ValidNumberAttribute : ValidationAttribute
{
    private const string AllowSpecialCharactersPattern = "^[0-9]*$";
    private readonly int _maxLength;

    public ValidNumberAttribute(int maxLength)
    {
        _maxLength = maxLength;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        var valueAsString = (string)value;

        if (valueAsString.Length > _maxLength)
            return new ValidationResult(string.Format(ValidationConstants.TextValidationMessage, validationContext.DisplayName.ToLower(), _maxLength));

        var specialCharactersRegex = new Regex(AllowSpecialCharactersPattern, RegexOptions.None, TimeSpan.FromSeconds(30));
        var match = specialCharactersRegex.Match(valueAsString);

        return match.Success
            ? ValidationResult.Success
            : new ValidationResult($"Please enter a valid number");
    }
}