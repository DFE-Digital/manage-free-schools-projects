using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace Dfe.ManageFreeSchoolProjects.Validators;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
public class ValidPhoneNumberAttribute : ValidationAttribute
{
    private const string AllowSpecialCharactersPattern = @"[^0-9'()\-\s+]";
    private readonly int _minLength = 5;
    private readonly int _maxLength = 15;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null || (string)value == string.Empty)
            return ValidationResult.Success;

        var valueAsString = (string)value;

        var valueAsDigits = Regex.Replace(valueAsString, "[^0-9]", "");

        if (valueAsDigits.Length < _minLength || valueAsDigits.Length > _maxLength)
            return new ValidationResult(string.Format("Phone number must be between {0} numbers and {1} numbers", _minLength, _maxLength));

        var specialCharactersRegex = new Regex(AllowSpecialCharactersPattern, RegexOptions.None, TimeSpan.FromSeconds(30));
        var match = specialCharactersRegex.Match(valueAsString);

        return match.Success
            ? new ValidationResult("Phone number must only include numbers and ( ) - +")
            : ValidationResult.Success;
    }
}