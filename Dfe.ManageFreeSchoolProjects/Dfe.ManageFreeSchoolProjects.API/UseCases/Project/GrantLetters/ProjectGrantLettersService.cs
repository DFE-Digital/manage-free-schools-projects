using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;
using NotFoundException = Dfe.ManageFreeSchoolProjects.API.Exceptions.NotFoundException;


namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.GrantLetters;

public interface IProjectGrantLettersService
{
    Task<PdgGrantLetters> Get(string projectId);
    Task Update(string projectId, PdgGrantLetters updatedPdgGrantLetters);
}

public class ProjectGrantLettersService(MfspContext context) : IProjectGrantLettersService
{
    public async Task<PdgGrantLetters> Get(string projectId)
    {
        var dbProject = await context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);
        if (dbProject == null)
            throw new NotFoundException($"Project with id {projectId} not found");

        var baseQuery = context.Kpi.Where(kpi => kpi.ProjectStatusProjectId == projectId);

        var result = await (from kpi in baseQuery
            join po in context.Po on kpi.Rid equals po.Rid into joinedPO
            from po in joinedPO.DefaultIfEmpty()
            select MapToGrantLetters(kpi, po)).FirstOrDefaultAsync();

        return result ?? new PdgGrantLetters();
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

    private static PdgGrantLetters MapToGrantLetters(Kpi kpi, Po po)
    {
        return new PdgGrantLetters
        {
            PdgGrantLetterDate = po?.ProjectDevelopmentGrantFundingPdgGrantLetterDate,
            PdgGrantLetterLink = po?.ProjectDevelopmentGrantFundingPdgGrantLetterLink,
            FirstPdgGrantVariationDate = po?.ProjectDevelopmentGrantFunding1stPdgGrantVariationDate,
            FirstPdgGrantVariationLink = po?.ProjectDevelopmentGrantFunding1stPdgGrantVariationLink,
            SecondPdgGrantVariationDate = po?.ProjectDevelopmentGrantFunding2ndPdgGrantVariationDate,
            SecondPdgGrantVariationLink = po?.ProjectDevelopmentGrantFunding2ndPdgGrantVariationLink,
            ThirdPdgGrantVariationDate = po?.ProjectDevelopmentGrantFunding3rdPdgGrantVariationDate,
            ThirdPdgGrantVariationLink = po?.ProjectDevelopmentGrantFunding3rdPdgGrantVariationLink,
            FourthPdgGrantVariationDate = po?.ProjectDevelopmentGrantFunding4thPdgGrantVariationDate,
            FourthPdgGrantVariationLink = po?.ProjectDevelopmentGrantFunding4thPdgGrantVariationLink
        };
    }
}