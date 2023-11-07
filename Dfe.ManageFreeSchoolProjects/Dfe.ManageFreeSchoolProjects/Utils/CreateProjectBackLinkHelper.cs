using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Services.Project;

namespace Dfe.ManageFreeSchoolProjects.Utils;

public static class CreateProjectBackLinkHelper
{
    public static string GetBackLink(CreateProjectNavigation navigation, string backPage)
    {
        return navigation == CreateProjectNavigation.BackToCheckYourAnswers
            ? RouteConstants.CreateProjectCheckYourAnswers
            : backPage;
    }
}