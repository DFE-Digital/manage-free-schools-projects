using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using System.Collections.Generic;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project.PupilNumbers
{
    public class UpdateRecruitmentAndViabilityServiceTests
    {
        [Theory]
        [MemberData(nameof(MinimumViabilityRatioData))]
        public void MinimumViableRatioReceptionToYear6(int minimumViableNumber, int applicationsReceived, string expected, int acceptedOffers, string acceptedExpected)
        {
            // Arrange
            var po = new Po();
            var request = new UpdatePupilNumbersRequest
            {
                RecruitmentAndViability = new()
                {
                    ReceptionToYear6 = new()
                    {
                        MinimumViableNumber = minimumViableNumber,
                        ApplicationsReceived = applicationsReceived,
                        AcceptedOffers = acceptedOffers
                    }
                }
            };

            // Act
            var service = new UpdateRecruitmentAndViabilityService();

            service.Execute(po, request);

            // Assert
            po.PupilNumbersAndCapacityReceivedApplicationsVsViabilityYrY6.Should().Be(expected);
            po.PupilNumbersAndCapacityAcceptedApplicationsVsViabilityYrY6.Should().Be(acceptedExpected);
        }

        [Theory]
        [MemberData(nameof(MinimumViabilityRatioData))]
        public void MinimumViableRatioYear7ToYear11(int minimumViableNumber, int applicationsReceived, string expected, int acceptedOffers, string acceptedExpected)
        {
            // Arrange
            var po = new Po();
            var request = new UpdatePupilNumbersRequest
            {
                RecruitmentAndViability = new()
                {
                    Year7ToYear11 = new()
                    {
                        MinimumViableNumber = minimumViableNumber,
                        ApplicationsReceived = applicationsReceived,
                        AcceptedOffers = acceptedOffers,
                    }
                }
            };

            // Act
            var service = new UpdateRecruitmentAndViabilityService();

            service.Execute(po, request);

            // Assert
            po.PupilNumbersAndCapacityReceivedApplicationsVsViabilityY7Y11.Should().Be(expected);
            po.PupilNumbersAndCapacityAcceptedApplicationsVsViabilityY7Y11.Should().Be(acceptedExpected);
        }

        [Theory]
        [MemberData(nameof(MinimumViabilityRatioData))]
        public void MinimumViableRatioYear12ToYear14(int minimumViableNumber, int applicationsReceived, string expected, int acceptedOffers, string acceptedExpected)
        {
            // Arrange
            var po = new Po();
            var request = new UpdatePupilNumbersRequest
            {
                RecruitmentAndViability = new()
                {
                    Year12ToYear14 = new()
                    {
                        MinimumViableNumber = minimumViableNumber,
                        ApplicationsReceived = applicationsReceived,
                        AcceptedOffers = acceptedOffers,
                    }
                }
            };

            // Act
            var service = new UpdateRecruitmentAndViabilityService();

            service.Execute(po, request);

            // Assert
            po.PupilNumbersAndCapacityReceivedApplicationsVsViabilityY12Y14.Should().Be(expected);
            po.PupilNumbersAndCapacityAcceptedApplicationsVsViabilityY12Y14.Should().Be(acceptedExpected);
        }

        public static IEnumerable<object[]> MinimumViabilityRatioData()
        {
            yield return new object[] { 5, 10, "200.00", 11, "220.00" };
            yield return new object[] { 15, 19, "126.67", 17, "113.33" };
            yield return new object[] { 0, 5, "0.00", 5, "0.00" };
            yield return new object[] { 5, 0, "0.00", 0, "0.00" };
            yield return new object[] { -1, 5, "0.00", 5, "0.00" };
            yield return new object[] { 5, -1, "0.00", -1, "0.00" };
        }
    }
}
