using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IBulkEditValidation
    {
        Task<BulkEditValidateResponse> Execute(BulkEditValidateRequest request);
    }
}