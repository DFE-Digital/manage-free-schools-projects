using System;
using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project
{
    public record CreateProjectResponse
    {
        public List<ProjectResponseDetails> Projects { get; set; } = new List<ProjectResponseDetails>();
    }

    public enum ProjectCreateState
    {
        New = 1,
        Exists = 2
    }

    public class ProjectResponseDetails
    {
        [Required]
        public string ProjectId { get; set; }
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string ApplicationNumber { get; set; }
        [Required]
        public string ApplicationWave { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public ProjectCreateState ProjectCreateState { get; set; }
    }
}

