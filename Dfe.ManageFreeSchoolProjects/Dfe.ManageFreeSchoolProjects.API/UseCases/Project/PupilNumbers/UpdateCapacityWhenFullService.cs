using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public class UpdateCapacityWhenFullService : IUpdatePupilNumbersSectionService
    {
        public void Execute(Po po, UpdatePupilNumbersRequest request)
        {
            if (request.CapacityWhenFull == null)
            {
                return;
            }

            po.PupilNumbersAndCapacityNurseryUnder5s = request.CapacityWhenFull.Nursery.ToString();
            po.PupilNumbersAndCapacityYrY6Capacity = request.CapacityWhenFull.ReceptionToYear6.ToString();
            po.PupilNumbersAndCapacityY7Y11Capacity = request.CapacityWhenFull.Year7ToYear11.ToString();
            po.PupilNumbersAndCapacityY12Y14Post16Capacity = request.CapacityWhenFull.Year12ToYear14.ToString();
            po.PupilNumbersAndCapacitySpecialistResourceProvisionSpecial = request.CapacityWhenFull.SpecialistEducationNeeds.ToString();
            po.PupilNumbersAndCapacitySpecialistResourceProvisionAp = request.CapacityWhenFull.AlternativeProvision.ToString();

            UpdatePupilNumbersTotalsBuilderParameters parameters = new UpdatePupilNumbersTotalsBuilderParameters()
            {
                Nursery = request.CapacityWhenFull.Nursery,
                ReceptionToYear6 = request.CapacityWhenFull.ReceptionToYear6,
                Year7ToYear11 = request.CapacityWhenFull.Year7ToYear11,
                Year12ToYear14 = request.CapacityWhenFull.Year12ToYear14
            };
            
            UpdatePupilNumbersTotalsBuilder.Build(po, parameters);
        }
    }
}
