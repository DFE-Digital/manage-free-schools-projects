using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.API.Constants;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class DatesTaskApiTests : ApiTestsBase
    {
        public DatesTaskApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Patch_DatesTask_Returns_201()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var DateTenDaysInFuture = new DateTime().AddDays(10);
            var DateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
                Dates = new DatesTask()
                {
                    DateOfEntryIntoPreopening = DateTenDaysInFuture,
                    ProvisionalOpeningDateAgreedWithTrust = DateNineDaysInFuture,
                    ProjectClosedDate = DateTenDaysInFuture,
                    RealisticYearOfOpening = "2049/2050",
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.Dates.ToString());

            projectResponse.Dates.DateOfEntryIntoPreopening.Should().Be(DateTenDaysInFuture);
            projectResponse.Dates.ProvisionalOpeningDateAgreedWithTrust.Should().Be(DateNineDaysInFuture);
            projectResponse.Dates.ProjectClosedDate.Should().Be(DateTenDaysInFuture);
            projectResponse.Dates.RealisticYearOfOpening.Should().Be(request.Dates.RealisticYearOfOpening);
            projectResponse.SchoolName.Should().Be(project.ProjectStatusCurrentFreeSchoolName);

            using var contextPostSave = _testFixture.GetContext();
            var updatedProject = contextPostSave.Kpi.First(p => p.ProjectStatusProjectId == projectId);
            updatedProject.RyooWd.Should().Be(request.Dates.RealisticYearOfOpening);
        }

        [Fact]
        public async Task Patch_DatesTask_NoRYOOSetsDefaultValue()
        {
            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var DateTenDaysInFuture = new DateTime().AddDays(10);
            var DateNineDaysInFuture = new DateTime().AddDays(9);

            var request = new UpdateProjectByTaskRequest()
            {
                Dates = new DatesTask()
                {
                    DateOfEntryIntoPreopening = DateTenDaysInFuture,
                    ProvisionalOpeningDateAgreedWithTrust = DateNineDaysInFuture,
                    ProjectClosedDate = DateTenDaysInFuture,
                    RealisticYearOfOpening = "",
                }
            };

            var projectResponse = await _client.UpdateProjectTask(projectId, request, TaskName.Dates.ToString());

            projectResponse.Dates.DateOfEntryIntoPreopening.Should().Be(DateTenDaysInFuture);
            projectResponse.Dates.ProvisionalOpeningDateAgreedWithTrust.Should().Be(DateNineDaysInFuture);
            projectResponse.Dates.ProjectClosedDate.Should().Be(DateTenDaysInFuture);
            projectResponse.Dates.RealisticYearOfOpening.Should().Be(request.Dates.RealisticYearOfOpening);
            projectResponse.SchoolName.Should().Be(project.ProjectStatusCurrentFreeSchoolName);

            using var contextPostSave = _testFixture.GetContext();
            var updatedProject = contextPostSave.Kpi.First(p => p.ProjectStatusProjectId == projectId);
            updatedProject.RyooWd.Should().Be(ProjectConstants.RYOODefaultValue);
        }
    }
}
