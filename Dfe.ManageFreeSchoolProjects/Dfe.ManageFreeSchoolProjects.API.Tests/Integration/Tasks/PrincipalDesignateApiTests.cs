using System;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;

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
                    ActualDatePrincipalDesignateAppointed = new DateTime().AddDays(9),
                    CommissionedExternalExpertVisitToSchool = YesNoNotApplicable.Yes,
                    ExpectedDatePrincipalDesignateAppointed = new DateTime().AddDays(8),
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.PrincipalDesignate.ToString());

            projectResponse.PrincipalDesignate.ActualDatePrincipalDesignateAppointed.Should()
                .Be(request.PrincipalDesignate.ActualDatePrincipalDesignateAppointed);
            projectResponse.PrincipalDesignate.CommissionedExternalExpertVisitToSchool.Should()
                .Be(request.PrincipalDesignate.CommissionedExternalExpertVisitToSchool);
            projectResponse.PrincipalDesignate.ExpectedDatePrincipalDesignateAppointed.Should()
                .Be(request.PrincipalDesignate.ExpectedDatePrincipalDesignateAppointed);
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
                    TrustAppointedPrincipalDesignate = false,
                    ActualDatePrincipalDesignateAppointed = null,
                    CommissionedExternalExpertVisitToSchool = YesNoNotApplicable.No,
                    ExpectedDatePrincipalDesignateAppointed = new DateTime().AddDays(8),
                }
            };

            await _client.UpdateProjectTask(projectId, request, TaskName.PrincipalDesignate.ToString());
            

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.PrincipalDesignate.ToString());

            projectResponse.PrincipalDesignate.TrustAppointedPrincipalDesignate.Should()
                .Be(false);
            projectResponse.PrincipalDesignate.ActualDatePrincipalDesignateAppointed.Should()
                .Be(null);
            projectResponse.PrincipalDesignate.CommissionedExternalExpertVisitToSchool.Should()
                .Be(request.PrincipalDesignate.CommissionedExternalExpertVisitToSchool);
            projectResponse.PrincipalDesignate.ExpectedDatePrincipalDesignateAppointed.Should()
                .Be(request.PrincipalDesignate.ExpectedDatePrincipalDesignateAppointed);
        }

    }
}
