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

        var po = await _context.Po.FirstOrDefaultAsync(p => p.Rid == dbProject.Rid);

        if (payment.PaymentIndex is null)
        {
            if (
                po.ProjectDevelopmentGrantFundingAmountOf1stPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf1stPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf1stPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf1stActualPayment is null
               )
            {
                payment.PaymentIndex = 1;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf2ndPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf2ndPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf2ndActualPayment is null
               )
            {
                payment.PaymentIndex = 2;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf3rdPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf3rdPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf3rdPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf3rdActualPayment is null
               )
            {
                payment.PaymentIndex = 3;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf4thPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf4thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf4thPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf4thActualPayment is null
               )
            {
                payment.PaymentIndex = 4;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf5thPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf5thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf5thPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf5thActualPayment is null
               )
            {
                payment.PaymentIndex = 5;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf6thPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf6thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf6thPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf6thActualPayment is null
               )
            {
                payment.PaymentIndex = 6;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf7thPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf7thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf7thPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf7thActualPayment is null
               )
            {
                payment.PaymentIndex = 7;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf8thPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf8thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf8thPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf8thActualPayment is null
               )
            {
                payment.PaymentIndex = 8;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf9thPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf9thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf9thPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf9thActualPayment is null
               )
            {
                payment.PaymentIndex = 9;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf10thPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf10thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf10thPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf10thActualPayment is null
               )
            {
                payment.PaymentIndex = 10;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf11thPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf11thActualPayment is null
               )
            {
                payment.PaymentIndex = 11;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf12thPaymentDue == "" &&
                po.ProjectDevelopmentGrantFundingDateOf12thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf12thPayment == "" &&
                po.ProjectDevelopmentGrantFundingDateOf12thActualPayment is null
               )
            {
                payment.PaymentIndex = 12;
            }

            else
            {
                throw new Exception("Cannot add more that 12 payments");
            }
        }

        po = ProjectPaymentsUpdater.Update(po, payment);

        await _context.SaveChangesAsync();

    }
}

