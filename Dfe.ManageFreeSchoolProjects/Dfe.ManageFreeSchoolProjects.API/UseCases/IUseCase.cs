namespace Dfe.ManageFreeSchoolProjects.API.UseCases
{
    public interface IUseCase<in TRequest, out TResponse>
    {
        TResponse Execute(TRequest request);
    }
}
