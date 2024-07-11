using System;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class PrincipleDesignateApiTests : ApiTestsBase
    {
        public PrincipleDesignateApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_NewPrincipleDesignate_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                PrincipleDesignate = new PrincipleDesignateTask()
                {
                    TrustAppointedPrincipleDesignateDate = new DateTime().AddDays(9),
                    CommissionedExternalExpertVisitToSchool = true
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.PrincipleDesignate.ToString());

            projectResponse.PrincipleDesignate.TrustAppointedPrincipleDesignate.Should()
                .Be(true);
            projectResponse.PrincipleDesignate.TrustAppointedPrincipleDesignateDate.Should()
                .Be(request.PrincipleDesignate.TrustAppointedPrincipleDesignateDate);
            projectResponse.PrincipleDesignate.CommissionedExternalExpertVisitToSchool.Should()
                .Be(request.PrincipleDesignate.CommissionedExternalExpertVisitToSchool);
        }

        [Fact]
        public async Task Patch_ExistingPrincipleDesignate_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            
            var principleDesignateTask = DatabaseModelBuilder.BuildPrincipleDesignateTask(project.Rid);
            context.Milestones.Add(principleDesignateTask);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                PrincipleDesignate = new PrincipleDesignateTask()
                {
                    TrustAppointedPrincipleDesignateDate = null,
                    CommissionedExternalExpertVisitToSchool = false
                }
            };

            await _client.UpdateProjectTask(projectId, request, TaskName.PrincipleDesignate.ToString());
            

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.PrincipleDesignate.ToString());

            projectResponse.PrincipleDesignate.TrustAppointedPrincipleDesignate.Should()
                .Be(false);
            projectResponse.PrincipleDesignate.TrustAppointedPrincipleDesignateDate.Should()
                .Be(null);
            projectResponse.PrincipleDesignate.CommissionedExternalExpertVisitToSchool.Should()
                .Be(request.PrincipleDesignate.CommissionedExternalExpertVisitToSchool);
        }

    }
}
