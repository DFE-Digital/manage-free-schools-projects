using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Project.PupilNumbers
{
    public class UpdatePublishedAdmissionNumberPercentageServiceTests
    {
        [Theory]
        [InlineData(10, 5, "50.00", 6, "60.00")]
        [InlineData(15, 19, "126.67", 20, "133.33")]
        [InlineData(0, 5, "0.00", 5, "0.00")]
        [InlineData(5, 0, "0.00", 0, "0.00")]
        [InlineData(-1, 0, "0.00", 0, "0.00")]
        [InlineData(0, -1, "0.00", -1, "0.00")]
        public void PublishedAdmissionNumberRatioReceptionToYear6(
            int receptionToYear6PublishedAdmissionNumber,
            int applicationsReceived,
            string expected,
            int acceptedOffers,
            string acceptedExpected)
        {
            // Arrange
            var po = new Po()
            {
                PupilNumbersAndCapacityYrPan = receptionToYear6PublishedAdmissionNumber.ToString(),
                PupilNumbersAndCapacityNoApplicationsReceivedYrY6 = applicationsReceived.ToString(),
                PupilNumbersAndCapacityNoApplicationsAcceptedYrY6 = acceptedOffers.ToString()
            };

            // Act
            var service = new UpdatePublishedAdmissionNumberPercentageService();

            service.Execute(po);

            // Assert
            po.PupilNumbersAndCapacityReceivedApplicationsVsPanYrY6.Should().Be(expected);
            po.PupilNumbersAndCapacityAcceptedApplicationsVsPanYrY6.Should().Be(acceptedExpected);
        }

        [Theory]
        [InlineData(10, 20, 50, 160, "200.00", 160, "200.00")]
        [InlineData(33, 66, 22, 17, "14.05", 17, "14.05")]
        public void PublishedAdmissionNumberRatioYear7ToYear11(
            int year7PublishedAdmissionNumber,
            int year10PublishedAdmissionNumber,
            int otherPre16PublishedAdmissionNumber,
            int applicationsReceived,
            string expected,
            int acceptedOffers,
            string acceptedExpected)
        {
            // Arrange
            var po = new Po()
            {
                PupilNumbersAndCapacityY7Pan = year7PublishedAdmissionNumber.ToString(),
                PupilNumbersAndCapacityY10Pan = year10PublishedAdmissionNumber.ToString(),
                PupilNumbersAndCapacityYOtherPanPre16 = otherPre16PublishedAdmissionNumber.ToString(),
                PupilNumbersAndCapacityNoApplicationsReceivedY7Y11 = applicationsReceived.ToString(),
                PupilNumbersAndCapacityNoApplicationsAcceptedY7Y11 = acceptedOffers.ToString()
            };

            // Act
            var service = new UpdatePublishedAdmissionNumberPercentageService();

            service.Execute(po);

            // Assert
            po.PupilNumbersAndCapacityReceivedApplicationsVsPanY7Y11.Should().Be(expected);
            po.PupilNumbersAndCapacityAcceptedApplicationsVsPanY7Y11.Should().Be(acceptedExpected);
        }

        [Theory]
        [InlineData(10, 70, 160, "200.00", 160, "200.00")]
        [InlineData(33, 88, 17, "14.05", 17, "14.05")]
        public void PublishedAdmissionNumberRatioYear12ToYear14(
            int year12PublishedAdmissionNumber,
            int otherPost16PublishedAdmissionNumber,
            int applicationsReceived,
            string expected,
            int acceptedOffers,
            string acceptedExpected)
        {
            // Arrange
            var po = new Po()
            {
                PupilNumbersAndCapacityY12Pan = year12PublishedAdmissionNumber.ToString(),
                PupilNumbersAndCapacityYOtherPanPost16 = otherPost16PublishedAdmissionNumber.ToString(),
                PupilNumbersAndCapacityNoApplicationsReceivedY12Y14 = applicationsReceived.ToString(),
                PupilNumbersAndCapacityNoApplicationsAcceptedY12Y14 = acceptedOffers.ToString(),
            };

            // Act
            var service = new UpdatePublishedAdmissionNumberPercentageService();

            service.Execute(po);

            // Assert
            po.PupilNumbersAndCapacityReceivedApplicationsVsPanY12Y14.Should().Be(expected);
            po.PupilNumbersAndCapacityAcceptedApplicationsVsPanY12Y14.Should().Be(acceptedExpected);
        }
    }
}
