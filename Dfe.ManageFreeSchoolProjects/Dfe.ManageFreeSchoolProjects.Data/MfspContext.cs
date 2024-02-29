using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.Data;

public partial class MfspContext : DbContext
{
    public MfspContext()
    {
    }

    public MfspContext(DbContextOptions<MfspContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMfspSqlServer("Server=localhost,1433;Database=mfsp;User Id=sa;Password=29Feb2024;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(modelBuilder);
    }

}
