using Ardalis.GuardClauses;
using Dfe.ManageFreeSchoolProjects.Data.Conventions;
using Dfe.ManageFreeSchoolProjects.Data.Models.Projects;
using Dfe.ManageFreeSchoolProjects.UserContext;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.Data
{
    public partial class ProjectsDbContext : DbContext
	{
		private readonly IServerUserInfoService _userInfoService;

		public ProjectsDbContext()
		{
		}

		public ProjectsDbContext(DbContextOptions<ProjectsDbContext> options, IServerUserInfoService userInfoService)
			: base(options)
		{
			Guard.Against.Null(userInfoService, nameof(userInfoService));
			_userInfoService = userInfoService;
		}

		public virtual DbSet<Project> Projects { get; set; }

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseConcernsSqlServer("Data Source=127.0.0.1;Initial Catalog=local_trams_test_db;persist security info=True;User id=sa; Password=StrongPassword905");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

			base.OnModelCreating(modelBuilder);
		}
	}
}