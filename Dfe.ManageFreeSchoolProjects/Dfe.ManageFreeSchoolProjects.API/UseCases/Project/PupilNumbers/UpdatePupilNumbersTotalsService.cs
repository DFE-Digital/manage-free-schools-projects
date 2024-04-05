using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.Identity.Client;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public class UpdatePupilNumbersTotalsService
    {
        public static void Build(Po po, CapacityWhenFull parameters)
        {
            po.PupilNumbersAndCapacityNurseryUnder5s = parameters.Nursery.ToString();
            po.PupilNumbersAndCapacityYrY6Capacity = parameters.ReceptionToYear6.ToString();
            po.PupilNumbersAndCapacityY7Y11Capacity = parameters.Year7ToYear11.ToString();
            po.PupilNumbersAndCapacityYrY11Pre16Capacity = (parameters.ReceptionToYear6 + parameters.Year7ToYear11).ToString();
            po.PupilNumbersAndCapacityY12Y14Post16Capacity = parameters.Year12ToYear14.ToString();

            var total =
                parameters.Nursery +
                parameters.ReceptionToYear6 + 
                parameters.Year7ToYear11 +
                parameters.Year12ToYear14 +
                parameters.SpecialistEducationNeeds +
                parameters.AlternativeProvision;

            po.PupilNumbersAndCapacityTotalOfCapacityTotals = total.ToString();
        }
    }
}
