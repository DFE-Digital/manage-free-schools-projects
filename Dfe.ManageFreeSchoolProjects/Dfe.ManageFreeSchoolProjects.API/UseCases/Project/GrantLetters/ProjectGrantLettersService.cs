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
    Task Update(string projectId, GrantLetter updatedOrNewGrantLetter);
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

        return result;
    }

    public async Task Update(string projectId, GrantLetter updatedPdgGrantLetters)
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

    private static void UpdateGrantLetters(Po po, GrantLetter newOrUpdatedGrantLetter)
    {
       //TODO: Find out if we need separate DB fields for Initial & Full letter dates
        if (newOrUpdatedGrantLetter.Type is GrantLetter.LetterType.Initial or GrantLetter.LetterType.Full)
        {
            po.ProjectDevelopmentGrantFundingPdgGrantLetterDate = newOrUpdatedGrantLetter.InitialGrantLetterDate ?? newOrUpdatedGrantLetter.FinalGrantLetterDate;
            po.ProjectDevelopmentGrantFundingPdgGrantLetterLink = newOrUpdatedGrantLetter.LetterLink;
        }

        switch (newOrUpdatedGrantLetter.Variation)
        {
            // Update variation fields based on the variation number
            case 1:
                po.ProjectDevelopmentGrantFunding1stPdgGrantVariationDate = newOrUpdatedGrantLetter.VariationLetterDate;
                po.ProjectDevelopmentGrantFunding1stPdgGrantVariationLink = newOrUpdatedGrantLetter.LetterLink;
                break;
            case 2:
                po.ProjectDevelopmentGrantFunding2ndPdgGrantVariationDate = newOrUpdatedGrantLetter.VariationLetterDate;
                po.ProjectDevelopmentGrantFunding2ndPdgGrantVariationLink = newOrUpdatedGrantLetter.LetterLink;
                break;
            case 3:
                po.ProjectDevelopmentGrantFunding3rdPdgGrantVariationDate = newOrUpdatedGrantLetter.VariationLetterDate;
                po.ProjectDevelopmentGrantFunding3rdPdgGrantVariationLink = newOrUpdatedGrantLetter.LetterLink;
                break;
            case 4:
                po.ProjectDevelopmentGrantFunding4thPdgGrantVariationDate = newOrUpdatedGrantLetter.VariationLetterDate;
                po.ProjectDevelopmentGrantFunding4thPdgGrantVariationLink = newOrUpdatedGrantLetter.LetterLink;
                break;
        }
    }

    private static ProjectGrantLetters MapToGrantLetters(Po po)
    {
        return new ProjectGrantLetters
        {
            Letters =
            [
                new GrantLetter
                {
                    //TODO: Do we need to change this to a Full letter when Full letter date has value
                    Type = GrantLetter.LetterType.Initial,
                    InitialGrantLetterDate = po?.ProjectDevelopmentGrantFundingPdgGrantLetterDate,
                    FinalGrantLetterDate = po?.ProjectDevelopmentGrantFundingPdgGrantLetterDate,
                    LetterLink = po?.ProjectDevelopmentGrantFundingPdgGrantLetterLink,
                    SavedToWorkplacesFolder = po?.PdgGrantLetterLinkSavedToWorkplaces
                },
                new GrantLetter
                {
                    Type = GrantLetter.LetterType.FirstVariation,
                    VariationLetterDate = po?.ProjectDevelopmentGrantFunding1stPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding1stPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgFirstVariationGrantLetterSavedToWorkplaces
                },
                new GrantLetter
                {
                    Type = GrantLetter.LetterType.SecondVariation,
                    VariationLetterDate = po?.ProjectDevelopmentGrantFunding2ndPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding2ndPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgSecondVariationGrantLetterSavedToWorkplaces
                },
                new GrantLetter
                {
                    Type = GrantLetter.LetterType.ThirdVariation,
                    VariationLetterDate = po?.ProjectDevelopmentGrantFunding3rdPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding3rdPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgThirdVariationGrantLetterSavedToWorkplaces
                },
                new GrantLetter
                {
                    Type = GrantLetter.LetterType.FourthVariation,
                    VariationLetterDate = po?.ProjectDevelopmentGrantFunding4thPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding4thPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgFourthVariationGrantLetterSavedToWorkplaces
                }
            ]
        };
    }
}