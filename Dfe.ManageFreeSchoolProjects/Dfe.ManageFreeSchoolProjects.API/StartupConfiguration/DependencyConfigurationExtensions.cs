using Dfe.ManageFreeSchoolProjects.API.UseCases;
using Dfe.ManageFreeSchoolProjects.Data.Gateways.Projects;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.UserContext;

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
			services.AddScoped<IProjectGateway, ProjectGateway>();

			services.AddScoped<IServerUserInfoService, ServerUserInfoService>();
			
			services.AddScoped<ICorrelationContext, CorrelationContext>();

			return services;
		}
	}
}
