using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PDG;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
namespace Dfe.ManageFreeSchoolProjects.API.Tests.UseCases.Project.Tasks.PDG
{
    public class PaymentCalculationTests
    {
        [Fact]
        public void GetTotalAmount_ReturnsNull_WhenAllPaymentsAreNull()
        {
            var po = new Po();

            var actual = PaymentCalculation.GetTotalAmount(po);

            Assert.Null(actual);
        }

        [Fact]
        public void GetTotalAmount_ReturnsSumOfPayments_WhenSomePaymentsAreNull()
        {
            var po = new Po
            {
                ProjectDevelopmentGrantFundingAmountOf1stPayment = "1.00",
                ProjectDevelopmentGrantFundingAmountOf2ndPayment = "2.00",
                ProjectDevelopmentGrantFundingAmountOf3rdPayment = null,
                ProjectDevelopmentGrantFundingAmountOf4thPayment = "4.00",
            };

            var actual = PaymentCalculation.GetTotalAmount(po);

            Assert.Equal(7.00m, actual);
        }

        [Fact]
        public void GetTotalAmount_ReturnsSumOfPayments_WhenAllPaymentsAreNotNull()
        {
            var po = new Po
            {
                ProjectDevelopmentGrantFundingAmountOf1stPayment = "1.00",
                ProjectDevelopmentGrantFundingAmountOf2ndPayment = "2.00",
                ProjectDevelopmentGrantFundingAmountOf3rdPayment = "3.00",
                ProjectDevelopmentGrantFundingAmountOf4thPayment = "4.00",
                ProjectDevelopmentGrantFundingAmountOf5thPayment = "5.00",
                ProjectDevelopmentGrantFundingAmountOf6thPayment = "6.00",
                ProjectDevelopmentGrantFundingAmountOf7thPayment = "7.00",
                ProjectDevelopmentGrantFundingAmountOf8thPayment = "8.00",
                ProjectDevelopmentGrantFundingAmountOf9thPayment = "9.00",
                ProjectDevelopmentGrantFundingAmountOf10thPayment = "10.00",
                ProjectDevelopmentGrantFundingAmountOf11thPayment = "11.00",
                ProjectDevelopmentGrantFundingAmountOf12thPayment = "12.00",
            };

            var actual = PaymentCalculation.GetTotalAmount(po);

            Assert.Equal(78.00m, actual);
        }
    }
}
