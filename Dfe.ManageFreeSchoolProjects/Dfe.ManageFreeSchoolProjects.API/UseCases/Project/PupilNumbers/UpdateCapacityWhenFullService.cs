using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public class UpdateCapacityWhenFullService : IUpdatePupilNumbersSectionService
    {
        public void Execute(UpdatePupilNumbersRequest request, Po po)
        {
            if (request.CapacityWhenFull == null)
            {
                return;
            }

            po.PupilNumbersAndCapacityNurseryUnder5s = request.CapacityWhenFull.Nursery.ToString();
            po.PupilNumbersAndCapacityYrY6Capacity = request.CapacityWhenFull.ReceptionToYear6.ToString();
            po.PupilNumbersAndCapacityY7Y11Capacity = request.CapacityWhenFull.Year7ToYear11.ToString();
            po.PupilNumbersAndCapacityY12Y14Post16Capacity = request.CapacityWhenFull.Year12ToYear14.ToString();
            po.PupilNumbersAndCapacitySpecialistResourceProvisionSpecial = request.CapacityWhenFull.SpecalistEducationNeeds.ToString();
            po.PupilNumbersAndCapacitySpecialistResourceProvisionAp = request.CapacityWhenFull.AlternativeProvision.ToString();
        }
    }
}
