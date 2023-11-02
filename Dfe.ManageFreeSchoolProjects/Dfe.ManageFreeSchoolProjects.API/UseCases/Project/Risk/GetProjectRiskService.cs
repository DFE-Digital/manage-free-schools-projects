using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.Exceptions;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Risk
{
    public interface IGetProjectRiskService
    {
        Task<GetProjectRiskResponse> Execute(string projectId);
    }

    public class GetProjectRiskService : IGetProjectRiskService
    {
        private readonly MfspContext _context;

        public GetProjectRiskService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetProjectRiskResponse> Execute(string projectId)
        {
            var dbProject = await _context.Kpi.FirstOrDefaultAsync(x => x.ProjectStatusProjectId == projectId);

            if (dbProject == null)
            {
                throw new NotFoundException($"Project with id {projectId} not found");
            }

            var result = await _context.Rag
                .Where(e => e.Rid == dbProject.Rid)
                .Select(rag => new GetProjectRiskResponse
                {
                    GovernanceAndSuitability = new()
                    {
                        RiskRating = MapToRisk(rag.RagRatingsGovernanceAndSuitabilityRagRating)
                    },
                    Education = new()
                    {
                        RiskRating = MapToRisk(rag.RagRatingsEducationRag)
                    },
                    Finance = new()
                    {
                        RiskRating = MapToRisk(rag.RagRatingsFinancesRagRating),
                        Summary = rag.RagRatingsFinanceRagSummary
                    },
                    Overall = new()
                    {
                        RiskRating = MapToRisk(rag.RagRatingsOverallRagRating),
                        Summary = rag.RagRatingsOverallRagSummary
                    },
                }).FirstOrDefaultAsync();

            var history = await _context.Rag
                .TemporalAll()
                .Where(e => e.Rid == dbProject.Rid)
                .Select(rag => new ProjectRiskHistoryResponse
                {
                    Date = DateTime.Now,
                    RiskRating = MapToRisk(rag.RagRatingsOverallRagRating)
                })
                .ToListAsync();

            result.History = history;

            return result;
        }

        private static ProjectRiskRating MapToRisk(string value)
        {
            if (value == ProjectRiskRating.Green.GetDescription())
            {
                return ProjectRiskRating.Green;
            }

            if (value == ProjectRiskRating.AmberGreen.GetDescription())
            {
                return ProjectRiskRating.AmberGreen;
            }

            if (value == ProjectRiskRating.AmberRed.GetDescription())
            {
                return ProjectRiskRating.AmberRed;
            }

            if (value == ProjectRiskRating.Red.GetDescription())
            {
                return ProjectRiskRating.Red;
            }

            return ProjectRiskRating.Unknown;
        }
    }
}
