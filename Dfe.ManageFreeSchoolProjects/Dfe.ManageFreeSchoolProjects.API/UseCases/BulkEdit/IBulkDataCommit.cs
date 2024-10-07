namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IBulkDataCommit<in TDto> where TDto : IBulkEditDto
    {
        Task Save(IEnumerable<TDto> data);
    }
}
