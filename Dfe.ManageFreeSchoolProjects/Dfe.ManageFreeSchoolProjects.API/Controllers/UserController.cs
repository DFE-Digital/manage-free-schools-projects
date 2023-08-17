using Dfe.ManageFreeSchoolProjects.API.Contracts.Users;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Users;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.ManageFreeSchoolProjects.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/client/users")]
    [ApiController]
    public class UserController
    {
        private readonly ICreateUserService _createUser;
        private readonly IValidator<CreateUserRequest> _createUserRequestValidator;

        public UserController(
            IValidator<CreateUserRequest> createUserRequestValidator,
            ICreateUserService createUser)
        {
            _createUser = createUser;
            _createUserRequestValidator = createUserRequestValidator;
        }

        [HttpPost]
        public ActionResult CreateUser(CreateUserRequest request)
        {
            var validationResult = _createUserRequestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors);
            }

            var result = _createUser.Execute(request);

            if (result.UserCreateState == UserCreateState.Exists)
            {
                return new OkResult();
            }

            return new ObjectResult(null) {
                StatusCode = StatusCodes.Status201Created 
            };
        }
    }
}
