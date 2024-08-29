namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Grants;

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