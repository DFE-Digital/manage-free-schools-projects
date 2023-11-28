using System.ComponentModel.DataAnnotations;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Validators;
using FluentAssertions;

namespace Dfe.ManageFreeSchoolProjects.Tests.Tasks;

public class FreeSchoolNameValidatorTests
{
    [Theory]
    [InlineData("Great New School, with special characters (it's really not great)")]
    [InlineData("Great New School without special characters")]
    public void SchoolNameWithLetters_WithOptionalSpecialCharacters_ValidationResultSuccess(string schoolName)
    {
        var validationContext = new ValidationContext(schoolName);

        var freeSchoolNameValidator = new SpecialCharacterValidatorAttribute();
        var validationResult = freeSchoolNameValidator.GetValidationResult(schoolName, validationContext);

        validationResult.Should().Be(ValidationResult.Success);
    }

    [Theory]
    [InlineData("School~>>>> Name with invalid character@s")]
    [InlineData("School'@s Name%%^££££")]
    public void SchoolName_WithInvalid_Characters_ReturnsErrorMessage(string schoolName)
    {
        const string expectedErrorMessage = "School name must not include special characters other than , ( ) '";
        
        var validationContext = new ValidationContext(schoolName);

        var freeSchoolNameValidator = new SpecialCharacterValidatorAttribute();
        var validationResult = freeSchoolNameValidator.GetValidationResult(schoolName, validationContext);

        validationResult.Should().NotBe(ValidationResult.Success);
        validationResult?.ErrorMessage.Should().NotBeNullOrEmpty();
        validationResult?.ErrorMessage.Should().Be(expectedErrorMessage);
    }
}