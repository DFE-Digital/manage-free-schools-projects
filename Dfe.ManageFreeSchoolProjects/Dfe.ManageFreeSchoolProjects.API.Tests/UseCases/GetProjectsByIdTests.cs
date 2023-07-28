using ConcernsCaseWork.API.UseCases.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.RequestModels.Projects;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels.Project;
using Dfe.ManageFreeSchoolProjects.Data.Gateways.Projects;
using Dfe.ManageFreeSchoolProjects.Data.Models.Projects;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases
{
    public class GetProjectsByIdTests
    {
        [Fact]
        public void GetProjectByID_ReturnsProjects()
        {
            const string ProjectId = "FS1234";

            var projectGateway = new Mock<IProjectGateway>();

            var getProjectRequest = Builder<GetProjectRequest>
                .CreateNew()
                .With(pr =>
                {
                    return pr.ProjectId = ProjectId;
                })
                .Build();

            var project = Builder<Project>
                .CreateNew()
                .With(pr =>
                {
                    return pr.ProjectId = ProjectId;
                })
                .Build();

            var projectRepsonse = new ProjectResponse()
            {
                ApplicationNumber = project.ApplicationNumber,
                ApplicationWave = project.ApplicationWave,
                Id = project.Id,
                ProjectId = ProjectId,
                SchoolName = project.SchoolName,
                CreatedAt = project.CreatedAt,
                CreatedBy = project.CreatedBy,
                UpdatedAt = project.UpdatedAt
            };

            projectGateway.Setup(a => a.GetProjectById(It.Is<string>(a => a == ProjectId)))
                .Returns(() => project);

            var getProjectById = new GetProjectById(projectGateway.Object);
            
            var result = getProjectById.Execute(getProjectRequest);
            result.Should().BeEquivalentTo(projectRepsonse);
        }
    }
}
