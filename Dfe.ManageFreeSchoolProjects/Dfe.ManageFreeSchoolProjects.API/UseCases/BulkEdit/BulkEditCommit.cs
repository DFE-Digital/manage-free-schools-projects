using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.Data;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IBulkEditCommit
    {
        Task Execute(BulkEditRequest request);
    }

    public class BulkEditCommit<TDto>(
            IHeaderRegister<TDto> headerRegister,
            IBulkEditDataRetrieval<TDto> dataRetrieval,
            IBulkDataCommit<TDto> dataCommit
            ) : IBulkEditCommit where TDto : IBulkEditDto
    {
        public async Task Execute(BulkEditRequest request)
        {
            var headers = headerRegister.GetHeaders();

            var IdColumnIndex = request.Headers.FirstOrDefault(x => x.Name == headerRegister.IdentifingHeader).Index;

            var projectIds = request.Rows.Select(x => x.Columns.Where(y => y.ColumnIndex == IdColumnIndex).Select(y => y.Value).FirstOrDefault()).ToList();
            var projects = await dataRetrieval.Retrieve(projectIds);

            foreach (var row in request.Rows)
            {
                var currentRow = projects[row.Columns.FirstOrDefault(x => x.ColumnIndex == IdColumnIndex).Value];

                foreach (var column in row.Columns)
                {
                    if (column.ColumnIndex == IdColumnIndex)
                    {
                        continue;
                    }

                    var header = headers.FirstOrDefault(x => x.Name == request.Headers[column.ColumnIndex].Name);
                    var value = column.Value;
                    header.SetToDto(value, currentRow);
                }
            }

            await dataCommit.Save(projects.Values.ToList());
        }
    }
}
