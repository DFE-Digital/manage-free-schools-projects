using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;

public interface IContactsService
{
    Task<GetContactsByRefResponse> Execute(string projectId);
}

public class GetContactsService : IContactsService
{
    private readonly MfspContext _context;

    public GetContactsService(MfspContext context)
    {
        _context = context;
    }

    public async Task<GetContactsByRefResponse> Execute(string projectId)
    {
        var dbProject = await _context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        if (dbProject == null)
        {
            throw new NotFoundException($"Project with id {projectId} not found");
        }
    }
}