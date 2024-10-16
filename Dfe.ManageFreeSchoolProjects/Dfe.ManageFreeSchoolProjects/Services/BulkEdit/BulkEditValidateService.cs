using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.BulkEdit
{
    public interface IBulkEditValidateService
    {
        public Task<ApiSingleResponseV2<BulkEditValidateResponse>> Execute(BulkEditRequest bulkEditValidateRequest);
    }

    public class BulkEditValidateService(MfspApiClient apiClient) : IBulkEditValidateService
    {
        public async Task<ApiSingleResponseV2<BulkEditValidateResponse>> Execute(BulkEditRequest bulkEditValidateRequest)
        {
            return await apiClient.Post<BulkEditRequest, ApiSingleResponseV2 <BulkEditValidateResponse>> ("/api/v1/bulkedit/validate/", bulkEditValidateRequest);
        }
    }
}
