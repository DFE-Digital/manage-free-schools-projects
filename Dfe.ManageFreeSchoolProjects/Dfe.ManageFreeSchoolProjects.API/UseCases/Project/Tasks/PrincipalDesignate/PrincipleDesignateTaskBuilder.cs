using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.PrincipalDesignate
{
	public static class PrincipalDesignateTaskBuilder
	{
		public static PrincipalDesignateTask Build(Milestones milestones)
		{
			if (milestones == null)
			{
				return new PrincipalDesignateTask();
			}

			return new PrincipalDesignateTask()
			{
				TrustAppointedPrincipleDesignate =
					milestones.FsgPreOpeningMilestonesAppointedPrincipalDesignate,
				TrustAppointedPrincipleDesignateDate =
					milestones.FsgPreOpeningMilestonesPdappActualDateOfCompletion,
				CommissionedExternalExpertVisitToSchool =
					milestones.FsgPreOpeningMilestonesCommissionedExternalExpertVisitToSchool,
			};
		}
	}
}
