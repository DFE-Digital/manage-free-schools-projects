using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.BulkEdit
{
    public interface IBulkEditValidateService
    {
        public Task<ApiSingleResponseV2<BulkEditValidateResponse>> Execute(BulkEditValidateRequest bulkEditValidateRequest);
    }

    public class BulkEditValidateService(MfspApiClient apiClient) : IBulkEditValidateService
    {
        public async Task<ApiSingleResponseV2<BulkEditValidateResponse>> Execute(BulkEditValidateRequest bulkEditValidateRequest)
        {
            return await apiClient.Post<BulkEditValidateRequest, ApiSingleResponseV2 <BulkEditValidateResponse>> ($"/api/v1/bulkedit/validate/", bulkEditValidateRequest);
        }
    }
}
