
namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IHeaderRegister<T>
    {
        string IdentifingHeader { get; }
        List<HeaderType<T>> GetHeaders();
    }
}