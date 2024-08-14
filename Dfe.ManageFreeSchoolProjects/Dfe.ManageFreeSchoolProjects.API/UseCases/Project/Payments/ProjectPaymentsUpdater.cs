using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Payments
{
    public static class ProjectPaymentsUpdater
    {

        public static void Update(Po po, Payment payment)
        {

            switch (payment.PaymentIndex)
            {
                case 1:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf1stPaymentDue, po.ProjectDevelopmentGrantFundingDateOf1stPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf1stPayment, po.ProjectDevelopmentGrantFundingDateOf1stActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf1stPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf1stPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf1stPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf1stActualPayment = payment.PaymentActualDate;
                    break;

                case 2:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf2ndPaymentDue, po.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf2ndPayment, po.ProjectDevelopmentGrantFundingDateOf2ndActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf2ndPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf2ndPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf2ndActualPayment = payment.PaymentActualDate;
                    break;

                case 3:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf3rdPaymentDue, po.ProjectDevelopmentGrantFundingDateOf3rdPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf3rdPayment, po.ProjectDevelopmentGrantFundingDateOf3rdActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf3rdPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf3rdPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf3rdPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf3rdActualPayment = payment.PaymentActualDate;
                    break;

                case 4:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf4thPaymentDue, po.ProjectDevelopmentGrantFundingDateOf4thPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf4thPayment, po.ProjectDevelopmentGrantFundingDateOf4thActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf4thPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf4thPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf4thPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf4thActualPayment = payment.PaymentActualDate;
                    break;

                case 5:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf5thPaymentDue, po.ProjectDevelopmentGrantFundingDateOf5thPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf5thPayment, po.ProjectDevelopmentGrantFundingDateOf5thActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf5thPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf5thPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf5thPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf5thActualPayment = payment.PaymentActualDate;
                    break;

                case 6:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf6thPaymentDue, po.ProjectDevelopmentGrantFundingDateOf6thPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf6thPayment, po.ProjectDevelopmentGrantFundingDateOf6thActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf6thPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf6thPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf6thPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf6thActualPayment = payment.PaymentActualDate;
                    break;

                case 7:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf7thPaymentDue, po.ProjectDevelopmentGrantFundingDateOf7thPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf7thPayment, po.ProjectDevelopmentGrantFundingDateOf7thActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf7thPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf7thPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf7thPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf7thActualPayment = payment.PaymentActualDate;
                    break;

                case 8:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf8thPaymentDue, po.ProjectDevelopmentGrantFundingDateOf8thPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf8thPayment, po.ProjectDevelopmentGrantFundingDateOf8thActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf8thPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf8thPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf8thPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf8thActualPayment = payment.PaymentActualDate;
                    break;

                case 9:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf9thPaymentDue, po.ProjectDevelopmentGrantFundingDateOf9thPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf9thPayment, po.ProjectDevelopmentGrantFundingDateOf9thActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf9thPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf9thPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf9thPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf9thActualPayment = payment.PaymentActualDate;
                    break;

                case 10:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf10thPaymentDue, po.ProjectDevelopmentGrantFundingDateOf10thPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf10thPayment, po.ProjectDevelopmentGrantFundingDateOf10thActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf10thPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf10thPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf10thPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf10thActualPayment = payment.PaymentActualDate;
                    break;

                case 11:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue, po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf11thPayment, po.ProjectDevelopmentGrantFundingDateOf11thActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf11thPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf11thActualPayment = payment.PaymentActualDate;
                    break;

                case 12:
                    CheckPaymentExists(po.ProjectDevelopmentGrantFundingAmountOf12thPaymentDue, po.ProjectDevelopmentGrantFundingDateOf12thPaymentDue, po.ProjectDevelopmentGrantFundingAmountOf12thPayment, po.ProjectDevelopmentGrantFundingDateOf12thActualPayment);

                    po.ProjectDevelopmentGrantFundingAmountOf12thPaymentDue = payment.PaymentScheduleAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf12thPaymentDue = payment.PaymentScheduleDate;
                    po.ProjectDevelopmentGrantFundingAmountOf12thPayment = payment.PaymentActualAmount?.ToString("0.00");
                    po.ProjectDevelopmentGrantFundingDateOf12thActualPayment = payment.PaymentActualDate;
                    break;

                default:
                    throw new NotFoundException($"Index {payment.PaymentIndex} cannot be found");
            }
        }

        public static void CheckPaymentExists(string paymentScheduleAmount, DateTime? paymentScheduleDate, string paymentActualAmount, DateTime? paymentActualDate)
        {
            if (paymentScheduleAmount is null && paymentScheduleDate is null && paymentActualAmount is null && paymentActualDate is null)
            {
                throw new NotFoundException($"Payment not found");
            }
        }
    }
}

