using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project
{
    public class ProjectMapperTests
    {
        [Theory]
        [InlineData("FS - AP", SchoolType.AlternativePosition)]
        [InlineData("FS - Special", SchoolType.Special)]
        [InlineData("SS", SchoolType.StudioSchool)]
        [InlineData("UTC", SchoolType.UniversityTechnicalCollege)]
        [InlineData("InvalidType", null)]
        public void ToSchoolType_Returns_ExpectedString(string input, SchoolType? expectedResult)
        {
            var result = ProjectMapper.ToSchoolType(input);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(SchoolType.AlternativePosition, "FS - AP")]
        [InlineData(SchoolType.Special, "FS - Special")]
        [InlineData(SchoolType.StudioSchool, "SS")]
        [InlineData(SchoolType.UniversityTechnicalCollege, "UTC")]
        [InlineData(null, null)]
        public void ToSchoolType_ReturnsExpectedEnum(SchoolType? input, string expectedResult)
        {
            var result = ProjectMapper.ToSchoolType(input);

            Assert.Equal(expectedResult, result);
        }
    }
}
