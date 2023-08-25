using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [Route("api/v{version:apiVersion}/client/projects/{projectId}")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public async void Get(string projectId)
        {
            
        }
    }
}
