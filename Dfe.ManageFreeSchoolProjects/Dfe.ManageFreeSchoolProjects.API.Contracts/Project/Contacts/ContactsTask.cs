namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;

    public record ContactsTask
    {
        public string ProjectManagedByName { get; set; }
        public string ProjectManagedByEmail { get; set; }
        public string TeamLeadName { get; set; }
        public string TeamLeadEmail { get; set; }
        public string Grade6Name { get; set; }
        public string Grade6Email { get; set; }
        public string ProjectDirectorName { get; set; }
        public string ProjectDirectorEmail { get; set; }
        public string TrustChairName { get; set; }
        public string TrustChairEmail { get; set; }
        public string SchoolChairName { get; set; }
        public string SchoolChairEmail { get; set; }


    }

