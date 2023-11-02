using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks;

public static class TaskParsers
{
    public static FaithStatus ParseFaithStatus(string input)
    {
        return Enum.TryParse<FaithStatus>(input, out var faithStatus) ? faithStatus : FaithStatus.NotSet;
    }

    public static FaithType ParseFaithType(string input)
    {
        return Enum.TryParse<FaithType>(input, out var faithType) ? faithType : FaithType.NotSet;
    }
        
    public static Gender ParseGender(string input)
    {
        return Enum.TryParse<Gender>(input, out var gender) ? gender : Gender.NotSet;
    }
}