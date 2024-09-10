using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks.PDG
{
    public class PDGDashboard
    {
        public decimal? InitialGrant { get; set; }
        public decimal? RevisedGrant { get; set; }
        public decimal? PaymentScheduleAmount{ get; set; }
        public DateTime? PaymentScheduleDate { get; set; }
        public decimal? PaymentActualAmount { get; set; }
        public DateTime? PaymentActualDate { get; set; }
        public DateTime? TrustSignedPDGLetterDate { get; set; }
        public bool? PDGLetterSavedInWorkplaces { get; set; }
        public string PaymentStopped { get; set; }
        public DateTime? PaymentStoppedDate { get; set; }
        public DateTime? LatestRefundDate { get; set; }
        public decimal? RefundsTotalAmount { get; set; }
        public bool? IsWriteOffSetup { get; set; }
        public string WriteOffReason { get; set; }
        public decimal? WriteOffAmount { get; set; }
        public DateTime? WriteOffDate { get; set; }
        public string FinanceBusinessPartnerApprovalReceivedFrom { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
