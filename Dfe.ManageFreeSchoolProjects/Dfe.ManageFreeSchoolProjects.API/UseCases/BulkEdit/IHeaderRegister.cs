
namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IHeaderRegister<T> where T : IBulkEditDto
    {
        string IdentifingHeader { get; }
        List<HeaderType<T>> GetHeaders();
    }
}