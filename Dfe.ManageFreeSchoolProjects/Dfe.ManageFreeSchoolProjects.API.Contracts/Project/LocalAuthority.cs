namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project;

public record LocalAuthority
{
    public EastMidlands EastMidlands { get; set; }
    public EastOfEngland EastOfEngland { get; set; }
    public London London { get; set; }
    public NorthEast NorthEast { get; set; }
    public NorthWest NorthWest { get; set; }
    public SouthEast SouthEast { get; set; }
    public SouthWest SouthWestType { get; set; }
    public WestMidlands WestMidlands { get; set; }
    public YorkshireAndHumber YorkshireAndHumber { get; set; }
}

public record YorkshireAndHumber
{
}

public record WestMidlands
{
}

public record SouthWest
{
}

public record SouthEast
{
}

public record NorthWest
{
}

public record NorthEast
{
}

public record London
{
}

public record EastOfEngland
{
}

public record EastMidlands
{
}