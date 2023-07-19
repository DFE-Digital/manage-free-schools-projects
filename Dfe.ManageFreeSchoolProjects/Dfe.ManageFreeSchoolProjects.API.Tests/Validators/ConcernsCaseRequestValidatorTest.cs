using Dfe.ManageFreeSchoolProjects.API.RequestModels;
using Dfe.ManageFreeSchoolProjects.API.Validators;
using FizzWare.NBuilder;
using FluentValidation.TestHelper;
using Xunit;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Validators
{
    public class ConcernsCaseRequestValidatorTest
    {
        [Fact]
        public void ShouldHaveError_WhenRatingIdIs0()
        {
            var validator = new ConcernsCaseRequestValidator();
            
            var request = Builder<ConcernCaseRequest>.CreateNew()
                .With(c => c.RatingId = 0)
                .Build();
            
            var result = validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(c => c.RatingId)
                .WithErrorCode("GreaterThanOrEqualValidator");
        }
    }
}