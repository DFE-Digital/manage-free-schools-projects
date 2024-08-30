using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;

public record ProjectGrantLetters
{
    public DateTime? InitialGrantLetterDate { get; set; }

    public DateTime? FinalGrantLetterDate { get; set; }

    public string InitalGrantLetterLink { get; set; }
    
    public string FullGrantLetterLink { get; set; }
    
    public bool? InitialGrantLetterSavedToWorkplaces { get; set; }
    
    public bool? FinalGrantLetterSavedToWorkplaces { get; set; }
    
    public IEnumerable<GrantVariationLetter> VariationLetters { get; set; }
}

public record GrantVariationLetter
{
    public GrantLetterVariation Variation { get; set; }
    public DateTime? LetterDate { get; set; }
    public string LetterLink { get; set; }
    public bool? SavedToWorkplacesFolder { get; set; }
    
    public enum GrantLetterVariation
    {
        NotSet = 0,
        FirstVariation = 1,
        SecondVariation = 2,
        ThirdVariation = 3,
        FourthVariation = 4
    }
}