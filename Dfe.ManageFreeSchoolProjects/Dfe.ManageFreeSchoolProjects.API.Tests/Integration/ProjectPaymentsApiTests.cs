using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.API.Tests.Utils;
using System.Net;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ProjectPaymentsApiTests : ApiTestsBase
    {
        public ProjectPaymentsApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Get_ProjectPayments_Returns_200()
        {
            // Ensures that if the child tables for the tasks are not populated, the api still works
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var payments = DatabaseModelBuilder.BuildPaymentScheduleTask(project.Rid);
            context.Po.Add(payments);

            await context.SaveChangesAsync();

            var getProjectPaymentsResponse = await _client.GetAsync($"/api/v1/client/projects/{projectId}/payments");
            getProjectPaymentsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
