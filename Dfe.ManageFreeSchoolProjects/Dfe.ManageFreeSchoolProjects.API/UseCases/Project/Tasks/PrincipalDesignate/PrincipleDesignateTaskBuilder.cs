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

			var principleDesignateTask = new PrincipalDesignateTask();

			if (milestones.FsgPreOpeningMilestonesAppointedPrincipalDesignate == null && milestones.FsgPreOpeningMilestonesPdappActualDateOfCompletion.HasValue)
			{
				principleDesignateTask.TrustAppointedPrincipleDesignate =
					true;
			}

			else
			{
				principleDesignateTask.TrustAppointedPrincipleDesignate =
					milestones.FsgPreOpeningMilestonesAppointedPrincipalDesignate;
			}

			principleDesignateTask.TrustAppointedPrincipleDesignateDate =
				milestones.FsgPreOpeningMilestonesPdappActualDateOfCompletion;
			principleDesignateTask.CommissionedExternalExpertVisitToSchool =
				milestones.FsgPreOpeningMilestonesCommissionedExternalExpertVisitToSchool;
			
			return principleDesignateTask;
		}
	}
}
