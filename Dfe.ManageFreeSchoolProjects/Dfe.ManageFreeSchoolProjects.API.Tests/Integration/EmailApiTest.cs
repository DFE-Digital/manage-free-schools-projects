using System.Net;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration;

[Collection(ApiTestCollection.ApiTestCollectionName)]
public class EmailApiTest : ApiTestsBase
{
    public EmailApiTest(ApiTestFixture apiTestFixture) : base(apiTestFixture)
    {
    }
    
    [Fact(Skip = "Will be used when required.")]
    public async Task When_Email_Empty_Or_Null_Returns_Status400_With_ErrorMsg()
    {
        var result = await _client.PostAsync("/api/v1/email?email=", null);
        var responseMessage = await result.Content.ReadAsStringAsync();
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Be("Email is required.");
    } 
    
    [Theory(Skip = "Will be used when required.")]
    [InlineData("@invalidemail.com")]
    [InlineData("invalid@")]
    public async Task When_Email_Is_Invalid_Returns_Status400_With_ErrorMsg(string email)
    {
        var result = await _client.PostAsync($"/api/v1/email?email={email}", null);
        var responseMessage = await result.Content.ReadAsStringAsync();
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Be("Email is not valid.");
    } 
    
    [Fact(Skip = "Will be used when required.")]
    public async Task When_Email_Valid_And_Sent_Returns_Ok()
    {
        //Note: Use Test api key as it mimics sending email
        var result = await _client.PostAsync($"/api/v1/email?email=test%40education.gov.uk", null);
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    } 
    
    [Fact(Skip = "Will be used when required.")]
    public async Task When_Error_Returned_From_GovNotify_Returns_Status500_With_ErrorMsg()
    {
        //Note: Use Test api key as it mimics sending email
        var result = await _client.PostAsync($"/api/v1/email?email=asdsadadad%40education.gov.uk", null);
        var responseMessage = await result.Content.ReadAsStringAsync();

        result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        responseMessage.Should().Be("Failed to send email. Check email exists/valid or Gov Notify service.");
    } 
}