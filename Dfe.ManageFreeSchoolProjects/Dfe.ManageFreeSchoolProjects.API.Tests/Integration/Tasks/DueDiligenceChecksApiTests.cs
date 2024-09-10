using System;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration.Tasks;

[Collection(ApiTestCollection.ApiTestCollectionName)]
public class DueDiligenceChecksApiTests(ApiTestFixture apiTestFixture) : ApiTestsBase(apiTestFixture)
{
    [Fact]
    public async Task Patch_New_DueDiligenceChecksReturns_201()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        await context.SaveChangesAsync();

        var updateDueDiligenceChecksRequest = new UpdateProjectByTaskRequest
        {
            DueDiligenceChecks = new DueDiligenceChecks
            {
                ReceivedChairOfTrusteesCountersignedCertificate = true,
                NonSpecialistChecksDoneOnAllTrustMembersAndTrustees = true,
                DateWhenAllChecksWereCompleted = new DateTime().Date.AddDays(30),
                RequestedCounterExtremismChecks = true,
                SavedNonSpecialistChecksSpreadsheetInWorkplaces = true,
                DeletedAnyCopiesOfChairsDBSCertificate = true,
                DeletedEmailsContainingSuitabilityAndDeclarationForms = true
            }
        };

        var projectResponse =
            await _client.UpdateProjectTask(projectId, updateDueDiligenceChecksRequest,
                TaskName.DueDiligenceChecks.ToString());

        AssertDueDiligenceChecks(projectResponse.DueDiligenceChecks,
            updateDueDiligenceChecksRequest.DueDiligenceChecks);
    }


    [Fact]
    public async Task Patch_New_DueDiligenceChecksReturnsWithNullValues_201()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        await context.SaveChangesAsync();

        var updateDueDiligenceChecksRequest = new UpdateProjectByTaskRequest
        {
            DueDiligenceChecks = new DueDiligenceChecks
            {
                ReceivedChairOfTrusteesCountersignedCertificate = null,
                NonSpecialistChecksDoneOnAllTrustMembersAndTrustees = null,
                DateWhenAllChecksWereCompleted = null,
                RequestedCounterExtremismChecks = null,
                SavedNonSpecialistChecksSpreadsheetInWorkplaces = null,
                DeletedAnyCopiesOfChairsDBSCertificate = null,
                DeletedEmailsContainingSuitabilityAndDeclarationForms = null
            }
        };

        var projectResponse =
            await _client.UpdateProjectTask(projectId, updateDueDiligenceChecksRequest,
                TaskName.DueDiligenceChecks.ToString());

        AssertDueDiligenceChecks(projectResponse.DueDiligenceChecks,
            updateDueDiligenceChecksRequest.DueDiligenceChecks);
    }


    [Fact]
    public async Task Patch_Existing_DueDiligenceChecksReturns_201()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        await context.SaveChangesAsync();

        var createDueDiligenceChecksRequest = new UpdateProjectByTaskRequest
        {
            DueDiligenceChecks = new DueDiligenceChecks
            {
                ReceivedChairOfTrusteesCountersignedCertificate = true,
                NonSpecialistChecksDoneOnAllTrustMembersAndTrustees = true,
                DateWhenAllChecksWereCompleted = new DateTime().Date.AddDays(30),
                RequestedCounterExtremismChecks = true,
                SavedNonSpecialistChecksSpreadsheetInWorkplaces = true,
                DeletedAnyCopiesOfChairsDBSCertificate = true,
                DeletedEmailsContainingSuitabilityAndDeclarationForms = true
            }
        };

        await _client.UpdateProjectTask(projectId, createDueDiligenceChecksRequest, TaskName.DueDiligenceChecks.ToString());
        
        var updateDueDiligenceChecksRequest = new UpdateProjectByTaskRequest
        {
            DueDiligenceChecks = new DueDiligenceChecks
            {
                ReceivedChairOfTrusteesCountersignedCertificate = true,
                NonSpecialistChecksDoneOnAllTrustMembersAndTrustees = null,
                DateWhenAllChecksWereCompleted = new DateTime().Date.AddDays(60),
                RequestedCounterExtremismChecks = true,
                SavedNonSpecialistChecksSpreadsheetInWorkplaces = null,
                DeletedAnyCopiesOfChairsDBSCertificate = false,
                DeletedEmailsContainingSuitabilityAndDeclarationForms = false
            }
        };
        
        var projectResponse =
            await _client.UpdateProjectTask(projectId, updateDueDiligenceChecksRequest,
                TaskName.DueDiligenceChecks.ToString());
        
        AssertDueDiligenceChecks(projectResponse.DueDiligenceChecks, updateDueDiligenceChecksRequest.DueDiligenceChecks);
    }


    private static void AssertDueDiligenceChecks(DueDiligenceChecks actual, DueDiligenceChecks expected)
    {
        actual.ReceivedChairOfTrusteesCountersignedCertificate.Should()
            .Be(expected.ReceivedChairOfTrusteesCountersignedCertificate);
        actual.NonSpecialistChecksDoneOnAllTrustMembersAndTrustees.Should()
            .Be(expected.NonSpecialistChecksDoneOnAllTrustMembersAndTrustees);
        actual.DateWhenAllChecksWereCompleted.Should().Be(expected.DateWhenAllChecksWereCompleted);
        actual.RequestedCounterExtremismChecks.Should().Be(expected.RequestedCounterExtremismChecks);
        actual.DeletedAnyCopiesOfChairsDBSCertificate.Should()
            .Be(expected.DeletedAnyCopiesOfChairsDBSCertificate);
        actual.DeletedEmailsContainingSuitabilityAndDeclarationForms.Should()
            .Be(expected.DeletedEmailsContainingSuitabilityAndDeclarationForms);
    }
}