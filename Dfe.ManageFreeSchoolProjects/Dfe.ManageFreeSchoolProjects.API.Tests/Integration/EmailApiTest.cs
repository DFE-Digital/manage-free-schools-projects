using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Newtonsoft.Json;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration;

[Collection(ApiTestCollection.ApiTestCollectionName)]
public class EmailApiTest : ApiTestsBase
{
    public EmailApiTest(ApiTestFixture apiTestFixture) : base(apiTestFixture)
    {
    }
    
    [Fact]
    public async Task When_Email_Empty_Or_Null_Returns_Status400_With_ErrorMsg()
    {
        var emptyString = ConvertToJson(string.Empty);
        
        var result = await _client.PostAsync("/api/v1/email", emptyString);
        var responseMessage = await result.Content.ReadAsStringAsync();
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("Email is required.");
    } 
    
    [Theory]
    [InlineData("@invalidemail.com")]
    [InlineData("invalid@")]
    public async Task When_Email_Is_Invalid_Returns_Status400_With_ErrorMsg(string email)
    {
        var result = await _client.PostAsync($"/api/v1/email", ConvertToJson(email));
        var responseMessage = await result.Content.ReadAsStringAsync();
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Be("Email is not valid.");
    } 
    
    [Fact]
    public async Task When_Email_Valid_And_Sent_Returns_Ok()
    {
        var result = await _client.PostAsync($"/api/v1/email", ConvertToJson("test@education.gov.uk"));
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    } 
    
    private static StringContent ConvertToJson(string email)
    {
        var body = JsonConvert.SerializeObject(email);

        return new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json);
    }
}