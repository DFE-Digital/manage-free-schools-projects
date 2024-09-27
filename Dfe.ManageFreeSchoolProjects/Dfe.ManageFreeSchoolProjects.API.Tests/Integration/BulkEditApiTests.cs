using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using System;
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
            var ExistingOpeningDate = project.ProjectStatusActualOpeningDate;
            var NewSchoolName = "School Name";
            var NewOpeningDate = "01/01/2021";


            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();
            
            var bulkValidateRequest = new BulkEditRequest
            {
                Headers = new List<HeaderInfo>
                {
                    new HeaderInfo { Name = HeaderNames.ProjectId, Index = 0 },
                    new HeaderInfo { Name = HeaderNames.SchoolName, Index = 1 },
                    new HeaderInfo { Name = HeaderNames.OpeningDate, Index = 2 },
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
                            new ColumnInfo { ColumnIndex = 2, Value = NewOpeningDate},
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
                },
                new()
                {
                    ColumnIndex = 2,
                    CurrentValue = ExistingOpeningDate.Value.ToString("dd/MM/yyyy"),
                    NewValue = NewOpeningDate
                }
            });

        }

        [Fact]
        public async Task BasicSaveTest()
        {

            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;
            var NewSchoolName = "School Name";
            var NewOpeningDate = new DateTime(2021, 1, 1);

            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var bulkEditRequest = new BulkEditRequest
            {
                Headers = new List<HeaderInfo>
                {
                    new HeaderInfo { Name = HeaderNames.ProjectId, Index = 0 },
                    new HeaderInfo { Name = HeaderNames.SchoolName, Index = 1 },
                    new HeaderInfo { Name = HeaderNames.OpeningDate, Index = 2 },
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
                            new ColumnInfo { ColumnIndex = 2, Value = NewOpeningDate.ToString("dd/MM/yyyy")},
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
            result.Data.ProjectStatus.ActualOpeningDate.Should().Be(NewOpeningDate);
        }


        [Fact]
        public async Task ProjectIDMissingTest()
        {
            var project = DatabaseModelBuilder.BuildProject();

            var projectId = project.ProjectStatusProjectId;
            var ExistingSchoolName = project.ProjectStatusCurrentFreeSchoolName;
            var ExistingOpeningDate = project.ProjectStatusActualOpeningDate;
            var NewSchoolName = "School Name";
            var NewOpeningDate = "01/01/2021";


            using var context = _testFixture.GetContext();
            context.Kpi.Add(project);
            await context.SaveChangesAsync();

            var bulkValidateRequest = new BulkEditRequest
            {
                Headers = new List<HeaderInfo>
                {
                    new HeaderInfo { Name = HeaderNames.ProjectId, Index = 0 },
                    new HeaderInfo { Name = HeaderNames.SchoolName, Index = 1 },
                    new HeaderInfo { Name = HeaderNames.OpeningDate, Index = 2 },
                },
                Rows = new List<RowInfo>
                {
                    new RowInfo
                    {
                        FileRowIndex = 1,
                        Columns = new List<ColumnInfo>
                        {
                            new ColumnInfo { ColumnIndex = 0, Value = "ABCDEF" },
                            new ColumnInfo { ColumnIndex = 1, Value = NewSchoolName },
                            new ColumnInfo { ColumnIndex = 2, Value = NewOpeningDate },
                        }
                    },
                    new RowInfo
                    {
                        FileRowIndex = 2,
                        Columns = new List<ColumnInfo>
                        {
                            new ColumnInfo { ColumnIndex = 0, Value = projectId.ToString() },
                            new ColumnInfo { ColumnIndex = 1, Value = NewSchoolName },
                            new ColumnInfo { ColumnIndex = 2, Value = NewOpeningDate },
                        }
                    }
                }
            };

            var response = await _testFixture.Client.PostAsync($"api/v1/bulkedit/validate", bulkValidateRequest.ConvertToJson());

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiSingleResponseV2<BulkEditValidateResponse>>();

            result.Data.Headers.Should().BeEquivalentTo(bulkValidateRequest.Headers);

            var resultRow = result.Data.ValidationResultRows.FirstOrDefault(x => x.FileRowIndex == 1);

            resultRow.Columns.Should().BeEquivalentTo(new List<ValueChangeInfo>
            {
                new()
                {
                    ColumnIndex = 0,
                    CurrentValue = "",
                    NewValue = "ABCDEF",
                    Error = "Project Id does not exist"
                },
                new()
                {
                    ColumnIndex = 1,
                    CurrentValue = "",
                    NewValue = NewSchoolName
                },
                new()
                {
                    ColumnIndex = 2,
                    CurrentValue = "",
                    NewValue = NewOpeningDate
                }
            });

            var resultValidRow = result.Data.ValidationResultRows.FirstOrDefault(x => x.FileRowIndex == 2);

            resultValidRow.Columns.Should().BeEquivalentTo(new List<ValueChangeInfo>
            {
                new()
                {
                    ColumnIndex = 0,
                    CurrentValue = projectId.ToString(),
                    NewValue = projectId.ToString(),
                },
                new()
                {
                    ColumnIndex = 1,
                    CurrentValue = ExistingSchoolName,
                    NewValue = NewSchoolName
                },
                new()
                {
                    ColumnIndex = 2,
                    CurrentValue = ExistingOpeningDate.Value.ToString("dd/MM/yyyy"),
                    NewValue = NewOpeningDate
                }
            });
        }


        [Fact]
        public async Task IfBlankDoNotChangeTest()
        {

            var project = DatabaseModelBuilder.BuildProject();
            var projectId = project.ProjectStatusProjectId;
            var schoolName = project.ProjectStatusCurrentFreeSchoolName;

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
                            new ColumnInfo { ColumnIndex = 1, Value = string.Empty },
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

            result.Data.ProjectStatus.CurrentFreeSchoolName.Should().Be(schoolName);
        }
    }
}
