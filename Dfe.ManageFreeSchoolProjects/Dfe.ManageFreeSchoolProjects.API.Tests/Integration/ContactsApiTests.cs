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
            ProjectAssignedTo = new Contact() { Name = _project.KeyContactsFsgLeadContact, Email = _project.KeyContactsFsgLeadContactEmail },
            TeamLead = new Contact() { Name = _project.KeyContactsFsgTeamLeader, Email = _project.KeyContactsFsgTeamLeaderEmail },
            Grade6 = new Contact() { Name = _project.KeyContactsFsgGrade6, Email = _project.KeyContactsFsgGrade6Email },
            ProjectManager = new Contact() { Name = _project.KeyContactsEsfaCapitalProjectManager, Email = _project.KeyContactsEsfaCapitalProjectManagerEmail },
            ProjectDirector = new Contact() { Name = _project.KeyContactsEsfaCapitalProjectDirector, Email = _project.KeyContactsEsfaCapitalProjectDirectorEmail },
            TrustContact = new Contact() { Name = _project.KeyContactsChairOfGovernorsMat, Email = _project.KeyContactsChairOfGovernorsMatEmail, PhoneNumber = _project.KeyContactsChairOfGovernorsMatPhone, Role = _project.KeyContactsChairOfGovernorsMatRole },
            OfstedContact = new Contact() { Name = _project.KeyContactsOfstedContact, Email = _project.KeyContactsOfstedContactEmail, PhoneNumber = _project.KeyContactsOfstedContactPhone, Role = _project.KeyContactsOfstedContactRole },
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
                ProjectAssignedTo = new Contact() { Name = Fixture.Create<string>(), Email = Fixture.Create<string>() },
                TeamLead = new Contact() { Name = Fixture.Create<string>(), Email = Fixture.Create<string>() },
                TrustContact= new Contact() { Name = Fixture.Create<string>(), Email = Fixture.Create<string>(), PhoneNumber = Fixture.Create<string>() },
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

            ProjectAssignedTo = new Contact() { Name = contactToUpdate.Contacts.ProjectAssignedTo.Name, Email = contactToUpdate.Contacts.ProjectAssignedTo.Email },
            TeamLead = new Contact() { Name = contactToUpdate.Contacts.TeamLead.Name, Email = contactToUpdate.Contacts.TeamLead.Email },
            TrustContact = new Contact() { Name = contactToUpdate.Contacts.TrustContact.Name, Email = contactToUpdate.Contacts.TrustContact.Email, PhoneNumber = contactToUpdate.Contacts.TrustContact.PhoneNumber },
            Grade6 = new Contact() { Name = _project.KeyContactsFsgGrade6, Email = _project.KeyContactsFsgGrade6Email },
            ProjectManager = new Contact() { Name = _project.KeyContactsEsfaCapitalProjectManager, Email = _project.KeyContactsEsfaCapitalProjectManagerEmail },
            ProjectDirector = new Contact() { Name = _project.KeyContactsEsfaCapitalProjectDirector, Email = _project.KeyContactsEsfaCapitalProjectDirectorEmail },
            OfstedContact = new Contact() { Name = _project.KeyContactsOfstedContact, Email = _project.KeyContactsOfstedContactEmail, PhoneNumber = _project.KeyContactsOfstedContactPhone, Role = _project.KeyContactsOfstedContactRole },

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