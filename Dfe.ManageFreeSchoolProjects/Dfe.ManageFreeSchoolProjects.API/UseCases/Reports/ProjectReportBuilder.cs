using Dfe.ManageFreeSchoolProjects.API.Extensions;
using System.Reflection;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Reports
{
    public static class ProjectReportBuilder
    {
        public static ProjectReport Build(List<ProjectReportSourceData> projectReportSourceData)
        {
            var result = new ProjectReport();

            const string aboutTheProjectSection = "About the project";
            const string settingUpSection = "Setting-up";
            const string referenceNumbersSection = "Reference numbers";
            const string pdgSection = "Project development grant (PDG)";
            const string preOpeningSection = "Pre-opening";
            const string signOffPreparationSection = "Sign-off preparation";
            const string gettingReadyToOpenSection = "Getting ready to open";
            const string afterOpeningSection = "After opening";
            
            foreach (var project in projectReportSourceData)
            {
                 var tasks = new List<ProjectTaskInformation> 
                 {
                    new() { Task = project.ProjectReferenceData, TaskName = "Reference data", Section = aboutTheProjectSection },
                    new() { Task = project.TaskInformation.Dates, TaskName = "Dates", Section = settingUpSection },
                    new() { Task = project.TaskInformation.School, TaskName = "School", Section = settingUpSection },
                    new() { Task = project.TaskInformation.Trust, TaskName = "Trust", Section = settingUpSection },
                    new() { Task = project.TaskInformation.RegionAndLocalAuthority, TaskName = "Region and local authority", Section = settingUpSection },
                    new() { Task = project.TaskInformation.Constituency, TaskName = "Constituency", Section = settingUpSection },
                    new() { Task = project.TaskInformation.RiskAppraisalMeeting, TaskName = "Risk appraisal meeting", Section = settingUpSection },
                    new() { Task = project.TaskInformation.ReferenceNumbers, TaskName = "Reference numbers", Section = referenceNumbersSection },
                    new() { Task = project.TaskInformation.PDGDashboard, TaskName = "Project development grant (PDG)", Section = pdgSection },
                    new() { Task = project.TaskInformation.KickOffMeeting, TaskName = "Kick-off meeting", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.FundingAgreement, TaskName = "Funding agreement", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.FundingAgreementHealthCheck, TaskName = "Funding agreement health check", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.FundingAgreementSubmission, TaskName = "Funding agreement submission", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.ArticlesOfAssociation, TaskName = "Articles of association", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.AdmissionsArrangements, TaskName = "Admissions Arrangements", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.GovernancePlan, TaskName = "Governance plan", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.FinancePlan, TaskName = "Finance plan", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.EqualitiesAssessment, TaskName = "Equalities assessment", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.StatutoryConsultation, TaskName = "Statutory consultation", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.PrincipalDesignate, TaskName = "Principal Designate", Section = preOpeningSection },
                    new() { Task = project.TaskInformation.Gias, TaskName = "Gias", Section = signOffPreparationSection },
                    new() { Task = project.TaskInformation.EducationBrief, TaskName = "Education brief", Section = signOffPreparationSection },
                    new() { Task = project.TaskInformation.EvidenceOfAcceptedOffers, TaskName = "Accepted offers evidence", Section = gettingReadyToOpenSection },
                    new() { Task = project.TaskInformation.ImpactAssessment, TaskName = "Impact assessment", Section = gettingReadyToOpenSection },
                    new() { Task = project.TaskInformation.OfstedInspection, TaskName = "Ofsted pre-registration", Section = gettingReadyToOpenSection },
                    new() { Task = project.TaskInformation.ApplicationsEvidence, TaskName = "Applications evidence", Section = gettingReadyToOpenSection },
                    new() { Task = project.TaskInformation.MovingToOpen, TaskName = "Moving to open", Section = gettingReadyToOpenSection },
                    new() { Task = project.TaskInformation.FinalFinancePlan, TaskName = "Final finance plan", Section = gettingReadyToOpenSection },
                    new() { Task = project.TaskInformation.PupilNumbersChecks, TaskName = "Pupil numbers checks", Section = gettingReadyToOpenSection },
                    new() { Task = project.TaskInformation.CommissionedExternalExpert, TaskName = "External expert visit", Section = afterOpeningSection },
                    new() { Task = project.TaskInformation.DueDiligenceChecks, TaskName = "Due diligence checks", Section = gettingReadyToOpenSection },
                    new() { Task = project.TaskInformation.ReadinessToOpenMeetingTask, TaskName = "Readiness to open meeting", Section = gettingReadyToOpenSection }
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

        public class ProjectTaskInformation
        {
            public object Task { get; set; }
            public string TaskName { get; set; }
            public string Section { get; set; }
        }
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

