using Azure;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Users;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client/users")]
    [ApiController]
    public class UserController
    {
        private ICreateUser _createUser;

        public UserController(ICreateUser createUser)
        {
            _createUser = createUser;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserRequest request)
        {
            await _createUser.Execute(request);

            return new ObjectResult(null) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
