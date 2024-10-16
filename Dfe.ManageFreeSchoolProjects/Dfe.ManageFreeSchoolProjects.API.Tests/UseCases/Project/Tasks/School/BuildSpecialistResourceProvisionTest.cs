using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.School;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases.Project.Tasks.School
{
    public class BuildSpecialistResourceProvisionTest
    {
        [Theory]
        [InlineData(ClassType.SpecialEducationNeeds.NotSet, ClassType.AlternativeProvision.NotSet, null)]
        [InlineData(ClassType.SpecialEducationNeeds.NotSet, ClassType.AlternativeProvision.No, "No")]
        [InlineData(ClassType.SpecialEducationNeeds.NotSet, ClassType.AlternativeProvision.Yes, "Yes")]
        [InlineData(ClassType.SpecialEducationNeeds.Yes, ClassType.AlternativeProvision.NotSet, "Yes")]
        [InlineData(ClassType.SpecialEducationNeeds.Yes, ClassType.AlternativeProvision.No, "Yes")]
        [InlineData(ClassType.SpecialEducationNeeds.Yes, ClassType.AlternativeProvision.Yes, "Yes")]
        [InlineData(ClassType.SpecialEducationNeeds.No, ClassType.AlternativeProvision.NotSet, "No")]
        [InlineData(ClassType.SpecialEducationNeeds.No, ClassType.AlternativeProvision.No, "No")]
        [InlineData(ClassType.SpecialEducationNeeds.No, ClassType.AlternativeProvision.Yes, "Yes")]
        public void TestBasedOnState(ClassType.SpecialEducationNeeds specialEducationNeeds,
                                     ClassType.AlternativeProvision alternativeProvision,
                                     string expected)
        {
            var result = BuildSpecialistResourceProvision
                                .GetLegacySpecialistResourceProvision(specialEducationNeeds, alternativeProvision);

            result.Should().Be(expected);
        }
    }
}
