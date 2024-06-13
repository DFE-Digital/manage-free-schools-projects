using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public interface IUpdateRecruitmentAndViabilityService
    {
        public void Execute(Po po, UpdatePupilNumbersRequest request);
    }

    public class UpdateRecruitmentAndViabilityService : IUpdateRecruitmentAndViabilityService
    {
        public void Execute(Po po, UpdatePupilNumbersRequest request)
        {
            if (request.RecruitmentAndViability == null)
            {
                return;
            }

            po.PupilNumbersAndCapacityMinimumFirstYearRecruitmentForViabilityYrY6 = request.RecruitmentAndViability.ReceptionToYear6.MinimumViableNumber.ToString();
            po.PupilNumbersAndCapacityNoApplicationsReceivedYrY6 = request.RecruitmentAndViability.ReceptionToYear6.ApplicationsReceived.ToString();
            po.PupilNumbersAndCapacityNoApplicationsAcceptedYrY6 = request.RecruitmentAndViability.ReceptionToYear6.AcceptedOffers.ToString();

            po.PupilNumbersAndCapacityMinimumFirstYearRecruitmentForViabilityY7Y11 = request.RecruitmentAndViability.Year7ToYear11.MinimumViableNumber.ToString();
            po.PupilNumbersAndCapacityNoApplicationsReceivedY7Y11 = request.RecruitmentAndViability.Year7ToYear11.ApplicationsReceived.ToString();
            po.PupilNumbersAndCapacityNoApplicationsAcceptedY7Y11 = request.RecruitmentAndViability.Year7ToYear11.AcceptedOffers.ToString();

            po.PupilNumbersAndCapacityMinimumFirstYearRecruitmentForViabilityY12Y14 = request.RecruitmentAndViability.Year12ToYear14.MinimumViableNumber.ToString();
            po.PupilNumbersAndCapacityNoApplicationsReceivedY12Y14 = request.RecruitmentAndViability.Year12ToYear14.ApplicationsReceived.ToString();
            po.PupilNumbersAndCapacityNoApplicationsAcceptedY12Y14 = request.RecruitmentAndViability.Year12ToYear14.AcceptedOffers.ToString();

            var totalMinimumViableNumber = request.RecruitmentAndViability.ReceptionToYear6.MinimumViableNumber +
                                           request.RecruitmentAndViability.Year7ToYear11.MinimumViableNumber +
                                           request.RecruitmentAndViability.Year12ToYear14.MinimumViableNumber;

            var totalApplicationsReceived = request.RecruitmentAndViability.ReceptionToYear6.ApplicationsReceived +
                                            request.RecruitmentAndViability.Year7ToYear11.ApplicationsReceived +
                                            request.RecruitmentAndViability.Year12ToYear14.ApplicationsReceived;
            var totalAcceptedOffers = request.RecruitmentAndViability.ReceptionToYear6.AcceptedOffers +
                                      request.RecruitmentAndViability.Year7ToYear11.AcceptedOffers +
                                      request.RecruitmentAndViability.Year12ToYear14.AcceptedOffers;

            po.PupilNumbersAndCapacityMinimumFirstYearRecruitmentForViabilityTotal = totalMinimumViableNumber.ToString();
            po.PupilNumbersAndCapacityNoApplicationsReceivedTotal = totalApplicationsReceived.ToString();
            po.PupilNumbersAndCapacityNoApplicationsAcceptedTotal = totalAcceptedOffers.ToString();

            UpdateMinimumViableRatio(po);
            UpdateAcceptedVsViabilityRatio(po);
        }

        private static void UpdateMinimumViableRatio(Po po)
        {
            po.PupilNumbersAndCapacityReceivedApplicationsVsViabilityYrY6 = CalculateViableRatio(
                po.PupilNumbersAndCapacityMinimumFirstYearRecruitmentForViabilityYrY6.ToDecimal(),
                po.PupilNumbersAndCapacityNoApplicationsReceivedYrY6.ToDecimal());

            po.PupilNumbersAndCapacityReceivedApplicationsVsViabilityY7Y11 = CalculateViableRatio(
                po.PupilNumbersAndCapacityMinimumFirstYearRecruitmentForViabilityY7Y11.ToDecimal(),
                po.PupilNumbersAndCapacityNoApplicationsReceivedY7Y11.ToDecimal());

            po.PupilNumbersAndCapacityReceivedApplicationsVsViabilityY12Y14 = CalculateViableRatio(
                po.PupilNumbersAndCapacityMinimumFirstYearRecruitmentForViabilityY12Y14.ToDecimal(),
                po.PupilNumbersAndCapacityNoApplicationsReceivedY12Y14.ToDecimal());
        }

        private static void UpdateAcceptedVsViabilityRatio(Po po)
        {
            po.PupilNumbersAndCapacityAcceptedApplicationsVsViabilityYrY6 = CalculateViableRatio(
                po.PupilNumbersAndCapacityMinimumFirstYearRecruitmentForViabilityYrY6.ToDecimal(),
                po.PupilNumbersAndCapacityNoApplicationsAcceptedYrY6.ToDecimal());

            po.PupilNumbersAndCapacityAcceptedApplicationsVsViabilityY7Y11 = CalculateViableRatio(
                po.PupilNumbersAndCapacityMinimumFirstYearRecruitmentForViabilityY7Y11.ToDecimal(),
                po.PupilNumbersAndCapacityNoApplicationsAcceptedY7Y11.ToDecimal());

            po.PupilNumbersAndCapacityAcceptedApplicationsVsViabilityY12Y14 = CalculateViableRatio(
                po.PupilNumbersAndCapacityMinimumFirstYearRecruitmentForViabilityY12Y14.ToDecimal(),
                po.PupilNumbersAndCapacityNoApplicationsAcceptedY12Y14.ToDecimal());
        }

        private static string CalculateViableRatio(decimal minimumViableNumber, decimal factor)
        {
            if (minimumViableNumber <= 0 || factor <= 0)
            {
                return "0.00";
            }

            var result = (factor / minimumViableNumber) * 100;

            return result.ToString("0.00");
        }
    }
}
