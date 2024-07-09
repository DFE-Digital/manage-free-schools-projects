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

			return new PrincipleDesignateTask()
			{
				TrustAppointedPrincipleDesignate =
					milestones.FsgPreOpeningMilestonesPdappActualDateOfCompletion.HasValue,
				TrustAppointedPrincipleDesignateDate =
					milestones.FsgPreOpeningMilestonesPdappActualDateOfCompletion,
				CommissionedExternalExpertVisitToSchool =
					milestones.FsgPreOpeningMilestonesCommissionedExternalExpertVisitToSchool,
			};
		}
	}
}
