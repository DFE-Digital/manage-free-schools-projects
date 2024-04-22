namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class PupilNumbersChecksTask
{
    public bool? SchoolReceivedEnoughApplications { get; set; }
        
    public bool? CapacityDataMatchesFundingAgreement { get; set; }
        
    public bool? CapacityDataMatchesGiasRegistration  { get; set; }
}