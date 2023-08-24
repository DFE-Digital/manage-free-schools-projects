using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Dashboard;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Users;
using Dfe.ManageFreeSchoolProjects.API.UseCases.Project;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.UserContext;
using System.Reflection;
using FluentValidation;
using Dfe.ManageFreeSchoolProjects.API.UseCases.ProjectOverview;

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

            services.AddScoped<IGetDashboardAllService, GetDashboardAllService>();
			services.AddScoped<IGetDashboardByUserService, GetDashboardByUserService>();
			services.AddScoped<ICreateProjectService, CreateProject>();
            services.AddScoped<ICreateUserService, CreateUserService>();
			services.AddScoped<IGetProjectOverviewService, GetProjectOverviewService>();
            services.AddValidatorsFromAssembly(Assembly.Load(Assembly.GetExecutingAssembly().FullName));

            return services;
		}
	}
}
