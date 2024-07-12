using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace Dfe.ManageFreeSchoolProjects.Validators;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
public class ValidUrnAttribute : ValidationAttribute
{
    private const string UrnPattern = @"^[0-9]{6}$";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        var valueAsString = (string)value;

        var urnRegex = new Regex(UrnPattern, RegexOptions.None, TimeSpan.FromSeconds(30));
        var match = urnRegex.Match(valueAsString);

        //Unlike the other validators, here a match is valid so the ternary is flipped
        return match.Success
            ? ValidationResult.Success
            : new ValidationResult("URN must be 6 numbers, like 123456");
    }
}