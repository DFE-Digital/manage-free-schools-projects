using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;
using NotFoundException = Dfe.ManageFreeSchoolProjects.API.Exceptions.NotFoundException;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;


namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.GrantLetters;

public interface IProjectGrantLettersService
{
    Task<Contracts.Project.Grants.GrantLetters> Get(string projectId);
    Task Update(string projectId, Contracts.Project.Grants.GrantLetters updatedGrantLetters);
}

public class ProjectGrantLettersService(MfspContext context) : IProjectGrantLettersService
{
    public async Task<Contracts.Project.Grants.GrantLetters> Get(string projectId)
    {
        var dbProject = await context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);
        if (dbProject == null)
            throw new NotFoundException($"Project with id {projectId} not found");

        var baseQuery = context.Kpi.Where(kpi => kpi.ProjectStatusProjectId == projectId);

        var result = await (from kpi in baseQuery
            join po in context.Po on kpi.Rid equals po.Rid into joinedPO
            from po in joinedPO.DefaultIfEmpty()
            select MapToGrantLetters(kpi, po)).FirstOrDefaultAsync();

        return result ?? new Contracts.Project.Grants.GrantLetters();
    }

    public Task Update(string projectId, Contracts.Project.Grants.GrantLetters updatedGrantLetters)
    {
        throw new NotImplementedException();
    }

    private static Contracts.Project.Grants.GrantLetters MapToGrantLetters(Kpi kpi, Po po)
    {
        return new Contracts.Project.Grants.GrantLetters
        {
            GrantLetterDate = po?.ProjectDevelopmentGrantFundingPdgGrantLetterDate,
            GrantLetterLink = po?.ProjectDevelopmentGrantFundingPdgGrantLetterLink,
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