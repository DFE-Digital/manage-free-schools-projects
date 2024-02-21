using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using System.ComponentModel;
using System.Reflection;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Reports
{
    public static class ProjectReportBuilder
    {
        public static ProjectReport Build(ProjectReportBuilderParameters parameters)
        {
            var result = new ProjectReport();

            foreach (var project in parameters.Projects)
            {
                var tasks = new List<ProjectTaskInformation>
                {
                    new ProjectTaskInformation { Task = project.Dates, TaskName = "Dates", Section = "Setting-up" },
                    new ProjectTaskInformation { Task = project.School, TaskName = "School", Section = "Setting-up" },
                    new ProjectTaskInformation { Task = project.Trust, TaskName = "Trust", Section = "Setting-up" },
                    new ProjectTaskInformation { Task = project.RegionAndLocalAuthority, TaskName = "Region and local authority", Section = "Setting-up" },
                    new ProjectTaskInformation { Task = project.Constituency, TaskName = "Constituency", Section = "Setting-up" },
                    new ProjectTaskInformation { Task = project.RiskAppraisalMeeting, TaskName = "Risk appraisal meeting", Section = "Setting-up" },
                    new ProjectTaskInformation { Task = project.KickOffMeeting, TaskName = "Kick-off meeting", Section = "Pre-opening" },
                    new ProjectTaskInformation { Task = project.ArticlesOfAssociation, TaskName = "Articles of association", Section = "Pre-opening" },
                    new ProjectTaskInformation { Task = project.FinancePlan, TaskName = "Finance plan", Section = "Pre-opening" }
                };

                if (result.Headers.Count == 0)
                {
                    result.Headers = BuildHeaderRows(tasks);
                }

                result.Projects.Add(BuildProjectRow(tasks));
            }

            return result;
        }

        private static List<ProjectHeaderRow> BuildHeaderRows(List<ProjectTaskInformation> projectTaskInformation)
        {
            var result = new List<ProjectHeaderRow>();

            foreach (var task in projectTaskInformation)
            {
                var headerRows = BuildHeaderRowsForTask(task);

                result.AddRange(headerRows);
            }

            return result;
        }   

        private static List<ProjectHeaderRow> BuildHeaderRowsForTask(ProjectTaskInformation projectTaskInformation)
        {
            var taskType = projectTaskInformation.Task.GetType();
            var taskProperties = taskType.GetProperties();

            var result = taskProperties.Select(p => new ProjectHeaderRow()
            {
                Section = projectTaskInformation.Section,
                TaskName = projectTaskInformation.TaskName,
                ColumnName = p.Name
            });

            return result.ToList();
        }

        private static ProjectDataRow BuildProjectRow(List<ProjectTaskInformation> projectTaskInformation)
        {
            var result = new ProjectDataRow();

            foreach (var task in projectTaskInformation)
            {
                var values = BuildDataRowForTask(task);

                result.Values.AddRange(values);
            }

            return result;
        }

        private static List<ProjectDataRowValue> BuildDataRowForTask(ProjectTaskInformation projectTaskInformation)
        {
            var taskType = projectTaskInformation.Task.GetType();
            var taskProperties = taskType.GetProperties();

            var result = taskProperties.Select(p => new ProjectDataRowValue()
            {
                Value = ConvertValue(p.GetValue(projectTaskInformation.Task), p)
            }).ToList();

            return result;
        }

        private static string ConvertValue(object value, PropertyInfo propertyInfo)
        {
            var emptyValue = "EMPTY";

            if (value == null)
            {
                return emptyValue;
            }

            if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
            {
                return ((DateTime)value).ToString("dd/MM/yyyy");
            }

            if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
            {
                return (bool)value ? "Yes" : "No";
            }

            if (propertyInfo.PropertyType.IsEnum || Nullable.GetUnderlyingType(propertyInfo.PropertyType)?.IsEnum == true)
            {
                var enumDescription = value.ToDescription();

                if (enumDescription == "NotSet")
                {
                    return emptyValue;
                }

                return value.ToDescription();
            }

            return value.ToString();
        }
    }

    public class ProjectReportBuilderParameters
    {
        public List<GetProjectByTaskResponse> Projects { get; set; }
    }

    public class ProjectTaskInformation
    {
        public object Task { get; set; }
        public string TaskName { get; set; }
        public string Section { get; set; }
    }

    public class ProjectReport
    {
        public List<ProjectHeaderRow> Headers { get; set; } = new();
        public List<ProjectDataRow> Projects { get; set; } = new();
    }

    public class ProjectHeaderRow
    {
        public string Section { get; set; }

        public string TaskName { get; set; }

        public string ColumnName { get; set; }
    }

    public class ProjectDataRow
    {
        public List<ProjectDataRowValue> Values { get; set; } = new();
    }

    public class ProjectDataRowValue
    {
        public string Value { get; set; }
    }
}
