using System;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class PrincipalDesignateApiTests : ApiTestsBase
    {
        public PrincipalDesignateApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_NewPrincipalDesignate_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                PrincipalDesignate = new PrincipalDesignateTask()
                {
                    TrustAppointedPrincipleDesignate = true,
                    TrustAppointedPrincipleDesignateDate = new DateTime().AddDays(9),
                    CommissionedExternalExpertVisitToSchool = true
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.PrincipalDesignate.ToString());

            projectResponse.PrincipalDesignate.TrustAppointedPrincipleDesignate.Should()
                .Be(true);
            projectResponse.PrincipalDesignate.TrustAppointedPrincipleDesignateDate.Should()
                .Be(request.PrincipalDesignate.TrustAppointedPrincipleDesignateDate);
            projectResponse.PrincipalDesignate.CommissionedExternalExpertVisitToSchool.Should()
                .Be(request.PrincipalDesignate.CommissionedExternalExpertVisitToSchool);
        }

        [Fact]
        public async Task Patch_ExistingPrincipalDesignate_Returns_201()
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
                PrincipalDesignate = new PrincipalDesignateTask()
                {
                    TrustAppointedPrincipleDesignate = false,
                    TrustAppointedPrincipleDesignateDate = null,
                    CommissionedExternalExpertVisitToSchool = false
                }
            };

            await _client.UpdateProjectTask(projectId, request, TaskName.PrincipalDesignate.ToString());
            

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.PrincipalDesignate.ToString());

            projectResponse.PrincipalDesignate.TrustAppointedPrincipleDesignate.Should()
                .Be(false);
            projectResponse.PrincipalDesignate.TrustAppointedPrincipleDesignateDate.Should()
                .Be(null);
            projectResponse.PrincipalDesignate.CommissionedExternalExpertVisitToSchool.Should()
                .Be(request.PrincipalDesignate.CommissionedExternalExpertVisitToSchool);
        }

    }
}
