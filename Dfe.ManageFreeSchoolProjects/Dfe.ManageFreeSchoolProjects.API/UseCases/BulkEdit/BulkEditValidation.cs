using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.Data;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public class BulkEditValidation<TDto>(IHeaderRegister<TDto> headerRegister, IBulkEditDataRetrieval<TDto> dataRetrieval) : IBulkEditValidation where TDto : IBulkEditDto
    {
        public async Task<BulkEditValidateResponse> Execute(BulkEditValidateRequest request)
        {
            var headers = headerRegister.GetHeaders();

            var response = new BulkEditValidateResponse();

            response.Headers = request.Headers;

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
                var validRow = true;
                var currentRow = projects[row.Columns.FirstOrDefault(x => x.ColumnIndex == IdColumnIndex).Value];

                foreach (var column in row.Columns)
                {
                    var header = headerMap[column.ColumnIndex];
                    var validationResult = header.Type.Execute(currentRow, column.Value);
                    if (!validationResult.IsValid)
                    {
                        validRow = false;

                        if (response.InvalidRows == null)
                        {
                            response.InvalidRows = new List<InvalidRowInfo>();
                        }

                        var existingRow = response.InvalidRows.FirstOrDefault(x => x.FileRowIndex == row.FileRowIndex);

                        if (existingRow == null)
                        {
                            response.InvalidRows.Add(new InvalidRowInfo()
                            {
                                FileRowIndex = row.FileRowIndex,
                                Errors = new List<ErrorInfo>()
                            {
                                new ErrorInfo()
                                {
                                    ColumnIndex = column.ColumnIndex,
                                    Error = validationResult.errorMessage
                                }
                            }
                            });
                        }
                        else
                        {
                            existingRow.Errors.Add(new ErrorInfo()
                            {
                                ColumnIndex = column.ColumnIndex,
                                Error = validationResult.errorMessage
                            });
                        }
                    }
                }

                if (validRow)
                {
                    if (response.ValidRows == null)
                    {
                        response.ValidRows = new List<ValidRowInfo>();
                    }

                    response.ValidRows.Add(new ValidRowInfo()
                    {
                        FileRowIndex = row.FileRowIndex,
                        Columns = row.Columns
                        .Where(x => headerMap[x.ColumnIndex].GetFromDto(currentRow) != x.Value)
                        .Select(x => new ValueChangeInfo()
                        {
                            ColumnIndex = x.ColumnIndex,
                            CurrentValue = headerMap[x.ColumnIndex].GetFromDto(currentRow),
                            NewValue = x.Value
                        }).ToList()
                    });
                }

            }

            return response;
        }
    }
}
