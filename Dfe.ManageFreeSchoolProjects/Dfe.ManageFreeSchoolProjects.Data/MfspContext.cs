using Microsoft.EntityFrameworkCore;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.Data;

public partial class MfspContext : DbContext
{
    private readonly AuditInterceptor _auditInterceptor;

    public MfspContext()
    { 
    }

    public MfspContext(DbContextOptions<MfspContext> options, AuditInterceptor auditInterceptor)
        : base(options)
    {
        _auditInterceptor = auditInterceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_auditInterceptor != null)
        {
            optionsBuilder.AddInterceptors(_auditInterceptor);
        }

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMfspSqlServer("Server=localhost;Database=mfsp;Integrated Security=true;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        // SeedDevelopmentData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
    
    private static void SeedDevelopmentData(ModelBuilder modelBuilder)
    {
        var randomKpi = RandomDataGenerator.GenerateRandomValues<Kpi>();
    
        modelBuilder.Entity<Kpi>().HasData(
            randomKpi
        );
    }
}
