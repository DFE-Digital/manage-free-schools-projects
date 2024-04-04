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
        public void MinimumViableRatioReceptionToYear6(int minimumViableNumber, int applicationsReceived, string expected)
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
                        ApplicationsReceived = applicationsReceived
                    }
                }
            };

            // Act
            var service = new UpdateRecruitmentAndViabilityService();

            service.Execute(po, request);

            // Assert
            po.PupilNumbersAndCapacityAcceptedApplicationsVsViabilityYrY6.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(MinimumViabilityRatioData))]
        public void MinimumViableRatioYear7ToYear11(int minimumViableNumber, int applicationsReceived, string expected)
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
                        ApplicationsReceived = applicationsReceived
                    }
                }
            };

            // Act
            var service = new UpdateRecruitmentAndViabilityService();

            service.Execute(po, request);

            // Assert
            po.PupilNumbersAndCapacityAcceptedApplicationsVsViabilityY7Y11.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(MinimumViabilityRatioData))]
        public void MinimumViableRatioYear12ToYear14(int minimumViableNumber, int applicationsReceived, string expected)
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
                        ApplicationsReceived = applicationsReceived
                    }
                }
            };

            // Act
            var service = new UpdateRecruitmentAndViabilityService();

            service.Execute(po, request);

            // Assert
            po.PupilNumbersAndCapacityAcceptedApplicationsVsViabilityY12Y14.Should().Be(expected);
        }

        [Theory]
        [InlineData(10, 5, "50.00")]
        [InlineData(15, 19, "126.67")]
        [InlineData(0, 5, "0.00")]
        [InlineData(5, 0, "0.00")]
        [InlineData(-1, 0, "0.00")]
        [InlineData(0, -1, "0.00")]
        public void PublishedAdmissionNumberRatioReceptionToYear6(
            int receptionToYear6PublishedAdmissionNumber,
            int applicationsReceived,
            string expected)
        {
            // Arrange
            var po = new Po()
            {
                PupilNumbersAndCapacityYrPan = receptionToYear6PublishedAdmissionNumber.ToString()
            };

            var request = new UpdatePupilNumbersRequest
            {
                RecruitmentAndViability = new()
                {
                    ReceptionToYear6 = new()
                    {
                        ApplicationsReceived = applicationsReceived
                    }
                }
            };

            // Act
            var service = new UpdateRecruitmentAndViabilityService();

            service.Execute(po, request);

            // Assert
            po.PupilNumbersAndCapacityAcceptedApplicationsVsPanYrY6.Should().Be(expected);
        }

        [Theory]
        [InlineData(10, 20, 50, 160, "200.00")]
        [InlineData(33, 66, 22, 17, "14.05")]
        public void PublishedAdmissionNumberRatioYear7ToYear11(
            int year7PublishedAdmissionNumber,
            int year10PublishedAdmissionNumber,
            int otherPre16PublishedAdmissionNumber,
            int applicationsReceived,
            string expected)
        {
            // Arrange
            var po = new Po()
            {
                PupilNumbersAndCapacityY7Pan = year7PublishedAdmissionNumber.ToString(),
                PupilNumbersAndCapacityY10Pan = year10PublishedAdmissionNumber.ToString(),
                PupilNumbersAndCapacityYOtherPanPre16 = otherPre16PublishedAdmissionNumber.ToString()
            };

            var request = new UpdatePupilNumbersRequest
            {
                RecruitmentAndViability = new()
                {
                    Year7ToYear11 = new()
                    {
                        ApplicationsReceived = applicationsReceived
                    }
                }
            };

            // Act
            var service = new UpdateRecruitmentAndViabilityService();

            service.Execute(po, request);

            // Assert
            po.PupilNumbersAndCapacityAcceptedApplicationsVsPanY7Y11.Should().Be(expected);
        }

        public static IEnumerable<object[]> MinimumViabilityRatioData()
        {
            yield return new object[] { 5, 10, "200.00" };
            yield return new object[] { 15, 19, "126.67" };
            yield return new object[] { 0, 5, "0.00" };
            yield return new object[] { 5, 0, "0.00" };
            yield return new object[] { -1, 5, "0.00" };
            yield return new object[] { 5, -1, "0.00" };
        }
    }
}
