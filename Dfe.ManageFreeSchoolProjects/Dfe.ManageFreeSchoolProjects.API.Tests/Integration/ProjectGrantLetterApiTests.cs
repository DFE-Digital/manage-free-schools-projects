using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.API.Tests.Fixtures;
using Dfe.ManageFreeSchoolProjects.API.Tests.Helpers;

namespace Dfe.ManageFreeSchoolProjects.API.Tests.Integration;

[Collection(ApiTestCollection.ApiTestCollectionName)]
public class ProjectGrantLetterApiTests(ApiTestFixture apiTestFixture) : ApiTestsBase(apiTestFixture)
{
    private static readonly Fixture Fixture = new();


    [Fact]
    public async Task Get_ProjectGrantLetters_Returns_200()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        var grantLetters = DatabaseModelBuilder.BuildProjectGrantLetters(project.Rid);
        context.Po.Add(grantLetters);

        await context.SaveChangesAsync();

        var getProjectGrantLettersResponse =
            await _client.GetAsync($"/api/v1.0/client/projects/{projectId}/grant-letters");
        getProjectGrantLettersResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var grantLetterData = await getProjectGrantLettersResponse.Content
            .ReadResponseFromWrapper<ProjectGrantLetters>();
        
        grantLetterData.InitialGrantLetterDate.Should().Be(grantLetters.PdgInitialGrantLetterDate);
        grantLetterData.InitialGrantLetterSavedToWorkplaces.Should()
            .Be(grantLetters.PdgInitialGrantLetterSavedToWorkplaces);
        grantLetterData.FinalGrantLetterDate.Should().Be(grantLetters.PdgInitialGrantLetterDate);
        grantLetterData.FinalGrantLetterSavedToWorkplaces.Should().Be(grantLetters.PdgGrantLetterLinkSavedToWorkplaces);
    }

    [Fact]
    public async Task Get_GrantLetters_WithSavedToWorkplacesFlagFalse_Returns_FlagsAsTrue_And_200()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        var grantLetters = DatabaseModelBuilder.BuildProjectGrantLetters(project.Rid);

        grantLetters.PdgInitialGrantLetterSavedToWorkplaces = false;
        grantLetters.PdgGrantLetterLinkSavedToWorkplaces = false;

        context.Po.Add(grantLetters);

        await context.SaveChangesAsync();

        var getProjectGrantLettersResponse =
            await _client.GetAsync($"/api/v1.0/client/projects/{projectId}/grant-letters");
        getProjectGrantLettersResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var grantLetterData = await getProjectGrantLettersResponse.Content
            .ReadResponseFromWrapper<ProjectGrantLetters>();
        
        grantLetterData.InitialGrantLetterSavedToWorkplaces.Should().Be(true);
        grantLetterData.FinalGrantLetterSavedToWorkplaces.Should().Be(true);
    }

    [Fact]
    public async Task Get_GrantLetters_WithVariationLetters_Returns_VariationLetters_200()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        //With variation letters
        var grantLetters = DatabaseModelBuilder.BuildProjectGrantLetters(project.Rid, withVariationLetters: true);

        context.Po.Add(grantLetters);

        await context.SaveChangesAsync();

        var getProjectGrantLettersResponse =
            await _client.GetAsync($"/api/v1.0/client/projects/{projectId}/grant-letters");
        getProjectGrantLettersResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var grantLetterData = await getProjectGrantLettersResponse.Content
            .ReadResponseFromWrapper<ProjectGrantLetters>();

        grantLetterData.InitialGrantLetterDate.Should().Be(grantLetters.PdgInitialGrantLetterDate);
        grantLetterData.InitialGrantLetterSavedToWorkplaces.Should()
            .Be(grantLetters.PdgInitialGrantLetterSavedToWorkplaces);
        grantLetterData.FinalGrantLetterDate.Should().Be(grantLetters.PdgInitialGrantLetterDate);
        grantLetterData.FinalGrantLetterSavedToWorkplaces.Should().Be(grantLetters.PdgGrantLetterLinkSavedToWorkplaces);

        var variationLettersData = grantLetterData.VariationLetters.ToList();

        var actual1stVariationLetter =
            variationLettersData.Single(x => x.Variation == GrantVariationLetter.LetterVariation.FirstVariation);
        actual1stVariationLetter.LetterDate.Should()
            .Be(grantLetters.ProjectDevelopmentGrantFunding1stPdgGrantVariationDate);
        actual1stVariationLetter.SavedToWorkplacesFolder.Should()
            .Be(grantLetters.PdgFirstVariationGrantLetterSavedToWorkplaces);
        
        var actual2ndVariationLetter =
            variationLettersData.Single(x => x.Variation == GrantVariationLetter.LetterVariation.SecondVariation);
        actual2ndVariationLetter.LetterDate.Should()
            .Be(grantLetters.ProjectDevelopmentGrantFunding2ndPdgGrantVariationDate);
        actual2ndVariationLetter.SavedToWorkplacesFolder.Should()
            .Be(grantLetters.PdgSecondVariationGrantLetterSavedToWorkplaces);

        var actual3rdVariationLetter =
            variationLettersData.Single(x => x.Variation == GrantVariationLetter.LetterVariation.ThirdVariation);
        actual3rdVariationLetter.LetterDate.Should()
            .Be(grantLetters.ProjectDevelopmentGrantFunding3rdPdgGrantVariationDate);
        actual3rdVariationLetter.SavedToWorkplacesFolder.Should()
            .Be(grantLetters.PdgThirdVariationGrantLetterSavedToWorkplaces);

        var actual4thVariationLetter =
            variationLettersData.Single(x => x.Variation == GrantVariationLetter.LetterVariation.FourthVariation);
        actual4thVariationLetter.LetterDate.Should()
            .Be(grantLetters.ProjectDevelopmentGrantFunding4thPdgGrantVariationDate);
        actual4thVariationLetter.SavedToWorkplacesFolder.Should()
            .Be(grantLetters.PdgFourthVariationGrantLetterSavedToWorkplaces);
    }

    [Fact]
    public async Task Update_GrantLetters_Return_204()
    {
        var project = DatabaseModelBuilder.BuildProject();
        var projectId = project.ProjectStatusProjectId;

        using var context = _testFixture.GetContext();
        context.Kpi.Add(project);

        var grantLetters = DatabaseModelBuilder.BuildProjectGrantLetters(project.Rid);
        context.Po.Add(grantLetters);

        await context.SaveChangesAsync();

        var request = new ProjectGrantLetters
        {
            InitialGrantLetterDate = DateTime.Today.AddDays(1),
            InitialGrantLetterSavedToWorkplaces = true,
            FinalGrantLetterDate = DateTime.Today.AddDays(1),
            FinalGrantLetterSavedToWorkplaces = true
        };
        
        var updateGrantLettersResponse =
            await _client.PutAsync($"/api/v1.0/client/projects/{projectId}/grant-letters", request.ConvertToJson());
        updateGrantLettersResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        var getProjectGrantLettersResponse =
            await _client.GetAsync($"/api/v1.0/client/projects/{projectId}/grant-letters");
        getProjectGrantLettersResponse.StatusCode.Should().Be(HttpStatusCode.OK); 
        
        var grantLetterData = await getProjectGrantLettersResponse.Content
            .ReadResponseFromWrapper<ProjectGrantLetters>();
        
        grantLetterData.InitialGrantLetterDate.Should().Be(request.InitialGrantLetterDate);
        grantLetterData.InitialGrantLetterSavedToWorkplaces.Should().Be(request.InitialGrantLetterSavedToWorkplaces);
        grantLetterData.FinalGrantLetterDate.Should().Be(request.FinalGrantLetterDate);
        grantLetterData.FinalGrantLetterSavedToWorkplaces.Should().Be(request.FinalGrantLetterSavedToWorkplaces);
    }
}