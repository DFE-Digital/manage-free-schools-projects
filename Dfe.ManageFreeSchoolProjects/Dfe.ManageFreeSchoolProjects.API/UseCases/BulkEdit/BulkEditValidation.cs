using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Microsoft.IdentityModel.Tokens;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public class BulkEditValidation<TDto>(IHeaderRegister<TDto> headerRegister, IBulkEditDataRetrieval<TDto> dataRetrieval) : IBulkEditValidation where TDto : IBulkEditDto
    {
        public async Task<BulkEditValidateResponse> Execute(BulkEditRequest request)
        {
            var headers = headerRegister.GetHeaders();

            var response = new BulkEditValidateResponse();

            response.Headers = request.Headers;

            response.ValidationResultRows = new();

            var IdColumnIndex = request.Headers.FirstOrDefault(x => x.Name == headerRegister.IdentifingHeader).Index;

            // Validate headers
            var projectIds = request.Rows.Select(x => x.Columns.Where(y => y.ColumnIndex == IdColumnIndex).Select(y => y.Value).FirstOrDefault()).ToList();
            var projects = await dataRetrieval.Retrieve(projectIds);

            //Validate row by row
            var headerMap = new Dictionary<int, HeaderType<TDto>>();
            foreach (var header in request.Headers)
            {
                var headerInfo = headers.FirstOrDefault(x => x.Name == header.Name);
                headerMap.Add(header.Index, headerInfo);
            }

            foreach (var row in request.Rows)
            {
                var currentRow = projects[row.Columns.FirstOrDefault(x => x.ColumnIndex == IdColumnIndex).Value];

                var validationRowResult = new ValidationRowInfo()
                {
                    FileRowIndex = row.FileRowIndex,
                    Columns = new()
                };

                foreach (var column in row.Columns)
                {
                    if (column.Value.IsNullOrEmpty())
                    {
                        continue;
                    }

                    var header = headerMap[column.ColumnIndex];
                    var validationResult = header.Type.Execute(currentRow, column.Value);
                    if (!validationResult.IsValid)
                    {
                        validationRowResult.Columns.Add(new ValueChangeInfo()
                        {
                            ColumnIndex = column.ColumnIndex,
                            CurrentValue = header.GetFromDto(currentRow),
                            NewValue = column.Value,
                            Error = validationResult.errorMessage
                        });

                    }
                    else
                    {
                        validationRowResult.Columns.Add(new ValueChangeInfo()
                        {
                            ColumnIndex = column.ColumnIndex,
                            CurrentValue = header.GetFromDto(currentRow),
                            NewValue = column.Value
                        });
                    }
                }

                response.ValidationResultRows.Add(validationRowResult);
            }

            return response;
        }
    }
}
