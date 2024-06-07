namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;

    public record Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

    }

