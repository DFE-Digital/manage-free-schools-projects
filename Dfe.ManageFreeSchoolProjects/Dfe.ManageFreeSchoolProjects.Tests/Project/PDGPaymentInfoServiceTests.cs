using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Payments;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using FluentAssertions;

namespace Dfe.ManageFreeSchoolProjects.Tests.Project
{
    public class PDGPaymentInfoServiceTests
    {

        [Fact]
        public void GivenEmptyFuturePaymentReturnsFuturePayment()
        {
            var projectPayments = new ProjectPayments
            {
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        PaymentIndex = 1,
                        PaymentScheduleDate = new DateTime(2021, 1, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 1, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 2,
                        PaymentScheduleDate = new DateTime(2021, 2, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 2, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 3,
                        PaymentScheduleDate = new DateTime(2021, 3, 1),
                        PaymentScheduleAmount = 100
                    },
                }
            };

            var service = new PDGPaymentInfoService();

            var result = service.GetPDGPaymentInfo(projectPayments);

            result.numberOfScheduledPayments.Should().Be(3);
            result.numberOfSentPayments.Should().Be(2);
            result.nextPaymentDate.Should().Be(new DateTime(2021, 3, 1));
            result.nextPaymentAmount.Should().Be(100);
            result.amountSent.Should().Be(200);
        }


        [Fact]
        public void GivenTwoEmptyFuturePaymentReturnsFirstFuturePayment()
        {
            var projectPayments = new ProjectPayments
            {
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        PaymentIndex = 1,
                        PaymentScheduleDate = new DateTime(2021, 1, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 1, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 2,
                        PaymentScheduleDate = new DateTime(2021, 2, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 2, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 3,
                        PaymentScheduleDate = new DateTime(2021, 3, 1),
                        PaymentScheduleAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 4,
                        PaymentScheduleDate = new DateTime(2021, 4, 1),
                        PaymentScheduleAmount = 200
                    },
                }
            };

            var service = new PDGPaymentInfoService();

            var result = service.GetPDGPaymentInfo(projectPayments);

            result.numberOfScheduledPayments.Should().Be(4);
            result.numberOfSentPayments.Should().Be(2);
            result.nextPaymentDate.Should().Be(new DateTime(2021, 3, 1));
            result.nextPaymentAmount.Should().Be(100);
            result.amountSent.Should().Be(200);
        }



        [Fact]
        public void GivenFilledFuturePaymentReturnsEmptyFuturePayment()
        {
            var projectPayments = new ProjectPayments
            {
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        PaymentIndex = 1,
                        PaymentScheduleDate = new DateTime(2021, 1, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 1, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 2,
                        PaymentScheduleDate = new DateTime(2021, 2, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 2, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 3,
                        PaymentScheduleDate = new DateTime(2021, 3, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 3, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 4,
                        PaymentScheduleDate = new DateTime(2021, 4, 1),
                        PaymentScheduleAmount = 200
                    },
                }
            };

            var service = new PDGPaymentInfoService();

            var result = service.GetPDGPaymentInfo(projectPayments);

            result.numberOfScheduledPayments.Should().Be(4);
            result.numberOfSentPayments.Should().Be(3);
            result.nextPaymentDate.Should().Be(new DateTime(2021, 4, 1));
            result.nextPaymentAmount.Should().Be(200);
            result.amountSent.Should().Be(300);
        }



        [Fact]
        public void GivenPartiallyFilledDateFuturePaymentReturnsEmptyFuturePayment()
        {
            var projectPayments = new ProjectPayments
            {
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        PaymentIndex = 1,
                        PaymentScheduleDate = new DateTime(2021, 1, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 1, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 2,
                        PaymentScheduleDate = new DateTime(2021, 2, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 2, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 3,
                        PaymentScheduleDate = new DateTime(2021, 3, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 3, 1),
                    },
                    new Payment
                    {
                        PaymentIndex = 4,
                        PaymentScheduleDate = new DateTime(2021, 4, 1),
                        PaymentScheduleAmount = 200
                    },
                }
            };

            var service = new PDGPaymentInfoService();

            var result = service.GetPDGPaymentInfo(projectPayments);

            result.numberOfScheduledPayments.Should().Be(4);
            result.numberOfSentPayments.Should().Be(3);
            result.nextPaymentDate.Should().Be(new DateTime(2021, 4, 1));
            result.nextPaymentAmount.Should().Be(200);
            result.amountSent.Should().Be(200);
        }


        [Fact]
        public void GivenPartiallyFilledAmountFuturePaymentReturnsEmptyFuturePayment()
        {
            var projectPayments = new ProjectPayments
            {
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        PaymentIndex = 1,
                        PaymentScheduleDate = new DateTime(2021, 1, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 1, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 2,
                        PaymentScheduleDate = new DateTime(2021, 2, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualDate = new DateTime(2021, 2, 1),
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 3,
                        PaymentScheduleDate = new DateTime(2021, 3, 1),
                        PaymentScheduleAmount = 100,
                        PaymentActualAmount = 100
                    },
                    new Payment
                    {
                        PaymentIndex = 4,
                        PaymentScheduleDate = new DateTime(2021, 4, 1),
                        PaymentScheduleAmount = 200
                    },
                }
            };

            var service = new PDGPaymentInfoService();

            var result = service.GetPDGPaymentInfo(projectPayments);

            result.numberOfScheduledPayments.Should().Be(4);
            result.numberOfSentPayments.Should().Be(3);
            result.nextPaymentDate.Should().Be(new DateTime(2021, 4, 1));
            result.nextPaymentAmount.Should().Be(200);
            result.amountSent.Should().Be(300);
        }
    }
}
