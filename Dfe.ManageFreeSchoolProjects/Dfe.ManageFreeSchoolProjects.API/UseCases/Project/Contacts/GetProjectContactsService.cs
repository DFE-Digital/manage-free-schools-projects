
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Contacts;

public interface IGetProjectContactsService
{
    Task<GetContactsResponse> Execute(string projectId);
}

public class GetProjectContactsService : IGetProjectContactsService
{
    private readonly MfspContext _context;

    public GetProjectContactsService(MfspContext context)
    {
        _context = context;
    }

    public async Task<GetContactsResponse> Execute(string projectId)
    {
        var dbProject = await _context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        if (dbProject == null)
        {
            throw new NotFoundException($"Project with id {projectId} not found");
        }
        
        var result = new GetContactsResponse()
        {
           Contacts = new ContactsTask()
           {
                ProjectAssignedTo = new Contact() { Name = dbProject.KeyContactsFsgLeadContact, Email = dbProject.KeyContactsFsgLeadContactEmail },
                TeamLead = new Contact() { Name = dbProject.KeyContactsFsgTeamLeader, Email = dbProject.KeyContactsFsgTeamLeaderEmail },
                Grade6 = new Contact() { Name = dbProject.KeyContactsFsgGrade6, Email = dbProject.KeyContactsFsgGrade6Email },
                ProjectManager = new Contact() { Name = dbProject.KeyContactsEsfaCapitalProjectManager, Email = dbProject.KeyContactsEsfaCapitalProjectManagerEmail },
                ProjectDirector = new Contact() { Name = dbProject.KeyContactsEsfaCapitalProjectDirector, Email = dbProject.KeyContactsEsfaCapitalProjectDirectorEmail },
                TrustContact = new Contact() { Name = dbProject.KeyContactsChairOfGovernorsMat, Email = dbProject.KeyContactsChairOfGovernorsMatEmail, PhoneNumber = dbProject.KeyContactsChairOfGovernorsMatPhone },
           }
        };

        return result;
       
    }
}