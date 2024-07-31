using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PDG.PaymentInfo
{
    public static class PaymentInfoBuilder
    {
        public static PaymentInfoTask Build(Po po)
        {
            if (po == null)
            {
                return new PaymentInfoTask();
            }

            return new PaymentInfoTask()
            {
                Payments = new List<Payment>()
                {
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf1stPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf1stPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf1stPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf1stActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf2ndPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf2ndPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf2ndPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf2ndActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf3rdPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf3rdPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf3rdPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf3rdActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf4thPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf4thPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf4thPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf4thActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf5thPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf5thPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf5thPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf5thActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf6thPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf6thPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf6thPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf6thActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf7thPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf7thPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf7thPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf7thActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf8thPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf8thPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf8thPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf8thActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf9thPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf9thPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf9thPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf9thActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf10thPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf10thPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf10thPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf10thActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf11thPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf11thPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf11thPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf11thActualPayment
                    },
                    new Payment
                    {
                        PaymentScheduleAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf12thPaymentDue),
                        PaymentScheduleDate = po.ProjectDevelopmentGrantFundingDateOf12thPaymentDue,
                        PaymentActualAmount = ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf12thPayment),
                        PaymentActualDate = po.ProjectDevelopmentGrantFundingDateOf12thActualPayment
                    },
                }
            };
        }

        private static decimal? ParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return decimal.Parse(value);
        }
    }
}
