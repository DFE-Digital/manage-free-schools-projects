namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IBulkEditDataRetrieval<TDto> where TDto : IBulkEditDto
    {
        Task<Dictionary<string, TDto>> Retrieve(List<string> projectIds);
    }
}
