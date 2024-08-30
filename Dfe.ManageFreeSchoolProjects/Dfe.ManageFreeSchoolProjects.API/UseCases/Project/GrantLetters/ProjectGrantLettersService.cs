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
    Task Update(string projectId, VariationGrantLetter updatedOrNewVariationGrantLetter);
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

    public async Task Update(string projectId, VariationGrantLetter updatedPdgVariationGrantLetters)
    {
        var dbProject = await context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        if (dbProject == null)
        {
            throw new NotFoundException($"Project with id {projectId} not found");
        }

        var po = await context.Po.FirstOrDefaultAsync(p => p.Rid == dbProject.Rid);

        UpdateGrantLetters(po, updatedPdgVariationGrantLetters);

        await context.SaveChangesAsync();
    }

    private static void UpdateGrantLetters(Po po, VariationGrantLetter newOrUpdatedVariationGrantLetter)
    {
       //TODO: Find out if we need separate DB fields for Initial & Full letter dates
        if (newOrUpdatedVariationGrantLetter.LetterVariation is VariationGrantLetter.GrantLetterVariation.Initial or VariationGrantLetter.GrantLetterVariation.Full)
        {
            po.ProjectDevelopmentGrantFundingPdgGrantLetterDate = newOrUpdatedVariationGrantLetter.InitialGrantLetterDate ?? newOrUpdatedVariationGrantLetter.FinalGrantLetterDate;
            po.ProjectDevelopmentGrantFundingPdgGrantLetterLink = newOrUpdatedVariationGrantLetter.LetterLink;
        }

        switch (newOrUpdatedVariationGrantLetter.GrantLetterVariation)
        {
            // Update variation fields based on the variation number
            case 1:
                po.ProjectDevelopmentGrantFunding1stPdgGrantVariationDate = newOrUpdatedVariationGrantLetter.LetterDate;
                po.ProjectDevelopmentGrantFunding1stPdgGrantVariationLink = newOrUpdatedVariationGrantLetter.LetterLink;
                break;
            case 2:
                po.ProjectDevelopmentGrantFunding2ndPdgGrantVariationDate = newOrUpdatedVariationGrantLetter.LetterDate;
                po.ProjectDevelopmentGrantFunding2ndPdgGrantVariationLink = newOrUpdatedVariationGrantLetter.LetterLink;
                break;
            case 3:
                po.ProjectDevelopmentGrantFunding3rdPdgGrantVariationDate = newOrUpdatedVariationGrantLetter.LetterDate;
                po.ProjectDevelopmentGrantFunding3rdPdgGrantVariationLink = newOrUpdatedVariationGrantLetter.LetterLink;
                break;
            case 4:
                po.ProjectDevelopmentGrantFunding4thPdgGrantVariationDate = newOrUpdatedVariationGrantLetter.LetterDate;
                po.ProjectDevelopmentGrantFunding4thPdgGrantVariationLink = newOrUpdatedVariationGrantLetter.LetterLink;
                break;
        }
    }

    private static ProjectGrantLetters MapToGrantLetters(Po po)
    {
        return new ProjectGrantLetters
        {
           
            InitialGrantLetterDate = po?.ProjectDevelopmentGrantFundingPdgGrantLetterDate,
            FinalGrantLetterDate = po?.ProjectDevelopmentGrantFundingPdgGrantLetterDate,
            
            //TODO: do we need separate SavedToWorkspaces fields for Initial & Final letters?
            InitialGrantLetterSavedToWorkplaces = po?.PdgGrantLetterLinkSavedToWorkplaces,
            FinalGrantLetterSavedToWorkplaces = po?.PdgGrantLetterLinkSavedToWorkplaces,
            
            VariationLetters =
            [
               
                new VariationGrantLetter
                {
                    Variation = VariationGrantLetter.GrantLetterVariation.FirstVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding1stPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding1stPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgFirstVariationGrantLetterSavedToWorkplaces
                },
                new VariationGrantLetter
                {
                    Variation = VariationGrantLetter.GrantLetterVariation.SecondVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding2ndPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding2ndPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgSecondVariationGrantLetterSavedToWorkplaces
                },
                new VariationGrantLetter
                {
                    Variation = VariationGrantLetter.GrantLetterVariation.ThirdVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding3rdPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding3rdPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgThirdVariationGrantLetterSavedToWorkplaces
                },
                new VariationGrantLetter
                {
                    Variation = VariationGrantLetter.GrantLetterVariation.FourthVariation,
                    LetterDate = po?.ProjectDevelopmentGrantFunding4thPdgGrantVariationDate,
                    LetterLink = po?.ProjectDevelopmentGrantFunding4thPdgGrantVariationLink,
                    SavedToWorkplacesFolder = po?.PdgFourthVariationGrantLetterSavedToWorkplaces
                }
            ]
        };
    }
}