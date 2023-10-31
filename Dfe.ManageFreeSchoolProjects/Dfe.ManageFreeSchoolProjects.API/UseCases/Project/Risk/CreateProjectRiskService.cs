using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Risk
{
    public interface ICreateProjectRiskService
    {
        Task<CreateProjectRiskResponse> Execute(string projectId, CreateProjectRiskRequest request);
    }

    public class CreateProjectRiskService : ICreateProjectRiskService
    {
        private readonly MfspContext _context;

        public CreateProjectRiskService(MfspContext context)
        {
            _context = context;
        }

        public async Task<CreateProjectRiskResponse> Execute(string projectId, CreateProjectRiskRequest request)
        {
            var toAdd = new Rag()
            {
                RagRatingsGovernanceAndSuitabilityRagRating = request.GovernanceAndSuitability.RiskRating.GetDescription(),
                RagRatingsEducationRag = request.Education.RiskRating.GetDescription(),
                RagRatingsFinancesRagRating = request.Finance.RiskRating.GetDescription(),
                RagRatingsFinanceRagSummary = request.Finance.Summary,
                RagRatingsOverallRagRating = request.Overall.RiskRating.GetDescription(),
                RagRatingsOverallRagSummary = request.Overall.Summary,
            };

            _context.Rag.Add(toAdd);

            await _context.SaveChangesAsync();

            return new CreateProjectRiskResponse();
        }
    }
}
