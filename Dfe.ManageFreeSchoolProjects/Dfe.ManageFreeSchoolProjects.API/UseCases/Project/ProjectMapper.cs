using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using SchoolType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.SchoolType;
using ProjectStatusType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.ProjectStatus;
using ProjectCancelledReasonType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.ProjectCancelledReason;
using ProjectWithdrawnReasonType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.ProjectWithdrawnReason;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project
{
    public static class ProjectMapper
    {
        public static SchoolType ToSchoolType(string schoolType)
        {
            return schoolType switch
            {
                "FS - AP" => SchoolType.AlternativeProvision,
                "FS - Special" => SchoolType.Special,
                "SS" => SchoolType.StudioSchool,
                "FS - Mainstream" => SchoolType.Mainstream,
                "UTC" => SchoolType.UniversityTechnicalCollege,
                "FE" => SchoolType.FurtherEducation,
                _ => SchoolType.NotSet
            };
        }

        public static string ToSchoolType(SchoolType? schoolType)
        {
            return schoolType switch
            {
                SchoolType.AlternativeProvision => "FS - AP",
                SchoolType.Special => "FS - Special",
                SchoolType.StudioSchool => "SS",
                SchoolType.Mainstream => "FS - Mainstream",
                SchoolType.UniversityTechnicalCollege => "UTC",
                SchoolType.FurtherEducation => "FE",
                _ => "NotSet"
            };
        }

        public static SchoolPhase ToSchoolPhase(string schoolPhase)
        {
            return schoolPhase switch
            {
                "Primary" => SchoolPhase.Primary,
                "Secondary" => SchoolPhase.Secondary,
                "16-19" => SchoolPhase.SixteenToNineteen,
                "16 to 19" => SchoolPhase.SixteenToNineteen,
                "All-Through" => SchoolPhase.AllThrough,
                "All-through" => SchoolPhase.AllThrough,
                _ => SchoolPhase.NotSet
            };
        }

        public static string ToSchoolPhase(SchoolPhase schoolPhase)
        {
            return schoolPhase switch
            {
                SchoolPhase.Primary => "Primary",
                SchoolPhase.Secondary => "Secondary",
                SchoolPhase.SixteenToNineteen => "16-19",
                SchoolPhase.AllThrough => "All-Through",
                _ => "NotSet"
            };
        }

        public static FaithType ToFaithType(string faithTypeDescription)
        {
            return faithTypeDescription switch
            {
                "Church of England" => FaithType.ChurchOfEngland,
                "Greek Orthodox" => FaithType.GreekOrthodox,
                "Roman Catholic" => FaithType.RomanCatholic,
                _ => EnumParsers.ParseFaithType(faithTypeDescription)
            };
        }
        
        public static TrustType ToTrustType(string trustTypeDescription)
        {
            return trustTypeDescription switch
            {
                "Standalone" => TrustType.SingleAcademyTrust,
                "MAT" => TrustType.MultiAcademyTrust,
                _ => EnumParsers.ParseTrustType(trustTypeDescription)
            };
        }
        
        public static ProjectStatusType ToProjectStatusType(string projectStatus)
        {
            return projectStatus?.ToLower() switch
            {
                "pre-opening" => ProjectStatusType.Preopening,
                "open" => ProjectStatusType.Open,
                "closed" => ProjectStatusType.Closed,
                "cancelled during pre-opening" => ProjectStatusType.Cancelled,
                "cancelled" => ProjectStatusType.Cancelled,
                "withdrawn during pre-opening" => ProjectStatusType.WithdrawnInPreOpening,
                "withdrawn in pre-opening" => ProjectStatusType.WithdrawnInPreOpening,
                "rejected at application stage" => ProjectStatusType.Rejected,
                "application competition stage" => ProjectStatusType.ApplicationCompetitionStage,
                "application stage" => ProjectStatusType.ApplicationStage,
                "open free school - not included in figures" => ProjectStatusType.OpenNotIncludedInFigures,
                "pre-opening - not included in the figures" => ProjectStatusType.PreopeningNotIncludedInFigures,
                "withdrawn at application stage" => ProjectStatusType.WithdrawnDuringApplication,
                _ => ProjectStatusType.Preopening,
            };
        }

        public static string FromProjectStatusType(ProjectStatusType projectStatus)
        {
            return projectStatus switch
            {
                ProjectStatusType.Preopening => "Pre-opening",
                ProjectStatusType.Open => "Open",
                ProjectStatusType.Closed => "Closed",
                ProjectStatusType.Cancelled => "Cancelled during pre-opening",
                ProjectStatusType.WithdrawnInPreOpening => "Withdrawn in pre-opening",
                ProjectStatusType.Rejected => "Rejected at application stage",
                ProjectStatusType.ApplicationCompetitionStage => "Application Competition stage",
                ProjectStatusType.ApplicationStage => "Application stage",
                ProjectStatusType.OpenNotIncludedInFigures => "Open free school - Not included in figures", 
                ProjectStatusType.PreopeningNotIncludedInFigures => "Pre-opening - Not included in the figures",
                ProjectStatusType.WithdrawnDuringApplication => "Withdrawn at application stage",
                _ => throw new ArgumentOutOfRangeException(nameof(projectStatus), projectStatus, null)
            };
        }

        public static ProjectCancelledReasonType ToProjectCancelledReasonType(string projectCancelledReason)
        {
            return projectCancelledReason?.ToLower() switch
            {
                "educational" => ProjectCancelledReasonType.Educational,
                "governance" => ProjectCancelledReasonType.Governance,
                "planning" => ProjectCancelledReasonType.Planning,
                "procurement / construction" => ProjectCancelledReasonType.ProcurementConstruction,
                "property" => ProjectCancelledReasonType.Property,
                "pupil numbers / viability" => ProjectCancelledReasonType.PupilNumbersViability,
                "trust not content with site option" => ProjectCancelledReasonType.TrustNotContentWithSiteOption,
                "trust not willing to open in temporary accommodation" => ProjectCancelledReasonType.TrustNotWillingToOpenInTemporaryAccommodation,
                _ => ProjectCancelledReasonType.NotSet
            };
        }

        public static string FromProjectCancelledReasonType(ProjectCancelledReasonType projectCancelledReason)
        {
            return projectCancelledReason switch
            {
                ProjectCancelledReasonType.Educational => "educational",
                ProjectCancelledReasonType.Governance => "governance",
                ProjectCancelledReasonType.Planning => "planning",
                ProjectCancelledReasonType.ProcurementConstruction => "procurement / construction",
                ProjectCancelledReasonType.Property => "property",
                ProjectCancelledReasonType.PupilNumbersViability => "pupil numbers / viability",
                ProjectCancelledReasonType.TrustNotContentWithSiteOption => "trust not content with site option",
                ProjectCancelledReasonType.TrustNotWillingToOpenInTemporaryAccommodation => "trust not willing to open in temporary accommodation",
                _ => ""
            };
        }

        public static ProjectWithdrawnReasonType ToProjectWithdrawnReasonType(string projectWithdrawnReason)
        {
            return projectWithdrawnReason?.ToLower() switch
            {
                "educational" => ProjectWithdrawnReasonType.Educational,
                "governance" => ProjectWithdrawnReasonType.Governance,
                "planning" => ProjectWithdrawnReasonType.Planning,
                "procurement / construction" => ProjectWithdrawnReasonType.ProcurementConstruction,
                "property" => ProjectWithdrawnReasonType.Property,
                "pupil numbers / viability" => ProjectWithdrawnReasonType.PupilNumbersViability,
                "trust not content with site option" => ProjectWithdrawnReasonType.TrustNotContentWithSiteOption,
                "trust not willing to open in temporary accommodation" => ProjectWithdrawnReasonType.TrustNotWillingToOpenInTemporaryAccommodation,
                _ => ProjectWithdrawnReasonType.NotSet
            };
        }

        public static string FromProjectWithdrawnReasonType(ProjectWithdrawnReasonType projectWithdrawnReason)
        {
            return projectWithdrawnReason switch
            {
                ProjectWithdrawnReasonType.Educational => "educational",
                ProjectWithdrawnReasonType.Governance => "governance",
                ProjectWithdrawnReasonType.Planning => "planning",
                ProjectWithdrawnReasonType.ProcurementConstruction => "procurement / construction",
                ProjectWithdrawnReasonType.Property => "property",
                ProjectWithdrawnReasonType.PupilNumbersViability => "pupil numbers / viability",
                ProjectWithdrawnReasonType.TrustNotContentWithSiteOption => "trust not content with site option",
                ProjectWithdrawnReasonType.TrustNotWillingToOpenInTemporaryAccommodation => "trust not willing to open in temporary accommodation",
                _ => ""
            };
        }
    }
}