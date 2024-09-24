using Dfe.ManageFreeSchoolProjects.API.Contracts.BulkEdit;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.BulkEdit
{
    public interface IBulkEditDto
    {
        string Identifier { get; }
    }

    public interface IBulkEditDataRetrieval<TDto> where TDto : IBulkEditDto
    {
        Task<Dictionary<string, TDto>> Retrieve(List<string> projectIds);
    }

    public class BulkEditDataRetrieval(MfspContext context) : IBulkEditDataRetrieval<BulkEditDto>
    {
        public async Task<Dictionary<string, BulkEditDto>> Retrieve(List<string> projectIds)
        {
            return await context.Kpi.Where(x => projectIds.Contains(x.ProjectStatusProjectId))
                .Select(x => new BulkEditDto
                {
                    ProjectId = x.ProjectStatusProjectId,
                    SchoolName = x.ProjectStatusCurrentFreeSchoolName
                })
                .ToDictionaryAsync(k => k.ProjectId);
        }
    }

    public class BulkEditProcess<TDto>(IHeaderRegister<TDto> headerRegister, IBulkEditDataRetrieval<TDto> dataRetrieval, MfspContext context) where TDto: IBulkEditDto
    {   
        public async Task<BulkEditValidateResponse> Execute(BulkEditValidateRequest request)
        {
            var headers = headerRegister.GetHeaders();

            var response = new BulkEditValidateResponse();

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

                foreach (var column in row.Columns)
                {
                    var header = headerMap[column.ColumnIndex];
                    var validationResult = header.Type.Execute(column.Value);
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

                if(validRow)
                {
                    if (response.ValidRows == null)
                    {
                        response.ValidRows = new List<ValidRowInfo>();
                    }

                    //TODO - need to get project id from variable column index
                    var currentRow = projects[row.Columns.FirstOrDefault(x => x.ColumnIndex == IdColumnIndex).Value];

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
