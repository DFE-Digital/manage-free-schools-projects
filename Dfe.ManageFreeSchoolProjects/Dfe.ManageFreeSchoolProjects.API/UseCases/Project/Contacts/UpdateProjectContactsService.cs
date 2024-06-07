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

        dbProject.KeyContactsFsgLeadContact = updateRequest.Contacts.ProjectAssignedTo.Name ?? dbProject.KeyContactsFsgLeadContact;
        dbProject.KeyContactsFsgLeadContactEmail = updateRequest.Contacts.ProjectAssignedTo.Email ?? dbProject.KeyContactsFsgLeadContactEmail;

        dbProject.KeyContactsFsgTeamLeader = updateRequest.Contacts.TeamLead.Name ?? dbProject.KeyContactsFsgTeamLeader;
        dbProject.KeyContactsFsgTeamLeaderEmail = updateRequest.Contacts.TeamLead.Email ?? dbProject.KeyContactsFsgTeamLeaderEmail;

        dbProject.KeyContactsFsgGrade6 = updateRequest.Contacts.Grade6.Name ?? dbProject.KeyContactsFsgGrade6;
        dbProject.KeyContactsFsgGrade6Email = updateRequest.Contacts.Grade6.Email ?? dbProject.KeyContactsFsgGrade6Email;

        dbProject.KeyContactsEsfaCapitalProjectManager = updateRequest.Contacts.ProjectManager.Name ?? dbProject.KeyContactsEsfaCapitalProjectManager;
        dbProject.KeyContactsEsfaCapitalProjectManagerEmail = updateRequest.Contacts.ProjectManager.Email ?? dbProject.KeyContactsEsfaCapitalProjectManagerEmail;

        dbProject.KeyContactsEsfaCapitalProjectDirector = updateRequest.Contacts.ProjectDirector.Name ?? dbProject.KeyContactsEsfaCapitalProjectDirector;
        dbProject.KeyContactsEsfaCapitalProjectDirectorEmail = updateRequest.Contacts.ProjectDirector.Email ?? dbProject.KeyContactsEsfaCapitalProjectDirectorEmail;

        dbProject.KeyContactsChairOfGovernorsMat = updateRequest.Contacts.TrustContact.Name ?? dbProject.KeyContactsChairOfGovernorsMat;
        dbProject.KeyContactsChairOfGovernorsMatEmail = updateRequest.Contacts.TrustContact.Email ?? dbProject.KeyContactsChairOfGovernorsMatEmail;
        dbProject.KeyContactsChairOfGovernorsMatPhone = updateRequest.Contacts.TrustContact.PhoneNumber ?? dbProject.KeyContactsChairOfGovernorsMatPhone;
        dbProject.KeyContactsChairOfGovernorsMatRole = updateRequest.Contacts.TrustContact.Role ?? dbProject.KeyContactsChairOfGovernorsMatRole;


        dbProject.KeyContactsOfstedContact = updateRequest.Contacts.OfstedContact.Name ?? dbProject.KeyContactsOfstedContact;
        dbProject.KeyContactsOfstedContactEmail = updateRequest.Contacts.OfstedContact.Email ?? dbProject.KeyContactsOfstedContactEmail;
        dbProject.KeyContactsOfstedContactPhone = updateRequest.Contacts.OfstedContact.PhoneNumber ?? dbProject.KeyContactsOfstedContactPhone;
        dbProject.KeyContactsOfstedContactRole = updateRequest.Contacts.OfstedContact.Role ?? dbProject.KeyContactsOfstedContactRole;

        await _context.SaveChangesAsync();
    }
    
}