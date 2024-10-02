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

			principleDesignateTask.TrustAppointedPrincipalDesignate =
					milestones.FsgPreOpeningMilestonesAppointedPrincipalDesignate;
			principleDesignateTask.ActualDatePrincipalDesignateAppointed =
				milestones.FsgPreOpeningMilestonesPdappActualDateOfCompletion;
			principleDesignateTask.CommissionedExternalExpertVisitToSchool =
				milestones.FsgPreOpeningMilestonesCommissionedExternalExpertVisitToSchool;
			principleDesignateTask.ExpectedDatePrincipalDesignateAppointed = milestones.FsgPreOpeningMilestonesPdappForecastDate;
			
			return principleDesignateTask;
		}
	}
}
