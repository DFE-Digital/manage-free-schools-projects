namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class UpdateProjectByTaskRequest
    {
        public RiskAppraisalTask RiskAppraisal { get; set; }
        public DatesTask Dates { get; set; }
        public SchoolTask School { get; set; }
        public TrustTask Trust { get; set; }
		public ConstituencyTask Constituency { get; set; }

		public RegionAndLocalAuthorityTask RegionAndLocalAuthorityTask { get; set; }

        public string TaskToUpdate
        {
            get
            {
                if (School != null)
                    return "School";
                if (Dates != null)
                    return "Dates";
                if (RiskAppraisal != null)
                    return "RiskAppraisal";
                if (Trust != null)
                    return "Trust";
                if (RegionAndLocalAuthorityTask != null)
                    return "RegionAndLocalAuthority";
				if (Constituency != null)
					return "Constituency";
				return null;
            }
        }
    }
}
