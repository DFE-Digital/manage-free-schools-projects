using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Payments;
public interface IUpdateProjectPaymentsService
{
    Task Execute(string projectId, Payment payment);
}
public class UpdateProjectPaymentsService : IUpdateProjectPaymentsService
{
    private readonly MfspContext _context;

    public UpdateProjectPaymentsService(MfspContext context)
    {
        _context = context;
    }

    public async Task Execute(string projectId, Payment payment)
    {
        var dbProject = await _context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

        if (dbProject == null)
        {
            throw new NotFoundException($"Project with id {projectId} not found");
        }

        if (payment.PaymentIndex is null)
        {
            throw new NotFoundException($"Please provide a payment index");
        }

        var po = await _context.Po.FirstOrDefaultAsync(p => p.Rid == dbProject.Rid);

        ProjectPaymentsUpdater.Update(po, payment);

        await _context.SaveChangesAsync();

    }
}

