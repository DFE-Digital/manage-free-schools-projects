using ConcernsCaseWork.API.UseCases.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace ManageFreeSchoolProjects.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly ICreateProjectCase _createProjectUseCase;
        private readonly IGetProjectsByUser _getProjectUseCase;
        private readonly IGetProjectByIdCase _getProjectByIdUseCase;
        private readonly IUseCase<DeleteProjectRequest, ProjectResponse> _deleteProjectUseCase;
        private readonly IEditProjectCase _editProjectUseCase;

        public ProjectController(ILogger<ProjectController> logger,
                                 ICreateProjectCase createProjectUseCase,
                                 IGetProjectsByUser getProjectUseCase,
                                 IGetProjectByIdCase getProjectByIdUseCase,
                                 IUseCase<DeleteProjectRequest, ProjectResponse> deleteProjectUseCase,
                                 IEditProjectCase editProjectUseCase)
        {
            _logger = logger;
            _createProjectUseCase = createProjectUseCase;
            _getProjectUseCase = getProjectUseCase;
            _getProjectByIdUseCase = getProjectByIdUseCase;
            _deleteProjectUseCase = deleteProjectUseCase;
            _editProjectUseCase = editProjectUseCase;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ApiResponseV2<ProjectResponse[]>>> GetProjectsByUser(string user, CancellationToken cancellationToken = default)
        {
            var projects = _getProjectUseCase.Execute(new GetProjectsByUserRequest() { User = user });

            var pagingResponse = PagingResponseFactory.Create(1, projects.Count(), projects.Count(), Request);
            var response = new ApiResponseV2<ProjectResponse>(projects, pagingResponse);

            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [Route("id")]
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ApiSingleResponseV2<ProjectResponse>>> GetProjectById(string projectId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"GetProjectById called with Project ID: {projectId}");
            var project = _getProjectByIdUseCase.Execute(new GetProjectRequest() { ProjectId = projectId });

            if (project == null)
            {
                _logger.LogInformation($"No project found for ID: {project}");
                return NotFound();
            }

            var response = new ApiSingleResponseV2<ProjectResponse>(project);

            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ApiSingleResponseV2<ProjectResponse>>> Create(CreateProjectRequest request, CancellationToken cancellationToken = default)
        {
            var createdProject = _createProjectUseCase.Execute(request);
            var response = new ApiSingleResponseV2<ProjectResponse>(createdProject);

            return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ApiSingleResponseV2<ProjectResponse>>> Delete(string projectId, CancellationToken cancellationToken = default)
        {
            var deletedProject = _deleteProjectUseCase.Execute(new DeleteProjectRequest() { ProjectId = projectId });
            var response = new ApiSingleResponseV2<ProjectResponse>(deletedProject);

            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ApiSingleResponseV2<ProjectResponse>>> Edit(EditProjectRequest request, CancellationToken cancellationToken = default)
        {
            var editedProject = _editProjectUseCase.Execute(request);

            if (editedProject == null)
            {
                _logger.LogInformation($"No project found for ID: {request.ProjectId}");
                return NotFound();
            }

            var response = new ApiSingleResponseV2<ProjectResponse>(editedProject);

            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
