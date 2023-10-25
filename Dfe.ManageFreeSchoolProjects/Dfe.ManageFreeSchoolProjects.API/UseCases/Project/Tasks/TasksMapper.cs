using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks;

public static class TasksMapper
{
    private const string Special = "FS - Special";
    private const string AlternativeProvision = "FS - AP";
    private const string FurtherEducation = "FE";
    private const string Mainstream = "FS - Mainstream";
    private const string StudioSchool = "SS";
    private const string UniversityTechnicalCollege = "UTC";

    private const string Primary = "Primary";
    private const string Secondary = "Secondary";
    private const string SixteenToNineteen = "16-19";
    private const string AllThrough = "All-Through";
    public static string MapSchoolType(this SchoolType schoolType)
    {
        return schoolType switch
        {
            SchoolType.Special => Special,
            SchoolType.AlternativeProvision => AlternativeProvision,
            SchoolType.FurtherEducation => FurtherEducation,
            SchoolType.Mainstream => Mainstream,
            SchoolType.StudioSchool => StudioSchool,
            SchoolType.UniversityTechnicalCollege => UniversityTechnicalCollege,
            _ => ""
        };
    }

    public static SchoolType MapSchoolType(this string schoolType) => schoolType switch
    {
        Special => SchoolType.Special,
        AlternativeProvision => SchoolType.AlternativeProvision,
        FurtherEducation => SchoolType.FurtherEducation,
        Mainstream => SchoolType.Mainstream,
        StudioSchool => SchoolType.StudioSchool,
        UniversityTechnicalCollege => SchoolType.UniversityTechnicalCollege,
        _ => SchoolType.None
    };

    public static string MapSchoolPhase(this SchoolPhase schoolPhase)
    {
        return schoolPhase switch
        {
            SchoolPhase.Primary => SchoolPhase.Primary.ToString(),
            SchoolPhase.Secondary => SchoolPhase.Secondary.ToString(),
            SchoolPhase.SixteenToNineteen => SixteenToNineteen,
            SchoolPhase.AllThrough => AllThrough,
            _ => ""
        };
    }
    
    public static SchoolPhase MapSchoolPhase(this string schoolPhase)
    {
        return schoolPhase switch
        {
            Primary => SchoolPhase.Primary, 
            Secondary => SchoolPhase.Secondary, 
            SixteenToNineteen => SchoolPhase.SixteenToNineteen, 
            AllThrough => SchoolPhase.AllThrough,
            _ => SchoolPhase.None
        };
    }
}