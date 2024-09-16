using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PDG;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Payments
{
    public static class ProjectPaymentsAdder
    {

        public static void Add(Po po, Payment payment)
        {

            if (
                po.ProjectDevelopmentGrantFundingAmountOf1stPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf1stPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf1stPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf1stActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf1stPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf1stPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf1stPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf1stActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf2ndPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf2ndPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf2ndActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf2ndPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf2ndPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf2ndActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf3rdPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf3rdPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf3rdPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf3rdActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf3rdPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf3rdPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf3rdPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf3rdActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf4thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf4thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf4thPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf4thActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf4thPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf4thPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf4thPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf4thActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf5thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf5thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf5thPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf5thActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf5thPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf5thPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf5thPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf5thActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf6thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf6thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf6thPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf6thActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf6thPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf6thPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf6thPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf6thActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf7thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf7thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf7thPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf7thActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf7thPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf7thPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf7thPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf7thActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf8thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf8thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf8thPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf8thActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf8thPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf8thPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf8thPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf8thActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf9thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf9thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf9thPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf9thActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf9thPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf9thPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf9thPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf9thActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf10thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf10thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf10thPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf10thActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf10thPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf10thPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf10thPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf10thActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf11thPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf11thActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf11thPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf11thActualPayment = payment.PaymentActualDate;
            }

            else if (
                po.ProjectDevelopmentGrantFundingAmountOf12thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingDateOf12thPaymentDue is null &&
                po.ProjectDevelopmentGrantFundingAmountOf12thPayment is null &&
                po.ProjectDevelopmentGrantFundingDateOf12thActualPayment is null
                )
            {
                po.ProjectDevelopmentGrantFundingAmountOf12thPaymentDue = payment.PaymentScheduleAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf12thPaymentDue = payment.PaymentScheduleDate;
                po.ProjectDevelopmentGrantFundingAmountOf12thPayment = payment.PaymentActualAmount.ToString();
                po.ProjectDevelopmentGrantFundingDateOf12thActualPayment = payment.PaymentActualDate;
            }

            else
            {
                throw new NotFoundException("Cannot add more that 12 payments");
            }


            po.ProjectDevelopmentGrantFundingTotalPaymentsMade = PaymentCalculation.GetTotalAmount(po).ToString();

        }

    }
}