using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ModelFundingAgreement;

    internal class GetModelFundingAgreementTaskService : IGetTaskService
    {
        private readonly MfspContext _context;

        public GetModelFundingAgreementTaskService(MfspContext context)
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
                    ModelFundingAgreement = new()
                    {
                        TrustAgreesWithModelFA = milestones.FsgPreOpeningMilestonesMfadTrustAgreesWithModelFa,
                        DraftedFAHealthCheck = milestones.FsgPreOpeningMilestonesMfadDraftedFaHealthCheck,
                        DateTrustAgreesWithModelFA = milestones.FsgPreOpeningMilestonesMfadActualDateOfCompletion,
                        Comments = milestones.FsgPreOpeningMilestonesMi58CommentsOnDecisionToApproveIfApplicable,
                        SavedFADocumentsInWorkplacesFolder = milestones.FsgPreOpeningMilestonesMfadSavedFaDocumentsInWorkspacesFolder,
                        TayloredAModelFundingAgreement = milestones.FsgPreOpeningMilestonesMfadTayloredAModelFundingAgreement,
                        SharedFAWithTheTrust = milestones.FsgPreOpeningMilestonesMfadSharedFaWithTheTrust,
                    }
                }).FirstOrDefaultAsync();

            return result ?? new GetProjectByTaskResponse() { ModelFundingAgreement = new () };
        }
    }
