using System.Net;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Xunit.Abstractions;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration;

[Collection(ApiTestCollection.ApiTestCollectionName)]
public class EmailApiTest : ApiTestsBase
{
    private readonly ITestOutputHelper _testOutputHelper;

    public EmailApiTest(ApiTestFixture apiTestFixture, ITestOutputHelper testOutputHelper) : base(apiTestFixture)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task When_Email_Empty_Or_Null_Returns_Status400_With_ErrorMsg()
    {
        var result = await _client.PostAsync("/api/v1/email?email=", null);
        var responseMessage = await result.Content.ReadAsStringAsync();
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Be("Email is required.");
    } 
    
    [Theory]
    [InlineData("@invalidemail.com")]
    [InlineData("invalid@")]
    public async Task When_Email_Is_Invalid_Returns_Status400_With_ErrorMsg(string email)
    {
        var result = await _client.PostAsync($"/api/v1/email?email={email}", null);
        var responseMessage = await result.Content.ReadAsStringAsync();
        
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Be("Email is not valid.");
    } 
    
    [Fact]
    public async Task When_Email_Valid_And_Sent_Returns_Ok()
    {
        var result = await _client.PostAsync($"/api/v1/email?email=test%40education.gov.uk", null);
        
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    } 
}