using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PupilNumbersChecks
{
    public static class PupilNumbersChecksTaskBuilder
    {
        public static PupilNumbersChecksTask Build(Milestones milestones)
        {
            if (milestones == null)
            {
                return new PupilNumbersChecksTask();
            }

            return new PupilNumbersChecksTask()
            {
                SchoolReceivedEnoughApplications = 
                    milestones.FsgPreOpeningMilestonesSchoolReceivedEnoughApplications,
                CapacityDataMatchesFundingAgreement = 
                    milestones.FsgPreOpeningMilestonesCapacityDataMatchesFundingAgreement,
                CapacityDataMatchesGiasRegistration = milestones.FsgPreOpeningMilestonesCapacityDataMatchesGiasRegistration
                
            };

        }
    }
}
