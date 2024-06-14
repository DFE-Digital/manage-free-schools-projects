using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Dfe.ManageFreeSchoolProjects.Constants;
using Microsoft.IdentityModel.Tokens;

namespace Dfe.ManageFreeSchoolProjects.Validators;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]

public class ValidYearAttribute : ValidationAttribute
{
    public ValidYearAttribute()
    {
        
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult(string.Format(ValidationConstants.YearValidationMessage, validationContext.DisplayName.ToLower()));
        
        var valueAsString = (string) value;
        
        bool isClosedNumber = int.TryParse(valueAsString, out int intYear);

        if(valueAsString.IsNullOrEmpty() || !isClosedNumber)
            return new ValidationResult(string.Format(ValidationConstants.YearValidationMessage, validationContext.DisplayName.ToLower()));
        
        
        if(intYear is < 2000 or > 2050)
            return new ValidationResult(string.Format(ValidationConstants.YearValueValidationMessage, validationContext.DisplayName.ToLower()));
        
        return ValidationResult.Success; 
            
    }
}