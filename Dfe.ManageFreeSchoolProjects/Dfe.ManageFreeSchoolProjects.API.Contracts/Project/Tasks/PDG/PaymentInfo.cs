namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG
{
    public class PaymentInfoForProject
    {
        public string ProjectId { get; set; }
        public List<PaymentInfo> Payments { get; set; } = new();
    }
    public class PaymentInfo
    {
        public int PaymentIndex { get; set; }
        public decimal? PaymentScheduleAmount { get; set; }
        public DateTime? PaymentScheduleDate { get; set; }
        public decimal? PaymentActualAmount { get; set; }
        public DateTime? PaymentActualDate { get; set; }
    }

}
