using Dfe.ManageFreeSchoolProjects.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Create.Individual
{
    public class CreateProjectBaseModel : PageModel
    {
        public bool IsUserAuthorised()
        {
            return User.IsInRole(RolesConstants.ProjectRecordCreator);
        }

        public string GetNextPage(CreateProjectPageName pageName)
        {
            return pageName switch
            {
                CreateProjectPageName.LocalAuthority => RouteConstants.CreateProjectSchoolType,
                CreateProjectPageName.SchoolType => RouteConstants.CreateProjectCheckYourAnswers,
                CreateProjectPageName.SearchTrust => RouteConstants.CreateProjectSearchTrust,
                _ => throw new ArgumentOutOfRangeException($"Unsupported create project page {pageName}")
            };
        }

    }
}
