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

        public const string ViewTrustTask = TaskList + "/trust";
        public const string SearchTrustTask = ViewTrustTask + "/search";
        public const string EditTrustTask = ViewTrustTask + "/edit/{1}";

        public const string ViewConstituency = TaskList + "/constituency";
        public const string SearchConstituency = ViewConstituency + "/search";
        public const string EditConstituency = ViewConstituency + "/edit?search={1}";

        public const string ViewRegionAndLocalAuthorityTask = TaskList + "/region-and-localauthority";
        public const string EditRegion = ViewRegionAndLocalAuthorityTask + "/region/edit";
        public const string EditLocalAuthority = ViewRegionAndLocalAuthorityTask + "/localauthority/edit?region={1}";

        public const string ViewArticlesOfAssociation = TaskList + "/articlesofassociation";
        public const string EditArticlesOfAssociation = ViewArticlesOfAssociation + "/edit";        

        public const string ViewRiskAppraisalMeetingTask = TaskList + "/risk-appraisal-meeting";
        public const string EditRiskAppraisalMeetingTask = ViewRiskAppraisalMeetingTask + "/edit";
        
        public const string CreateProjectMethod = CreateProject + "/method";
        public const string CreateProjectId = CreateProject + "/projectid";
        public const string CreateProjectSchool = CreateProject + "/school";
        public const string CreateProjectSchoolPhase = CreateProject + "/schoolphase";
        public const string CreateProjectRegion = CreateProject + "/region";
        public const string CreateProjectLocalAuthority = CreateProject + "/localauthority";
        public const string CreateProjectCheckYourAnswers = CreateProject + "/checkyouranswers";
        public const string CreateProjectConfirmation = CreateProject + "/confirmation";
        public const string CreateProjectSchoolType = CreateProject + "/school-type";
        public const string CreateProjectSearchTrust = CreateProject + "/trust/search";
        public const string CreateProjectAgeRange = CreateProject + "/age-range";
        public const string CreateProjectConfirmTrust = CreateProject + "/trust/confirm/{0}";
        public const string CreateProjectCapacity = CreateProject + "/capacity";
        public const string CreateNotifyUser = CreateProject + "/notifyuser";
        public const string CreateClassType = CreateProject + "/class-type";
        public const string CreateFormsOfEntry = CreateProject + "/forms-of-entry";
        public const string CreateProjectProvisionalOpeningDate = CreateProject + "/provisional-opening-date";

        public const string ProjectRiskSummary = "/projects/{0}/risk/summary";
        public const string ProjectRiskReview = "/projects/{0}/risk/add/review";
        public const string ProjectRiskConfirmation = "/projects/{0}/risk/add/confirmation?schoolName={1}";
        public const string CreateFaithStatus = CreateProject + "/faith-status";
        public const string CreateFaithType = CreateProject + "/faith-type";
        
        public const string ViewContacts = "/projects/{0}/contacts/other-info-contacts-landing-page";
        public const string ViewTrustChairContact = "/projects/{0}/contacts/other-info-contacts-multi-academy-trust-chair-of-governors-edit";
        public const string ViewSchoolChairContact = "/projects/{0}/contacts/other-info-contacts-school-chair-of-governors-edit";
    }
}