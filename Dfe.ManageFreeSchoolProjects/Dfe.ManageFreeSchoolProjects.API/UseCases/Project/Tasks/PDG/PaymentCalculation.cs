using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PDG
{
    public class PaymentCalculation
    {
        public static decimal? GetTotalAmount(Po po)
        {
            if (po.ProjectDevelopmentGrantFundingAmountOf1stPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf2ndPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf3rdPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf4thPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf5thPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf6thPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf7thPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf8thPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf9thPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf10thPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf11thPayment == null &&
                po.ProjectDevelopmentGrantFundingAmountOf12thPayment == null)
                return null;

            return ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf1stPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf2ndPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf3rdPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf4thPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf5thPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf6thPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf7thPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf8thPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf9thPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf10thPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf11thPayment) +
                   ParseDecimal(po.ProjectDevelopmentGrantFundingAmountOf12thPayment);
        }

        private static decimal ParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            return decimal.Parse(value);
        }
    }
}
