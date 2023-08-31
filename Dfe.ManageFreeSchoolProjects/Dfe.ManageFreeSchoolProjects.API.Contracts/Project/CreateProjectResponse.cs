using System;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project
{
    public record CreateProjectResponse
    {
        public CreateProjectRequest createProjectRequest { get; set; }
        public ProjectCreateState ProjectCreateState { get; set; }
    }

    public enum ProjectCreateState
    {
        New = 1,
        Exists = 2
    }
}

