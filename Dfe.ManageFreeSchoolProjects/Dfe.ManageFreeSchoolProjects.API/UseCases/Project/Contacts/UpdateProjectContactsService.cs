using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Contacts;

public interface IUpdateProjectContactsService
{
    Task Execute(string projectId , UpdateContactsRequest request);
}

public class UpdateProjectContactsService : IUpdateProjectContactsService
{
    private readonly MfspContext _context;

    public UpdateProjectContactsService(MfspContext context)
    {
        _context = context;
    }

    public async Task Execute(string projectId, UpdateContactsRequest request)
    {
        var dbProject = await _context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        if (dbProject == null)
        {
            throw new NotFoundException($"Project with id {projectId} not found");
        }

        var updateRequest = new UpdateContactsRequest()
        {
            Contacts = request.Contacts
        };

        dbProject.KeyContactsFsgLeadContact = updateRequest.Contacts.ProjectManagedByName ?? dbProject.KeyContactsFsgLeadContact;
        dbProject.KeyContactsFsgLeadContactEmail = updateRequest.Contacts.ProjectManagedByEmail ?? dbProject.KeyContactsFsgLeadContactEmail;

        dbProject.KeyContactsFsgTeamLeader = updateRequest.Contacts.TeamLeadName ?? dbProject.KeyContactsFsgTeamLeader;
        dbProject.KeyContactsFsgTeamLeaderEmail = updateRequest.Contacts.TeamLeadEmail ?? dbProject.KeyContactsFsgTeamLeaderEmail;

        dbProject.KeyContactsFsgGrade6 = updateRequest.Contacts.Grade6Name ?? dbProject.KeyContactsFsgGrade6;
        dbProject.KeyContactsFsgGrade6Email = updateRequest.Contacts.Grade6Email ?? dbProject.KeyContactsFsgGrade6Email;

        dbProject.KeyContactsEsfaCapitalProjectDirector = updateRequest.Contacts.ProjectDirectorName ?? dbProject.KeyContactsEsfaCapitalProjectDirector;
        dbProject.KeyContactsEsfaCapitalProjectDirectorEmail = updateRequest.Contacts.ProjectDirectorEmail ?? dbProject.KeyContactsEsfaCapitalProjectDirectorEmail;

        dbProject.KeyContactsChairOfGovernorsName = updateRequest.Contacts.TrustChairName ?? dbProject.KeyContactsChairOfGovernorsName;
        dbProject.KeyContactsChairOfGovernorsEmail = updateRequest.Contacts.TrustChairEmail ?? dbProject.KeyContactsChairOfGovernorsEmail;

        dbProject.KeyContactsChairOfGovernorsMat = updateRequest.Contacts.SchoolChairName ?? dbProject.KeyContactsChairOfGovernorsMat;
        dbProject.KeyContactsChairOfGovernorsMatEmail = updateRequest.Contacts.SchoolChairEmail ?? dbProject.KeyContactsChairOfGovernorsMatEmail;
        
        await _context.SaveChangesAsync();
    }
    
}