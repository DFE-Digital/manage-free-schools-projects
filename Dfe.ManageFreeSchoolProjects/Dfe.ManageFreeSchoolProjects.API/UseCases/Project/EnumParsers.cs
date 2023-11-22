using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project;

public static class EnumParsers
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

    public static ClassType.SixthForm ParseSixthForm(string input)
    {
        return Enum.TryParse<ClassType.SixthForm>(input, out var parsedSixthForm)
            ? parsedSixthForm
            : ClassType.SixthForm.NotSet;
    }

    public static ClassType.Nursery ParseNursery(string input)
    {
        return Enum.TryParse<ClassType.Nursery>(input, out var paredNursery) ? paredNursery : ClassType.Nursery.NotSet;
    }
}