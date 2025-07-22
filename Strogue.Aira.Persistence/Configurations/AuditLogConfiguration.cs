namespace Strogue.Aira.Persistence.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.Property(e => e.UserId)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(e => e.EntityName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Action)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(e => e.Action)
            .IsRequired()
            .HasMaxLength(4000);
    }
}