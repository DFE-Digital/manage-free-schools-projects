using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects
{
    public class CreateProjectRequest
    {
        public List<ProjectDetails> Projects { get; set; } = new List<ProjectDetails>();
    }

    public class ProjectDetails
    {
        [Required]
        public string ProjectId { get; set; }
        public string SchoolName { get; set; }
        public string ApplicationNumber { get; set; }
        
        public string ApplicationWave { get; set; }
        public string Region { get; set; }
        public string LocalAuthority { get; set; }
        public string LocalAuthorityCode { get; set; }
        [Required]
        public string CreatedBy { get; set; }

        public SchoolType SchoolType { get; set; }
    }
}
