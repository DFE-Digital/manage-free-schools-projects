using Dfe.ManageFreeSchoolProjects.Constants;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Dfe.ManageFreeSchoolProjects.Services.Project;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    public class CreateProjectBaseModel : PageModel
    {
        protected internal string BackLink { get; set; }
        protected readonly ICreateProjectCache _createProjectCache;

        public CreateProjectBaseModel(ICreateProjectCache createProjectCache)
        {
            _createProjectCache = createProjectCache;
        }

        public bool IsUserAuthorised()
        {
            return User.IsInRole(RolesConstants.ProjectRecordCreator);
        }

        public string GetPreviousPage(CreateProjectPageName currentPageName,
            string routeParameter = "")
        {
            return currentPageName switch
            {
                CreateProjectPageName.LocalAuthority => RouteConstants.CreateProjectRegion,
                CreateProjectPageName.FaithType => RouteConstants.CreateFaithStatus,
                CreateProjectPageName.ConfirmTrustSearch => RouteConstants.CreateProjectSearchTrust,
                _ => DefaultPreviousRoute(currentPageName, routeParameter)
            };           
        }

        private string DefaultPreviousRoute(CreateProjectPageName currentPageName, string routeParameter)
        {
            var cache = _createProjectCache.Get();

            if (cache.ReachedCheckYourAnswers)
                return RouteConstants.CreateProjectCheckYourAnswers;

            return currentPageName switch
            {
                CreateProjectPageName.ProjectId => RouteConstants.CreateProjectMethod,
                CreateProjectPageName.SchoolName => RouteConstants.CreateProjectId,
                CreateProjectPageName.Region => RouteConstants.CreateProjectSchool,
                CreateProjectPageName.LocalAuthority => RouteConstants.CreateProjectRegion,
                CreateProjectPageName.SearchTrust => RouteConstants.CreateProjectLocalAuthority,
                CreateProjectPageName.ConfirmTrustSearch => RouteConstants.CreateProjectSearchTrust,
                CreateProjectPageName.SchoolType => string.Format(RouteConstants.CreateProjectConfirmTrust,
                    routeParameter),
                CreateProjectPageName.SchoolPhase => RouteConstants.CreateProjectSchoolType,
                CreateProjectPageName.ClassType => RouteConstants.CreateProjectSchoolPhase,
                CreateProjectPageName.AgeRange => RouteConstants.CreateClassType,
                CreateProjectPageName.Capacity => RouteConstants.CreateProjectAgeRange,
                CreateProjectPageName.FormsOfEntry => RouteConstants.CreateProjectCapacity,
                CreateProjectPageName.FaithStatus => RouteConstants.CreateFormsOfEntry,
                CreateProjectPageName.FaithType => RouteConstants.CreateFaithStatus,
                CreateProjectPageName.ProvisionalOpeningDate => RouteConstants.CreateFaithType,
                CreateProjectPageName.NotifyUser => RouteConstants.CreateProjectProvisionalOpeningDate,
                CreateProjectPageName.CheckYourAnswers => RouteConstants.CreateNotifyUser,
                _ => throw new ArgumentOutOfRangeException($"Unsupported create project page {currentPageName}")
            };
        }

        public string GetNextPage(CreateProjectPageName currentPageName, string routeParameter = "")
        {
            return currentPageName switch
            {
                CreateProjectPageName.FaithStatus => NextFaithStatus(),
                CreateProjectPageName.Region => RouteConstants.CreateProjectLocalAuthority,
                CreateProjectPageName.SearchTrust => string.Format(RouteConstants.CreateProjectConfirmTrust, routeParameter),
                _ => DefaultNextRoute(currentPageName, routeParameter)
            };
        }

        private string NextFaithStatus()
        {
            var cache = _createProjectCache.Get();

            var faithStatus = cache.ReachedCheckYourAnswers ? cache.PreviousFaithStatus : cache.FaithStatus;

            if (faithStatus == API.Contracts.Project.Tasks.FaithStatus.None)
                return cache.ReachedCheckYourAnswers ? RouteConstants.CreateProjectCheckYourAnswers
                                                     : RouteConstants.CreateProjectProvisionalOpeningDate;

            return RouteConstants.CreateFaithType;
        }

        private string DefaultNextRoute(CreateProjectPageName currentPageName, string routeParameter)
        {
            var cache = _createProjectCache.Get();

            if (cache.ReachedCheckYourAnswers)
                return RouteConstants.CreateProjectCheckYourAnswers;

            return currentPageName switch
            {
                CreateProjectPageName.ProjectId => RouteConstants.CreateProjectSchool,
                CreateProjectPageName.SchoolName => RouteConstants.CreateProjectRegion,
                CreateProjectPageName.Region => RouteConstants.CreateProjectLocalAuthority,
                CreateProjectPageName.LocalAuthority => RouteConstants.CreateProjectSearchTrust,
                CreateProjectPageName.SearchTrust => string.Format(RouteConstants.CreateProjectConfirmTrust,
                    routeParameter),
                CreateProjectPageName.ConfirmTrustSearch => RouteConstants.CreateProjectSchoolType,
                CreateProjectPageName.SchoolType => RouteConstants.CreateProjectSchoolPhase,
                CreateProjectPageName.SchoolPhase => RouteConstants.CreateClassType,
                CreateProjectPageName.ClassType => RouteConstants.CreateProjectAgeRange,
                CreateProjectPageName.AgeRange => RouteConstants.CreateProjectCapacity,
                CreateProjectPageName.Capacity => RouteConstants.CreateFormsOfEntry,
                CreateProjectPageName.FormsOfEntry => RouteConstants.CreateFaithStatus,
                CreateProjectPageName.FaithStatus => RouteConstants.CreateFaithType,
                CreateProjectPageName.FaithType => RouteConstants.CreateProjectProvisionalOpeningDate,
                CreateProjectPageName.ProvisionalOpeningDate => RouteConstants.CreateNotifyUser,
                CreateProjectPageName.NotifyUser => RouteConstants.CreateProjectCheckYourAnswers,
                CreateProjectPageName.CheckYourAnswers => RouteConstants.CreateProjectConfirmation,
                _ => throw new ArgumentOutOfRangeException($"Unsupported create project page {currentPageName}")
            };
        }
    }
}