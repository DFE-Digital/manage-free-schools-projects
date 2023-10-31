using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.RiskRating;
using System.Collections.Generic;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.RiskRating
{
	public static class RatingLabelBuilder
	{
		public static List<RatingLabel> Build()
		{
			return new List<RatingLabel>()
			{
				new RatingLabel()
				{
					Rating = ProjectRiskRating.Green,
					Label = "<strong class=\"govuk-tag govuk-tag--green\">Green</strong>"
				},
				new RatingLabel()
				{
					Rating = ProjectRiskRating.AmberGreen,
					Label = "<strong class=\"govuk-tag govuk-tag--yellow\">Amber</strong><strong class=\"govuk-tag govuk-tag--green\">Green</strong>"
				},
				new RatingLabel()
				{
					Rating = ProjectRiskRating.AmberRed,
					Label = "<strong class=\"govuk-tag govuk-tag--yellow\">Amber</strong><strong class=\"govuk-tag govuk-tag--red\">Red</strong>"
				},
				new RatingLabel()
				{
					Rating = ProjectRiskRating.Red,
					Label = "<strong class=\"govuk-tag govuk-tag--red\">Red</strong>"
				}
			};
		}
	}

	public class RatingLabel
	{
		public ProjectRiskRating Rating { get; set; }
		public string Label { get; set; }
	}
}
