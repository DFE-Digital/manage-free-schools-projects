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
    private readonly int _maxValue;

    public ValidNumberAttribute(int maxValue)
    {
        _maxValue = maxValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        var valueAsString = (string)value;

        if (int.Parse(valueAsString) > _maxValue)
            return new ValidationResult(string.Format(ValidationConstants.TextValidationMessage, validationContext.DisplayName.ToLower(), _maxValue));

        var specialCharactersRegex = new Regex(AllowSpecialCharactersPattern, RegexOptions.None, TimeSpan.FromSeconds(30));
        var match = specialCharactersRegex.Match(valueAsString);

        return match.Success
            ? ValidationResult.Success
            : new ValidationResult($"Please enter a valid number");
    }
}