using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public interface IUpdatePublishedAdmissionNumberPercentageService
    {
        public void Execute(Po po);
    }

    public class UpdatePublishedAdmissionNumberPercentageService : IUpdatePublishedAdmissionNumberPercentageService
    {
        public void Execute(Po po)
        {
            po.PupilNumbersAndCapacityReceivedApplicationsVsPanYrY6 = CalculatePublishedAdmissionNumberRatio(
                po.PupilNumbersAndCapacityYrPan.ToDecimal(),
                po.PupilNumbersAndCapacityNoApplicationsReceivedYrY6.ToDecimal());

            var year7ToYear11PublishedAdmissionNumber = po.PupilNumbersAndCapacityY7Pan.ToDecimal() +
                                                    po.PupilNumbersAndCapacityY10Pan.ToDecimal() +
                                                    po.PupilNumbersAndCapacityYOtherPanPre16.ToDecimal();

            po.PupilNumbersAndCapacityReceivedApplicationsVsPanY7Y11 = CalculatePublishedAdmissionNumberRatio(
                year7ToYear11PublishedAdmissionNumber,
                po.PupilNumbersAndCapacityNoApplicationsReceivedY7Y11.ToDecimal());

            var year12ToYear14PublishedAdmissionNumber = po.PupilNumbersAndCapacityY12Pan.ToDecimal() +
                                                    po.PupilNumbersAndCapacityYOtherPanPost16.ToDecimal();

            po.PupilNumbersAndCapacityReceivedApplicationsVsPanY12Y14 = CalculatePublishedAdmissionNumberRatio(
                year12ToYear14PublishedAdmissionNumber,
                po.PupilNumbersAndCapacityNoApplicationsReceivedY12Y14.ToDecimal());

            po.PupilNumbersAndCapacityAcceptedApplicationsVsPanYrY6 = CalculatePublishedAdmissionNumberRatio(
                po.PupilNumbersAndCapacityYrPan.ToDecimal(),
                po.PupilNumbersAndCapacityNoApplicationsAcceptedYrY6.ToDecimal());

            po.PupilNumbersAndCapacityAcceptedApplicationsVsPanY7Y11 = CalculatePublishedAdmissionNumberRatio(
                year7ToYear11PublishedAdmissionNumber,
                po.PupilNumbersAndCapacityNoApplicationsAcceptedY7Y11.ToDecimal());

            po.PupilNumbersAndCapacityAcceptedApplicationsVsPanY12Y14 = CalculatePublishedAdmissionNumberRatio(
                year12ToYear14PublishedAdmissionNumber,
                po.PupilNumbersAndCapacityNoApplicationsAcceptedY12Y14.ToDecimal());
        }

        private static string CalculatePublishedAdmissionNumberRatio(
            decimal publishedAdmissionNumber,
            decimal applicationsReceived)
        {
            if (publishedAdmissionNumber <= 0 || applicationsReceived <= 0)
            {
                return "0.00";
            }

            var result = (applicationsReceived / publishedAdmissionNumber) * 100;

            return result.ToString("0.00");
        }
    }
}
