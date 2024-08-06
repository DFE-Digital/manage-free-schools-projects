using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Payments
{
    public static class ProjectPaymentsDeleter
    {
        public static Po Delete(Po po, int paymentIndex)
        {

            if (paymentIndex <= 1)
            {
                po.ProjectDevelopmentGrantFundingAmountOf1stPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf2ndPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf1stPaymentDue = po.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf1stPayment = po.ProjectDevelopmentGrantFundingAmountOf2ndPayment;
                po.ProjectDevelopmentGrantFundingDateOf1stActualPayment = po.ProjectDevelopmentGrantFundingDateOf2ndActualPayment;
            };

            if (paymentIndex <= 2)
            {
                po.ProjectDevelopmentGrantFundingAmountOf2ndPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf3rdPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue = po.ProjectDevelopmentGrantFundingDateOf3rdPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf2ndPayment = po.ProjectDevelopmentGrantFundingAmountOf3rdPayment;
                po.ProjectDevelopmentGrantFundingDateOf2ndActualPayment = po.ProjectDevelopmentGrantFundingDateOf3rdActualPayment;
            };

            if (paymentIndex <= 3)
            {
                po.ProjectDevelopmentGrantFundingAmountOf3rdPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf4thPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf3rdPaymentDue = po.ProjectDevelopmentGrantFundingDateOf4thPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf3rdPayment = po.ProjectDevelopmentGrantFundingAmountOf4thPayment;
                po.ProjectDevelopmentGrantFundingDateOf3rdActualPayment = po.ProjectDevelopmentGrantFundingDateOf4thActualPayment;
            };

            if (paymentIndex <= 4)
            {
                po.ProjectDevelopmentGrantFundingAmountOf4thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf5thPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf4thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf5thPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf4thPayment = po.ProjectDevelopmentGrantFundingAmountOf5thPayment;
                po.ProjectDevelopmentGrantFundingDateOf4thActualPayment = po.ProjectDevelopmentGrantFundingDateOf5thActualPayment;
            };

            if (paymentIndex <= 5)
            {
                po.ProjectDevelopmentGrantFundingAmountOf5thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf5thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf5thPayment = po.ProjectDevelopmentGrantFundingAmountOf11thPayment;
                po.ProjectDevelopmentGrantFundingDateOf5thActualPayment = po.ProjectDevelopmentGrantFundingDateOf11thActualPayment;
            };

            if (paymentIndex <= 11)
            {
                po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf7thPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf7thPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf11thPayment = po.ProjectDevelopmentGrantFundingAmountOf7thPayment;
                po.ProjectDevelopmentGrantFundingDateOf11thActualPayment = po.ProjectDevelopmentGrantFundingDateOf7thActualPayment;
            };

            if (paymentIndex <= 7)
            {
                po.ProjectDevelopmentGrantFundingAmountOf7thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf8thPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf7thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf8thPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf7thPayment = po.ProjectDevelopmentGrantFundingAmountOf8thPayment;
                po.ProjectDevelopmentGrantFundingDateOf7thActualPayment = po.ProjectDevelopmentGrantFundingDateOf8thActualPayment;
            };

            if (paymentIndex <= 8)
            {
                po.ProjectDevelopmentGrantFundingAmountOf8thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf9thPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf8thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf9thPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf8thPayment = po.ProjectDevelopmentGrantFundingAmountOf9thPayment;
                po.ProjectDevelopmentGrantFundingDateOf8thActualPayment = po.ProjectDevelopmentGrantFundingDateOf9thActualPayment;
            };

            if (paymentIndex <= 9)
            {
                po.ProjectDevelopmentGrantFundingAmountOf9thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf10thPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf9thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf10thPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf9thPayment = po.ProjectDevelopmentGrantFundingAmountOf10thPayment;
                po.ProjectDevelopmentGrantFundingDateOf9thActualPayment = po.ProjectDevelopmentGrantFundingDateOf10thActualPayment;
            };

            if (paymentIndex <= 10)
            {
                po.ProjectDevelopmentGrantFundingAmountOf10thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf10thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf10thPayment = po.ProjectDevelopmentGrantFundingAmountOf11thPayment;
                po.ProjectDevelopmentGrantFundingDateOf10thActualPayment = po.ProjectDevelopmentGrantFundingDateOf11thActualPayment;
            };

            if (paymentIndex <= 11)
            {
                po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue = po.ProjectDevelopmentGrantFundingAmountOf12thPaymentDue;
                po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue = po.ProjectDevelopmentGrantFundingDateOf12thPaymentDue;
                po.ProjectDevelopmentGrantFundingAmountOf11thPayment = po.ProjectDevelopmentGrantFundingAmountOf12thPayment;
                po.ProjectDevelopmentGrantFundingDateOf11thActualPayment = po.ProjectDevelopmentGrantFundingDateOf12thActualPayment;
            };

            if (paymentIndex == 12)
            {
                po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue = null;
                po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue = null;
                po.ProjectDevelopmentGrantFundingAmountOf11thPayment = null;
                po.ProjectDevelopmentGrantFundingDateOf11thActualPayment = null;
            };

            if (paymentIndex > 12)
            {
                throw new Exception("Index out of range");
            }

            return po;
        }
    }
}

