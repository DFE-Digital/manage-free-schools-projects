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
            switch (pageName)
            {
                case CreateProjectPageName.LocalAuthority:
                    return RouteConstants.CreateProjectSchoolType;
                case CreateProjectPageName.SchoolType:
                    return RouteConstants.CreateProjectCheckYourAnswers;
                default:
                    throw new ArgumentOutOfRangeException($"Unsupported create project page {pageName}");
            }
        }

    }
}
