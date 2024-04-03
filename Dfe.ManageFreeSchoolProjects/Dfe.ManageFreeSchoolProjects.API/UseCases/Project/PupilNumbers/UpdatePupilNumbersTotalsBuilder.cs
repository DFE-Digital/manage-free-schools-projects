using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public record UpdatePupilNumbersTotalsBuilderParameters
    {
        public int Nursery { get; set; }
        public int ReceptionToYear6 { get; set; }
        public int Year7ToYear11 { get; set; }
        public int Year12ToYear14 { get; set; }
    }

    public class UpdatePupilNumbersTotalsBuilder
    {
        public static void Build(Po po, UpdatePupilNumbersTotalsBuilderParameters parameters)
        {
            po.PupilNumbersAndCapacityYrY11Pre16Capacity = (parameters.ReceptionToYear6 + parameters.Year7ToYear11).ToString();
            po.PupilNumbersAndCapacityTotalOfCapacityTotals = (parameters.Nursery + parameters.ReceptionToYear6 + parameters.Year7ToYear11 + parameters.Year12ToYear14).ToString();
        }
    }
}
