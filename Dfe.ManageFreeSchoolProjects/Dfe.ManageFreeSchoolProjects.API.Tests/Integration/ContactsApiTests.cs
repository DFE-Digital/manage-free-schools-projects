using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration;

[Collection(ApiTestCollection.ApiTestCollectionName)]
public class ContactsApiTests : ApiTestsBase
{
    private static readonly Fixture Fixture = new();

    private Kpi _project;

    public ContactsApiTests(ApiTestFixture apiTestFixture) : base(apiTestFixture)
    {
    }

    [Fact]
    public async Task GetProjectContacts_Returns_200()
    {
        await SetUpTestData();

        var contactsResponse =
            await _client.GetAsync(ContactsUrl(_project.ProjectStatusProjectId));
        contactsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent =
            await contactsResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetContactsResponse>>();
        
        var expectedContacts = new ContactsTask()
        {
            ProjectManagedBy = new Contact() { Name = _project.KeyContactsFsgLeadContact, Email = _project.KeyContactsFsgLeadContactEmail },
            TeamLead = new Contact() { Name = _project.KeyContactsFsgTeamLeader, Email = _project.KeyContactsFsgTeamLeaderEmail },
            Grade6 = new Contact() { Name = _project.KeyContactsFsgGrade6, Email = _project.KeyContactsFsgGrade6Email },
            ProjectDirector = new Contact() { Name = _project.KeyContactsEsfaCapitalProjectDirector, Email = _project.KeyContactsEsfaCapitalProjectDirectorEmail },
            TrustChair = new Contact() { Name = _project.KeyContactsChairOfGovernorsName, Email = _project.KeyContactsChairOfGovernorsEmail },
            SchoolChair = new Contact() { Name = _project.KeyContactsChairOfGovernorsMat, Email = _project.KeyContactsChairOfGovernorsMatEmail },
        };

        responseContent.Data.Contacts.Should().BeEquivalentTo(expectedContacts);
    }

    [Fact]
    public async Task GetProjectContacts_Returns_404_NotFound()
    {
        var contactsResponse = await _client.GetAsync(ContactsUrl("999"));
        contactsResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateProjectContacts_Returns_200()
    {
        await SetUpTestData();

        var contactToUpdate = new UpdateContactsRequest()
        {
            Contacts = new ContactsTask()
            {
                ProjectManagedBy = new Contact() { Name = Fixture.Create<string>(), Email = Fixture.Create<string>() },
                TeamLead = new Contact() { Name = Fixture.Create<string>(), Email = Fixture.Create<string>() },
                Grade6 = new Contact() { Name = Fixture.Create<string>(), Email = Fixture.Create<string>() },
            }
        };

        var createProjectRiskResponse = await _client.PatchAsync(
            ContactsUrl(_project.ProjectStatusProjectId), contactToUpdate.ConvertToJson());
        createProjectRiskResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var contactsResponse =
            await _client.GetAsync(ContactsUrl(_project.ProjectStatusProjectId));
        contactsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent =
            await contactsResponse.Content.ReadFromJsonAsync<ApiSingleResponseV2<GetContactsResponse>>();

        var expectedContacts = new ContactsTask()
        {

            ProjectManagedBy = new Contact() { Name = contactToUpdate.Contacts.ProjectManagedBy.Name, Email = contactToUpdate.Contacts.ProjectManagedBy.Email },
            TeamLead = new Contact() { Name = contactToUpdate.Contacts.TeamLead.Name, Email = contactToUpdate.Contacts.TeamLead.Email },
            Grade6 = new Contact() { Name = contactToUpdate.Contacts.Grade6.Name, Email = contactToUpdate.Contacts.Grade6.Email },
            ProjectDirector = new Contact() { Name = _project.KeyContactsEsfaCapitalProjectDirector, Email = _project.KeyContactsEsfaCapitalProjectDirectorEmail },
            TrustChair = new Contact() { Name = _project.KeyContactsChairOfGovernorsName, Email = _project.KeyContactsChairOfGovernorsEmail },
            SchoolChair = new Contact() { Name = _project.KeyContactsChairOfGovernorsMat, Email = _project.KeyContactsChairOfGovernorsMatEmail },
        };

        responseContent.Data.Contacts.Should().BeEquivalentTo(expectedContacts);
    }
    private string ContactsUrl(string projectid)
    {
        return $"/api/v1/client/contacts?projectId={projectid}";
    }

    private async Task  SetUpTestData()
    {
        var context = _testFixture.GetContext();
        _project = DatabaseModelBuilder.BuildProject();
        context.Kpi.Add(_project);
        await context.SaveChangesAsync();
    }
}