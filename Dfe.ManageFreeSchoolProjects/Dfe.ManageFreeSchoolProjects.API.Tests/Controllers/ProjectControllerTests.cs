using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using ManageFreeSchoolProjects.API.Controllers;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using ConcernsCaseWork.API.UseCases.Project;
using System.Linq;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Controllers
{
    public class ProjectControllerTests
    {
        private readonly Mock<ILogger<ProjectController>> mockLogger = new Mock<ILogger<ProjectController>>();

        [Fact]
        public async Task CreateProject_Returns201WhenSuccessfullyCreatesAProject()
        {
            var project = new Mock<ICreateProjectCase>();
            var createProjectRequest = Builder<CreateProjectRequest>
                .CreateNew()
                .With(pr => pr.ProjectId = "FS1234")
                .Build();

            var ProjectResponse = Builder<ProjectResponse>
                .CreateNew().Build();

            project.Setup(a => a.Execute(createProjectRequest))
                .Returns(ProjectResponse);

            var controller = new ProjectController(
                mockLogger.Object,
                project.Object,
                null,
                null,
                null,
                null
            );

            var result = await controller.Create(createProjectRequest);

            var expected = new ApiSingleResponseV2<ProjectResponse>(ProjectResponse);

            result.Result.Should().BeEquivalentTo(new ObjectResult(expected) {StatusCode = StatusCodes.Status201Created});
        }


        [Fact]
        public async Task GetProjectsByUser_Returns200_And_ReturnsProjects()
        {
            const string UserId = "User1";

            var getProjectsByUser = new Mock<IGetProjectsByUser>();

            var getAllProjectsRequest = Builder<GetProjectsByUserRequest>
                .CreateNew()
                .With(pr => pr.User = UserId)
                .Build();

            var ProjectResponse = Builder <ProjectResponse>
                .CreateListOfSize(5).Build().ToArray();

            getProjectsByUser.Setup(a => a.Execute(It.Is<GetProjectsByUserRequest>(g => g.User == UserId)))
                .Returns(ProjectResponse);

            var controller = new ProjectController(
                mockLogger.Object,
                null,
                getProjectsByUser.Object,
                null,
                null,
                null
            );

            var result = await controller.GetProjectsByUser(UserId);
            
            var expected = new ApiResponseV2<ProjectResponse>(ProjectResponse, new PagingResponse() { Page = 1, RecordCount = 5, NextPageUrl = null});

            result.Result.Should().BeEquivalentTo(new OkObjectResult(expected));
        }

        [Fact]
        public async Task GetProjectById_ReturnsNotFound_WhenProjectIsNotFound()
        {
            const string ProjectId = "FS1234";

            var getProjectById = new Mock<IGetProjectByIdCase>();

            var createProjectRequest = Builder<GetProjectRequest>
                .CreateNew()
                .With(pr =>
                {
                    return pr.ProjectId = ProjectId;
                })
                .Build();

            getProjectById.Setup(a => a.Execute(createProjectRequest))
                .Returns(() => null);

            var controller = new ProjectController(
                mockLogger.Object,
                null,
                null,
                getProjectById.Object,
                null,
                null
            );

            var result = await controller.GetProjectById(ProjectId);
            result.Result.Should().BeEquivalentTo(new NotFoundResult());
        }

        [Fact]
        public async Task GetProjectById_Returns200AndTheFoundProject_WhenSuccessfullyGetsAProjectById()
        {
            const string ProjectId = "FS1234";

            var getProjectById = new Mock<IGetProjectByIdCase>();

            var getProjectRequest = Builder<GetProjectRequest>
                .CreateNew()
                .With(pr =>
                {
                    return pr.ProjectId = ProjectId;
                })
                .Build();

            var ProjectResponse = Builder<ProjectResponse>
                .CreateNew().Build();

            getProjectById.Setup(a => a.Execute(It.Is<GetProjectRequest>(g => g.ProjectId == ProjectId)))
                .Returns(ProjectResponse);

            var controller = new ProjectController(
                mockLogger.Object,
                null,
                null,
                getProjectById.Object,
                null,
                null
            );

            var result = await controller.GetProjectById(ProjectId);

            var expected = new ApiSingleResponseV2<ProjectResponse>(ProjectResponse);

            result.Result.Should().BeEquivalentTo(new OkObjectResult(expected));
        }

        [Fact]
        public async Task EditProject_Returns200AndTheUpdatedProject_WhenSuccessfullyGetsAProject()
        {
            const string ProjectId = "FS1234";

            var editProject = new Mock<IEditProjectCase>();
            var editRequest = Builder<EditProjectRequest>.CreateNew()
                .With(pr => pr.ProjectId = ProjectId)
                .Build();

            var ProjectResponse = Builder<ProjectResponse>
                .CreateNew().Build();

            editProject.Setup(a => a.Execute(It.Is<EditProjectRequest>(e => e.ProjectId == ProjectId)))
                .Returns(ProjectResponse);

            var controller = new ProjectController(
                mockLogger.Object,
                null,
                null,
                null,
                null,
                editProject.Object
            );

            var result = await controller.Edit(editRequest);

            var expected = new ApiSingleResponseV2<ProjectResponse>(ProjectResponse);

            result.Result.Should().BeEquivalentTo(new OkObjectResult(expected));
        }

        [Fact]
        public async Task UpdateProject_Returns404NotFound_WhenNoProjectIsFound()
        {
            const string ProjectId = "FS1234";

            var editProject = new Mock<IEditProjectCase>();
            var editRequest = Builder<EditProjectRequest>.CreateNew()
                .With(pr => pr.ProjectId = ProjectId)
                .Build();

            var ProjectResponse = Builder<ProjectResponse>
            .CreateNew().Build();

            editProject.Setup(a => a.Execute(It.IsAny<EditProjectRequest>())).Returns(() => null);

            var controller = new ProjectController(
                mockLogger.Object,
                null,
                null,
                null,
                null,
                editProject.Object
            );

            var result = await controller.Edit(editRequest);

            result.Result.Should().BeEquivalentTo(new NotFoundResult());
        }
    }
}