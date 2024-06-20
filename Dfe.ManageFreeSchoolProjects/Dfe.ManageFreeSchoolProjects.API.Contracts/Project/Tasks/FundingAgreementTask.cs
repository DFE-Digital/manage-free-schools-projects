using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

public class FundingAgreementTask
{
    public bool? TailoredTheFundingAgreement { get; set; }
    public bool? SharedFAWithTheTrust { get; set; }
    public YesNo? TrustHasSignedTheFA { get; set; }
    public DateTime? DateTheTrustSignedFA { get; set; }
    public DateTime? ExpectedDateFAIsSignedOnSecretaryOfStatesBehalf { get; set; }
    public DateTime? DateFAWasSigned { get; set; }
    public bool? SavedFADocumentsInWorkplacesFolder { get; set; }
}