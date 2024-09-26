using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration
{
    [Collection(ApiTestCollection.ApiTestCollectionName)]
    public class BulkEditApiTests : ApiTestsBase
    {
        public BulkEditApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
            
        }

        [Fact]
        public async Task BasicValidationTest()
        {

            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;
            var ExistingSchoolName = project.ProjectStatusCurrentFreeSchoolName;
            var NewSchoolName = "School Name";


            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();
            
            var bulkValidateRequest = new BulkEditRequest
            {
                Headers = new List<HeaderInfo>
                {
                    new HeaderInfo { Name = HeaderNames.ProjectId, Index = 0 },
                    new HeaderInfo { Name = HeaderNames.SchoolName, Index = 1 },
                },
                Rows = new List<RowInfo>
                {
                    new RowInfo
                    {
                        FileRowIndex = 1,
                        Columns = new List<ColumnInfo>
                        {
                            new ColumnInfo { ColumnIndex = 0, Value = projectId.ToString() },
                            new ColumnInfo { ColumnIndex = 1, Value = NewSchoolName },
                        }
                    }
                }
            };

            var response = await _testFixture.Client.PostAsync($"api/v1/bulkedit/validate", bulkValidateRequest.ConvertToJson());

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiSingleResponseV2<BulkEditValidateResponse>>();

            result.Data.Headers.Should().BeEquivalentTo(bulkValidateRequest.Headers);
            
            var resultRow = result.Data.ValidationResultRows.FirstOrDefault();

            resultRow.Columns.Should().BeEquivalentTo(new List<ValueChangeInfo> 
            {
                new()
                {
                    ColumnIndex = 0,
                    CurrentValue = projectId.ToString(),
                    NewValue = projectId.ToString()
                }
                ,
                new() 
                { 
                    ColumnIndex = 1, 
                    CurrentValue = ExistingSchoolName, 
                    NewValue = NewSchoolName 
                } 
            });

        }

        [Fact]
        public async Task BasicSaveTest()
        {

            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;
            var NewSchoolName = "School Name";


            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var bulkEditRequest = new BulkEditRequest
            {
                Headers = new List<HeaderInfo>
                {
                    new HeaderInfo { Name = HeaderNames.ProjectId, Index = 0 },
                    new HeaderInfo { Name = HeaderNames.SchoolName, Index = 1 },
                },
                Rows = new List<RowInfo>
                {
                    new RowInfo
                    {
                        FileRowIndex = 1,
                        Columns = new List<ColumnInfo>
                        {
                            new ColumnInfo { ColumnIndex = 0, Value = projectId.ToString() },
                            new ColumnInfo { ColumnIndex = 1, Value = NewSchoolName },
                        }
                    }
                }
            };

            var response = await _testFixture.Client.PostAsync($"api/v1/bulkedit/commit", bulkEditRequest.ConvertToJson());

            response.EnsureSuccessStatusCode();

            await _testFixture.Client.PostAsync($"api/v1/bulkedit/commit", bulkEditRequest.ConvertToJson());


            var overviewResponse = await _testFixture.Client.GetAsync($"api/v1/client/projects/{project.ProjectStatusProjectId}/overview");
            overviewResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await overviewResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<ProjectOverviewResponse>>();

            result.Data.ProjectStatus.CurrentFreeSchoolName.Should().Be(NewSchoolName);
        }
    }
}
