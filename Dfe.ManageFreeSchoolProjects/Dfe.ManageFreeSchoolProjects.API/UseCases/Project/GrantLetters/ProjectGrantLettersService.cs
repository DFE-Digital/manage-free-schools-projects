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
    Task UpdateGrantLetter(string projectId, ProjectGrantLetters updatedOrNewGrantLetter);
    Task UpdateVariationLetter(string projectId, GrantVariationLetter updatedOrNewVariationLetter);
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

        if (result.InitialGrantLetterDate != null && !string.IsNullOrEmpty(result.GrantLetterLink))
            result.InitialGrantLetterSavedToWorkplaces = true;

        if (result.FinalGrantLetterDate != null && !string.IsNullOrEmpty(result.GrantLetterLink))
            result.FinalGrantLetterSavedToWorkplaces = true;
        
        var lettersWithLinkAndDateNotSaved =
            result.VariationLetters.Where(x =>
                !string.IsNullOrEmpty(x.LetterLink) && x.LetterDate != null
                                                    && x.SavedToWorkplacesFolder != null
                                                    && (bool)x.SavedToWorkplacesFolder == false).ToList();

        lettersWithLinkAndDateNotSaved.ForEach(x => x.SavedToWorkplacesFolder = true);

        return result;
    }

    public async Task UpdateGrantLetter(string projectId, ProjectGrantLetters updatedOrNewGrantLetter)
    {
        var dbProject = await context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        if (dbProject == null)
        {
            throw new NotFoundException($"Project with id {projectId} not found");
        }

        var po = await context.Po.FirstOrDefaultAsync(p => p.Rid == dbProject.Rid);

        UpdateGrantLetters(po, updatedOrNewGrantLetter);

        await context.SaveChangesAsync();
    }

    public async Task UpdateVariationLetter(string projectId, GrantVariationLetter updatedOrNewVariationLetter)
    {
        var dbProject = await context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        if (dbProject == null)
        {
            throw new NotFoundException($"Project with id {projectId} not found");
        }

        var po = await context.Po.FirstOrDefaultAsync(p => p.Rid == dbProject.Rid);

        UpdateVariationLetter(po, updatedOrNewVariationLetter);

        await context.SaveChangesAsync();
    }

    private static void UpdateVariationLetter(Po po, GrantVariationLetter variationLetter)
    {
        switch (variationLetter.Variation)
        {
            // Update variation fields based on the variation number
            case GrantVariationLetter.LetterVariation.FirstVariation:
                po.ProjectDevelopmentGrantFunding1stPdgGrantVariationDate = variationLetter.LetterDate;
                po.PdgFirstVariationGrantLetterSavedToWorkplaces = variationLetter.SavedToWorkplacesFolder;
                break;
            case GrantVariationLetter.LetterVariation.SecondVariation:
                po.ProjectDevelopmentGrantFunding2ndPdgGrantVariationDate = variationLetter.LetterDate;
                po.PdgSecondVariationGrantLetterSavedToWorkplaces = variationLetter.SavedToWorkplacesFolder;
                break;
            case GrantVariationLetter.LetterVariation.ThirdVariation:
                po.ProjectDevelopmentGrantFunding3rdPdgGrantVariationDate = variationLetter.LetterDate;
                po.PdgThirdVariationGrantLetterSavedToWorkplaces = variationLetter.SavedToWorkplacesFolder;
                break;
            case GrantVariationLetter.LetterVariation.FourthVariation:
                po.ProjectDevelopmentGrantFunding4thPdgGrantVariationDate = variationLetter.LetterDate;
                po.PdgFourthVariationGrantLetterSavedToWorkplaces = variationLetter.SavedToWorkplacesFolder;
                break;
            case GrantVariationLetter.LetterVariation.NotSet:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void UpdateGrantLetters(Po po, ProjectGrantLetters newOrUpdatedGrantLetter)
    {
        po.PdgInitialGrantLetterDate = newOrUpdatedGrantLetter.InitialGrantLetterDate;
        po.PdgInitialGrantLetterSavedToWorkplaces = newOrUpdatedGrantLetter.InitialGrantLetterSavedToWorkplaces;
        
        po.ProjectDevelopmentGrantFundingPdgGrantLetterDate = newOrUpdatedGrantLetter.FinalGrantLetterDate;
        po.PdgGrantLetterLinkSavedToWorkplaces = newOrUpdatedGrantLetter.FinalGrantLetterSavedToWorkplaces;
    }

    private static ProjectGrantLetters MapToGrantLetters(Po po)
    {
        return new ProjectGrantLetters
        {
            InitialGrantLetterDate = po?.ProjectDevelopmentGrantFundingPdgGrantLetterDate,
            FinalGrantLetterDate = po?.ProjectDevelopmentGrantFundingPdgGrantLetterDate,
            GrantLetterLink = po?.ProjectDevelopmentGrantFundingPdgGrantLetterLink,
            InitialGrantLetterSavedToWorkplaces = po?.PdgGrantLetterLinkSavedToWorkplaces,
            FinalGrantLetterSavedToWorkplaces = po?.PdgGrantLetterLinkSavedToWorkplaces,

            VariationLetters =
            [
                new GrantVariationLetter
                {
                    Variation = GrantVariationLetter.LetterVariation.FirstVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding1stPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding1stPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgFirstVariationGrantLetterSavedToWorkplaces
                },
                new GrantVariationLetter
                {
                    Variation = GrantVariationLetter.LetterVariation.SecondVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding2ndPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding2ndPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgSecondVariationGrantLetterSavedToWorkplaces
                },
                new GrantVariationLetter
                {
                    Variation = GrantVariationLetter.LetterVariation.ThirdVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding3rdPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding3rdPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgThirdVariationGrantLetterSavedToWorkplaces
                },
                new GrantVariationLetter
                {
                    Variation = GrantVariationLetter.LetterVariation.FourthVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding4thPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding4thPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgFourthVariationGrantLetterSavedToWorkplaces
                }
            ]
        };
    }
}