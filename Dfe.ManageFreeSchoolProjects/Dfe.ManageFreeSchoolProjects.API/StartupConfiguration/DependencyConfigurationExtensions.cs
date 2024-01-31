using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.ProjectOverview;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Users;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Email;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.UserContext;
using FluentValidation;
using System.Reflection;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Tasks;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Constituency;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Risk;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.School;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Dates;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RegionAndLocalAuthority;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.RiskAppraisalMeeting;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Constituency;
using System.ComponentModel;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Contacts;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.Trusts;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project.Tasks.ArticlesOfAssociation;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Construct;

namespace Dfe.ManageFreeSchoolProjects.API.StartupConfiguration
{
    public static class DependencyConfigurationExtensions
	{
		public static IServiceCollection AddUseCases(this IServiceCollection services)
		{
			var allTypes = typeof(IUseCase<,>).Assembly.GetTypes();

			foreach (var type in allTypes)
			{
				foreach (var @interface in type.GetInterfaces())
				{
					if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IUseCase<,>))
					{
						if (!type.IsInterface)
						{
							services.AddScoped(@interface, type);
						}
					}

					if (@interface.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IUseCase<,>)))
					{
                        services.AddScoped(@interface, type);
                    }
				}
			}

			return services;
		}

		public static IServiceCollection AddUseCaseAsyncs(this IServiceCollection services)
		{
			var allTypes = typeof(IUseCaseAsync<,>).Assembly.GetTypes();

			foreach (var type in allTypes)
			{
				foreach (var @interface in type.GetInterfaces())
				{
					if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IUseCaseAsync<,>))
					{
						services.AddScoped(@interface, type);
					}
				}
			}

			return services;
		}

		public static IServiceCollection AddApiDependencies(this IServiceCollection services)
		{
			services.AddScoped<IServerUserInfoService, ServerUserInfoService>();
			
			services.AddScoped<ICorrelationContext, CorrelationContext>();

            services.AddScoped<IGetDashboardService, GetDashboardService>();
			services.AddScoped<ICreateProjectService, CreateProject>();
            services.AddScoped<ICreateUserService, CreateUserService>();
			services.AddScoped<IGetProjectOverviewService, GetProjectOverviewService>();
			services.AddScoped<IUpdateProjectByTaskService, UpdateProjectByTaskService>();
            services.AddScoped<IGetProjectByTaskService, GetProjectByTaskService>();
			services.AddScoped<IGetLocalAuthoritiesService, GetLocalAuthoritiesService>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IGetTasksService, GetAllTasksStatusService>();
			services.AddScoped<IGetTaskStatusService, GetTaskStatusService>(); 
			services.AddScoped<IUpdateTaskStatusService, UpdateTaskStatusService>();
			services.AddScoped<ICreateTasksService, CreateTasksService>();
			services.AddScoped<ICreateProjectRiskService,  CreateProjectRiskService>();
			services.AddScoped<IGetProjectRiskService,  GetProjectRiskService>();
			services.AddScoped<IGetTrustByRefService, GetTrustByRefService>();
            services.AddScoped<ISearchTrustByRefService, SearchTrustByRefService>();
            services.AddScoped<ISearchConstituencyService, SearchConstituencyService>();
            services.AddScoped<IGetProjectManagersService, GetProjectManagersService>();
            services.AddScoped<IGetProjectContactsService, GetProjectContactsService>();
            services.AddScoped<IUpdateProjectContactsService, UpdateProjectContactsService>();
			services.AddScoped<IUpdateTaskService, UpdateSchoolTaskService>();
			services.AddScoped<IUpdateTaskService, UpdateDatesTaskService>();
			services.AddScoped<IUpdateTaskService, UpdateRegionAndLocalAuthorityTaskService>();
			services.AddScoped<IUpdateTaskService, UpdateRiskAppraisalMeetingTaskService>();
			services.AddScoped<IUpdateTaskService, UpdateConstituencyTaskService>();
			services.AddScoped<IUpdateTaskService, UpdateTrustTaskService>();
            services.AddScoped<IUpdateTaskService, UpdateArticlesOfAssociationTaskService>();
			services.AddScoped<IGetConstructProjectListService, GetConstructProjectListService>();

            services.AddValidatorsFromAssembly(Assembly.Load(Assembly.GetExecutingAssembly().FullName));

            return services;
		}
	}
}
