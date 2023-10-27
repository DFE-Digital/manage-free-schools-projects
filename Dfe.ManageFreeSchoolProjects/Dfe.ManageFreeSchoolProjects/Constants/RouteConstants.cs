namespace Dfe.ManageFreeSchoolProjects.Constants
{
    public static class RouteConstants
    {
        public const string ProjectOverview = "/projects/{0}/overview";
        public const string TaskList = "/projects/{0}/tasks";
        public const string CreateProject = "/project/create";

        public const string ViewSchoolTask = TaskList + "/school";
        public const string EditSchoolTask = ViewSchoolTask + "/edit";
        public const string ViewDatesTask = TaskList + "/dates";
        public const string EditDatesTask = ViewDatesTask + "/edit";

        public const string CreateProjectMethod = CreateProject + "/method";
        public const string CreateProjectId = CreateProject + "/projectid";
        public const string CreateProjectSchool = CreateProject + "/school";
        public const string CreateProjectLA = CreateProject + "/localauthority";
        public const string CreateProjectCheckYourAnswers = CreateProject + "/checkyouranswers";
        public const string CreateProjectConfirmation = CreateProject + "/confirmation";
        

    }
}
