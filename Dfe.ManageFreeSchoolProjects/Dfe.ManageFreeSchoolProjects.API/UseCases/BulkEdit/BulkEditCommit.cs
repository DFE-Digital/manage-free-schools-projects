﻿using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Microsoft.IdentityModel.Tokens;

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

            var IdColumnIndex = request.Headers.FirstOrDefault(x => string.Compare(x.Name, headerRegister.IdentifingHeader, true) == 0).Index;

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

                    if(column.Value.IsNullOrEmpty())
                    {
                        continue;
                    }
                    var headerName = request.Headers.Where(x => x.Index == column.ColumnIndex).FirstOrDefault()?.Name;
                    var header = headers.FirstOrDefault(x => string.Compare(x.Name, headerName, true) == 0);
                    var value = column.Value;
                    header.DataInteration.ApplyToDto(value, currentRow);
                }
            }

            await dataCommit.Save(projects.Values.ToList());
        }
    }
}
