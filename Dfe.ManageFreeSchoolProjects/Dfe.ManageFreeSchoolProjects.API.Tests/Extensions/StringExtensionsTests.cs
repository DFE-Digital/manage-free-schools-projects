using Dfe.ManageFreeSchoolProjects.API.Extensions;
using System;
using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Extensions
{
    public class StringExtensionsTests
    {
        public enum TestEnum
        {
            [Description("Default")]
            Default,
            [Description("Test Description")]
            TestValue,
            [Description("Other Description")]
            OtherValue,
            NoDescription
        }

        [Theory]
        [InlineData("Default", TestEnum.Default)]
        [InlineData("Test Description", TestEnum.TestValue)]
        [InlineData("Other Description", TestEnum.OtherValue)]
        [InlineData("NoDescription", TestEnum.NoDescription)]
        public void FromDescriptionWithEnumBecomesCorrectValue(string testString, TestEnum expected)
        {
            testString.ToEnumFromDescription<TestEnum>().Should().Be(expected);
        }

        [Fact]
        public void FromDescriptionWithInvalidDescriptionThrowsArgumentException()
        {
            Action act = () => "Invalid Description".ToEnumFromDescription<TestEnum>();

            act.Should().Throw<ArgumentException>();
        }
    }
}
