using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using System;
using System.Linq;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public record PDGPaymentInfo
    (
        int numberOfScheduledPayments,
        int numberOfSentPayments,
        DateTime? nextPaymentDate,
        decimal? nextPaymentAmount,
        decimal? amountSent
    );

    public interface IPDGPaymentInfoService
    {
        PDGPaymentInfo GetPDGPaymentInfo(ProjectPayments projectPayments);
    }

    public class PDGPaymentInfoService : IPDGPaymentInfoService
    {

        public PDGPaymentInfo GetPDGPaymentInfo(ProjectPayments projectPayments)
        {
            var numberOfScheduledPayments = projectPayments.Payments.Count();

            var paidPayments = projectPayments.Payments.Where(x => x.PaymentActualAmount != null || x.PaymentActualDate != null).ToList();
            var unpaidPayments = projectPayments.Payments.Where(x => x.PaymentActualAmount == null && x.PaymentActualDate == null).ToList();

            var numberOfSentPayments = paidPayments.Count();
            var nextPaymentDate = unpaidPayments.FirstOrDefault()?.PaymentScheduleDate;
            var nextPaymentAmount = unpaidPayments.FirstOrDefault()?.PaymentScheduleAmount;
            var amountSent = paidPayments.Select(p => p.PaymentActualAmount).Sum();

            return new PDGPaymentInfo(numberOfScheduledPayments, numberOfSentPayments, nextPaymentDate, nextPaymentAmount, amountSent);
        }
    }
}
