using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.API.Contracts.ResponseModels;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.Services.BulkEdit
{
    public interface IBulkEditCommitService
    {
        public Task Execute(BulkEditRequest bulkEditCommitRequest);
    }

    public class BulkEditCommitService(MfspApiClient apiClient) : IBulkEditCommitService
    {
        public async Task Execute(BulkEditRequest bulkEditCommitRequest)
        {
            await apiClient.Post<BulkEditRequest, ApiSingleResponseV2<object>> ($"/api/v1/bulkedit/commit/", bulkEditCommitRequest);
        }
    }
}
