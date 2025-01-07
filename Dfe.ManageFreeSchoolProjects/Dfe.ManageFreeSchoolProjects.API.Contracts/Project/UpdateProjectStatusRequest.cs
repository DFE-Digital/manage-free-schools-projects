using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

public class UpdateProjectStatusRequest
{
    public ProjectStatus ProjectStatus { get; set; }

    public ProjectCancelledReason ProjectCancelledReason { get; set; }

    public ProjectWithdrawnReason ProjectWithdrawnReason { get; set; }

    public DateTime? CancelledDate { get; set; }

    public DateTime? ClosedDate { get; set; }

    public DateTime? WithdrawnDate { get; set; }

    public YesNo? ProjectCancelledDueToNationalReviewOfPipelineProjects { get; set; }

    public YesNo? ProjectWithdrawnDueToNationalReviewOfPipelineProjects { get; set; }

    public string CommentaryForCancellation { get; set; }
    public string CommentaryForWithdrawal { get; set; }

}