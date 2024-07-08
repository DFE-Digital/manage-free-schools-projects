using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PrincipleDesignate
{
	
	
	public static class PrincipleDesignateTaskBuilder
	{
		public static PrincipleDesignateTask Build(Milestones milestones)
		{
			if (milestones == null)
			{
				return new PrincipleDesignateTask();
			}

			if (milestones.FsgPreOpeningMilestonesPdappActualDateOfCompletion.HasValue)
			{
				return new PrincipleDesignateTask()
				{
					TrustAppointedPrincipleDesignate = true,
					TrustAppointedPrincipleDesignateDate =
						milestones.FsgPreOpeningMilestonesPdappActualDateOfCompletion,
					CommissionedExternalExpertVisitToSchool =
						milestones.FsgPreOpeningMilestonesCommissionedExternalExpertVisitToSchool,
				};
			}
				return new PrincipleDesignateTask()
				{
					TrustAppointedPrincipleDesignate = false,
					TrustAppointedPrincipleDesignateDate =
						milestones.FsgPreOpeningMilestonesPdappActualDateOfCompletion,
					CommissionedExternalExpertVisitToSchool =
						milestones.FsgPreOpeningMilestonesCommissionedExternalExpertVisitToSchool,
				};
		}
	}
}
