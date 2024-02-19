using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.School
{
    public class GetSchoolTaskService : IGetTaskService
    {
        public async Task<GetProjectByTaskResponse> Get(GetTaskServiceParameters parameters)
        {
            var query = parameters.BaseQuery;

            var result = await query.Select(kpi => new GetProjectByTaskResponse()
            {
                School = new()
                {
                    CurrentFreeSchoolName = kpi.ProjectStatusCurrentFreeSchoolName,
                    SchoolType = ProjectMapper.ToSchoolType(kpi.SchoolDetailsSchoolTypeMainstreamApEtc),
                    SchoolPhase = ProjectMapper.ToSchoolPhase(kpi.SchoolDetailsSchoolPhasePrimarySecondary),
                    AgeRange = kpi.SchoolDetailsAgeRange,
                    Gender = EnumParsers.ParseGender(kpi.SchoolDetailsGender),
                    Nursery = EnumParsers.ParseNursery(kpi.SchoolDetailsNursery),
                    SixthForm = EnumParsers.ParseSixthForm(kpi.SchoolDetailsSixthForm),
                    FaithStatus = EnumParsers.ParseFaithStatus(kpi.SchoolDetailsFaithStatus),
                    FaithType = ProjectMapper.ToFaithType(kpi.SchoolDetailsFaithType),
                    OtherFaithType = kpi.SchoolDetailsPleaseSpecifyOtherFaithType,
                    FormsOfEntry = kpi.SchoolDetailsNumberOfFormsOfEntry
                }
            }).FirstOrDefaultAsync();

            return result;
        }
    }
}
