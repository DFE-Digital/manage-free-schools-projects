using Dfe.ManageFreeSchoolProjects.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Dfe.ManageFreeSchoolProjects.Services.Project;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    public class CreateProjectBaseModel : PageModel
    {
        public bool IsUserAuthorised()
        {
            return User.IsInRole(RolesConstants.ProjectRecordCreator);
        }

        public string GetPreviousPage(CreateProjectPageName currentPageName, CreateProjectNavigation navigationCache,
            string routeParameter = "")
        {
            if (navigationCache == CreateProjectNavigation.BackToCheckYourAnswers)
                return RouteConstants.CreateProjectCheckYourAnswers;

            return currentPageName switch
            {
                CreateProjectPageName.ProjectId => RouteConstants.CreateProjectMethod,
                CreateProjectPageName.SchoolName => RouteConstants.CreateProjectId,
                CreateProjectPageName.Region => RouteConstants.CreateProjectSchool,
                CreateProjectPageName.LocalAuthority => RouteConstants.CreateProjectRegion,
                CreateProjectPageName.SearchTrust => RouteConstants.CreateProjectLocalAuthority,
                CreateProjectPageName.ConfirmTrustSearch => RouteConstants.CreateProjectSearchTrust,
                CreateProjectPageName.SchoolType => string.Format(RouteConstants.CreateProjectConfirmTrust, routeParameter),
                CreateProjectPageName.SchoolPhase => RouteConstants.CreateProjectSchoolType,
                CreateProjectPageName.ClassType => RouteConstants.CreateProjectCapacity,
                CreateProjectPageName.Capacity => RouteConstants.CreateNotifyUser,
                CreateProjectPageName.NotifyUser => RouteConstants.CreateClassType,
                CreateProjectPageName.CheckYourAnswers => RouteConstants.CreateNotifyUser,
                _ => throw new ArgumentOutOfRangeException($"Unsupported create project page {currentPageName}")
            };
        }

        public string GetNextPage(CreateProjectPageName currentPageName, string routeParameter = "")
        {
            return currentPageName switch
            {
                CreateProjectPageName.ProjectId => RouteConstants.CreateProjectSchool,
                CreateProjectPageName.SchoolName => RouteConstants.CreateProjectRegion,
                CreateProjectPageName.Region => RouteConstants.CreateProjectLocalAuthority,
                CreateProjectPageName.LocalAuthority => RouteConstants.CreateProjectSearchTrust,
                CreateProjectPageName.SearchTrust => string.Format(RouteConstants.CreateProjectConfirmTrust, routeParameter),
                CreateProjectPageName.ConfirmTrustSearch => RouteConstants.CreateProjectSchoolType,
                CreateProjectPageName.SchoolType => RouteConstants.CreateProjectSchoolPhase,
                CreateProjectPageName.SchoolPhase => RouteConstants.CreateClassType,
                CreateProjectPageName.ClassType => RouteConstants.CreateProjectCapacity,
                CreateProjectPageName.Capacity => RouteConstants.CreateNotifyUser,
                CreateProjectPageName.NotifyUser => RouteConstants.CreateProjectCheckYourAnswers,
                CreateProjectPageName.CheckYourAnswers => RouteConstants.CreateProjectConfirmation,
                _ => throw new ArgumentOutOfRangeException($"Unsupported create project page {currentPageName}")
            };
        }
    }
}