namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class AdmissionsArrangementsTask
    {
        public DateTime? ForecastDateForConfirmingAdmissionsArrangements { get; set; }

        public bool? TrustConfirmedAdmissionsArrangementsTemplate { get; set; }
       
        public bool? TrustConfirmedAdmissionsArrangementsPolicies { get; set; }

        public DateTime? AdmissionsArrangementsConfirmedDate { get; set; }

        public bool? SavedToWorkplaces { get; set; }
    }
}
