using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ArticlesOfAssociation
{
    internal class GetArticlesOfAssociationTaskService : IGetTaskService
    {
        private readonly MfspContext _context;

        public GetArticlesOfAssociationTaskService(MfspContext context)
        {
            _context = context;

        }

        public async Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
        {
            var result = await(from kpi in parameters.BaseQuery
                                join milestones in _context.Milestones on kpi.Rid equals milestones.Rid into joinedMilestones
                                from milestones in joinedMilestones.DefaultIfEmpty()
                                select new GetProjectByTaskResponse()
                                {
                                    ArticlesOfAssociation = new()
                                    {
                                        CheckedSubmittedArticlesMatch = milestones.MAACheckedSubmittedArticlesMatch,
                                        ChairHaveSubmittedConfirmation = milestones.MAAChairHaveSubmittedConfirmation,
                                        ArrangementsMatchGovernancePlans = milestones.MAAArrangementsMatchGovernancePlans,
                                        ForecastDate = milestones.FsgPreOpeningMilestonesMaaForecastDate,
                                        ActualDate = milestones.FsgPreOpeningMilestonesMaaActualDateOfCompletion,
                                        CommentsOnDecision = milestones.FsgPreOpeningMilestonesMi56CommentsOnDecisionToApproveIfApplicable,
                                        SharepointLink = milestones.FsgPreOpeningMilestonesMi107LinkToSavedDocument
                                    }
                                }).FirstOrDefaultAsync();

            return result ?? new GetProjectByTaskResponse() { ArticlesOfAssociation = new () };
        }
}
}