using System.Collections;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;
using NotFoundException = Dfe.ManageFreeSchoolProjects.API.Exceptions.NotFoundException;


namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.GrantLetters;

public interface IProjectGrantLettersService
{
    Task<ProjectGrantLetters> Get(string projectId);
    Task Update(string projectId, PdgGrantLetters updatedPdgGrantLetters);
}

public class ProjectGrantLettersService(MfspContext context) : IProjectGrantLettersService
{
    public async Task<ProjectGrantLetters> Get(string projectId)
    {
        var dbProject = await context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);
        if (dbProject == null)
            throw new NotFoundException($"Project with id {projectId} not found");

        var baseQuery = context.Kpi.Where(kpi => kpi.ProjectStatusProjectId == projectId);

        var result = await (from kpi in baseQuery
            join po in context.Po on kpi.Rid equals po.Rid into joinedPO
            from po in joinedPO.DefaultIfEmpty()
            select MapToGrantLetters(po)).FirstOrDefaultAsync();
        
        var lettersWithLinkButNotSavedToWorkspaces =
            result.Letters.Where(x =>
                !string.IsNullOrEmpty(x.LetterLink) && x.SavedToWorkplacesFolder != null &&
                (bool) x.SavedToWorkplacesFolder == false).ToList();

        lettersWithLinkButNotSavedToWorkspaces.ForEach(x => x.SavedToWorkplacesFolder = true);

        return result;
    }

    public async Task Update(string projectId, PdgGrantLetters updatedPdgGrantLetters)
    {
        var dbProject = await context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        if (dbProject == null)
        {
            throw new NotFoundException($"Project with id {projectId} not found");
        }

        var po = await context.Po.FirstOrDefaultAsync(p => p.Rid == dbProject.Rid);

        UpdateGrantLetters(po, updatedPdgGrantLetters);

        await context.SaveChangesAsync();
    }

    private static void UpdateGrantLetters(Po po, PdgGrantLetters updatedGrantLetters)
    {
        po.ProjectDevelopmentGrantFundingPdgGrantLetterDate = updatedGrantLetters.PdgGrantLetterDate;
        po.ProjectDevelopmentGrantFundingPdgGrantLetterLink = updatedGrantLetters.PdgGrantLetterLink;
        po.ProjectDevelopmentGrantFunding1stPdgGrantVariationDate = updatedGrantLetters.FirstPdgGrantVariationDate;
        po.ProjectDevelopmentGrantFunding1stPdgGrantVariationLink = updatedGrantLetters.FirstPdgGrantVariationLink;
        po.ProjectDevelopmentGrantFunding2ndPdgGrantVariationDate = updatedGrantLetters.SecondPdgGrantVariationDate;
        po.ProjectDevelopmentGrantFunding2ndPdgGrantVariationLink = updatedGrantLetters.SecondPdgGrantVariationLink;
        po.ProjectDevelopmentGrantFunding3rdPdgGrantVariationDate = updatedGrantLetters.ThirdPdgGrantVariationDate;
        po.ProjectDevelopmentGrantFunding3rdPdgGrantVariationLink = updatedGrantLetters.ThirdPdgGrantVariationLink;
        po.ProjectDevelopmentGrantFunding4thPdgGrantVariationDate = updatedGrantLetters.FourthPdgGrantVariationDate;
        po.ProjectDevelopmentGrantFunding4thPdgGrantVariationLink = updatedGrantLetters.FourthPdgGrantVariationLink;
    }

    private static ProjectGrantLetters MapToGrantLetters(Po po)
    {
        return new ProjectGrantLetters
        {
            Letters =
            [
                new GrantLetter
                {
                    Type = GrantLetter.LetterType.Initial,
                    LetterDate = po?.ProjectDevelopmentGrantFundingPdgGrantLetterDate,
                    LetterLink = po?.ProjectDevelopmentGrantFundingPdgGrantLetterLink,
                    SavedToWorkplacesFolder = po?.PdgGrantLetterLinkSavedToWorkplaces
                },
                new GrantLetter
                {
                    Type = GrantLetter.LetterType.FirstVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding1stPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding1stPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgFirstVariationGrantLetterSavedToWorkplaces
                },
                new GrantLetter
                {
                    Type = GrantLetter.LetterType.SecondVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding2ndPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding2ndPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgSecondVariationGrantLetterSavedToWorkplaces
                },
                new GrantLetter
                {
                    Type = GrantLetter.LetterType.ThirdVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding3rdPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding3rdPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgThirdVariationGrantLetterSavedToWorkplaces
                },
                new GrantLetter
                {
                    Type = GrantLetter.LetterType.FourthVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding4thPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding4thPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgFourthVariationGrantLetterSavedToWorkplaces
                }
            ]
        };
    }
}