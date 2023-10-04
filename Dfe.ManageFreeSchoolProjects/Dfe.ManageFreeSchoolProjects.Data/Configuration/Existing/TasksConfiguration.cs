using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dfe.ManageFreeSchoolProjects.Data.Configuration.Existing;

public partial class KpiConfiguration : IEntityTypeConfiguration<Tasks>
{
    public void Configure(EntityTypeBuilder<Tasks> builder)
    {
        builder.HasKey(e => e.Rid);
        builder.ToTable("Tasks", "dbo");

        var taskConverter = new EnumToStringConverter<TaskName>();
        
        builder.Property(e => e.TaskName)
            .IsRequired()
            .HasMaxLength(30)
            .IsUnicode(false)
            .HasColumnName("TaskName")
            .HasConversion(taskConverter);
        
        var statusConverter = new EnumToStringConverter<Status>();
        
        builder.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasColumnName("Status")
            .HasConversion(statusConverter);
    }
}