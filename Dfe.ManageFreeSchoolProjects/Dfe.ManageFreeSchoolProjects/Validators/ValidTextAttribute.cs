using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Dfe.ManageFreeSchoolProjects.Constants;
namespace Dfe.ManageFreeSchoolProjects.Validators;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
public class ValidTextAttribute : ValidationAttribute
{
    private const string AllowSpecialCharactersPattern = @"^(?=.*[a-zA-Z])[a-zA-Z0-9'(),\s]*$";
    private readonly int _maxLength;
    private string _nameOfProperty;

    public ValidTextAttribute(int maxLength, string nameOfProperty = "")
    {
        _maxLength = maxLength;
        _nameOfProperty = nameOfProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;
        
        if (string.IsNullOrEmpty(_nameOfProperty))
            _nameOfProperty = validationContext.DisplayName;
        
        var valueAsString = (string) value;

        if(valueAsString.Length > _maxLength)
            return new ValidationResult(string.Format(ValidationConstants.TextValidationMessage, validationContext.DisplayName.ToLower(), _maxLength));

        var specialCharactersRegex = new Regex(AllowSpecialCharactersPattern, RegexOptions.None, TimeSpan.FromSeconds(30));
        var match = specialCharactersRegex.Match(valueAsString);
        
        return match.Success
            ? ValidationResult.Success
            : new ValidationResult($"{_nameOfProperty} must not include special characters other than , ( ) '");
    }
}