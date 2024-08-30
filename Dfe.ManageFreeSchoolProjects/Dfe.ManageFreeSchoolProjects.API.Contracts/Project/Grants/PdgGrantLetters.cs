using System.ComponentModel;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;

public record ProjectGrantLetters
{
    public IEnumerable<GrantLetter> Letters { get; set; }
}

public record GrantLetter
{
    public LetterType Type { get; set; }
    public DateTime? LetterDate { get; set; }
    public string LetterLink { get; set; }
    public bool? SavedToWorkplacesFolder { get; set; }

    public enum LetterType
    {
        NotSet = 0,
        Initial = 1,
        Full = 2,
        [Description("First")]
        FirstVariation = 3,
        [Description("Second")]
        SecondVariation = 4,
        [Description("Third")]
        ThirdVariation = 5,
        [Description("Fourth")]
        FourthVariation = 6
    }
}

public class PdgGrantLetters
{
    public DateTime? PdgGrantLetterDate { get; set; }

    public string PdgGrantLetterLink { get; set; }

    public DateTime? FirstPdgGrantVariationDate { get; set; }

    public string FirstPdgGrantVariationLink { get; set; }

    public DateTime? SecondPdgGrantVariationDate { get; set; }

    public string SecondPdgGrantVariationLink { get; set; }

    public DateTime? ThirdPdgGrantVariationDate { get; set; }

    public string ThirdPdgGrantVariationLink { get; set; }

    public DateTime? FourthPdgGrantVariationDate { get; set; }

    public string FourthPdgGrantVariationLink { get; set; }
}