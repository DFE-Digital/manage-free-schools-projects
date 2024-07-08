using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.API.Tests.Utils;
using System.Threading.Tasks;
using System.Linq;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ReferenceNumbersTaskApiTests : ApiTestsBase
    {
        public ReferenceNumbersTaskApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_ReferenceNumbersTask_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);

            var tasks = TasksStub.BuildListOfTasks(project.Rid);
            context.Tasks.AddRange(tasks);

            await context.SaveChangesAsync();

            var request = new UpdateProjectByTaskRequest()
            {
                ReferenceNumbers = new ReferenceNumbersTask()
                {
                    ProjectId = "poiuytrewq",
                    Urn = "abcdef"
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.ReferenceNumbers.ToString());

            projectResponse.ReferenceNumbers.ProjectId.Should().Be("poiuytrewq");
            projectResponse.ReferenceNumbers.Urn.Should().Be("abcdef");
        }
    }
}
