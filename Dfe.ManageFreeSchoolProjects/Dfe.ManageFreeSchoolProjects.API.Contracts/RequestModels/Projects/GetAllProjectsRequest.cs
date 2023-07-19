using System.ComponentModel.DataAnnotations;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects
{
    public class GetAllProjectsRequest
    {
        [Required]
        public string User { get; set; }
    }
}
