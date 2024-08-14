namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks
{
    public class FundingAgreementSubmissionTask
    {
        public bool? DraftedFundingAgreementSubmission { get; set; }
        public bool? RegionalDirectorSignedOffFundingAgreementSubmission { get; set; }
        public bool? MinisterSignedOffFundingAgreementSubmission { get; set; }
        public bool? SavedFundingAgreementSubmissionInWorkplacesFolder { get; set; }
    }
}
