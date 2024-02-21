using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class ReportApiTests : ApiTestsBase
    {
        public ReportApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task Get_AllProjectsExport_Returns_200()
        {
            using var context = _testFixture.GetContext();

            var project = DatabaseModelBuilder.BuildProject();

            context.Kpi.Add(project);

            await context.SaveChangesAsync();

            var response = await _client.GetAsync($"/api/v1/client/reports/all-projects-export");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var fileStreamResult = await response.Content.ReadAsStreamAsync();

            using var memoryStream = new MemoryStream();
            await fileStreamResult.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            // Check we are able to open the excel file
            using SpreadsheetDocument document = SpreadsheetDocument.Open(memoryStream, false);
        }
    }
}
