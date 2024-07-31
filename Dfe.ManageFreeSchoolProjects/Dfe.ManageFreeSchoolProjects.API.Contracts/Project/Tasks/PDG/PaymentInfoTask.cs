namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG
{
    public class PaymentInfoTask
    {
        public string ProjectId { get; set; }
        public IEnumerable<Payment> Payments { get; set; }
    }
    public class Payment
    {
        public decimal? PaymentScheduleAmount { get; set; }
        public DateTime? PaymentScheduleDate { get; set; }
        public decimal? PaymentActualAmount { get; set; }
        public DateTime? PaymentActualDate { get; set; }
    }

}
