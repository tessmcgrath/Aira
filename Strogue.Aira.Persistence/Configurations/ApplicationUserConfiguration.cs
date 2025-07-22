namespace Strogue.Aira.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(e => e.TenantId)
            .HasDefaultValue(1);

        builder.Property(e => e.PositionId)
            .HasDefaultValue(1);

        builder.Property(e => e.FirstName)
            .HasMaxLength(75);

        builder.Property(e => e.LastName)
            .HasMaxLength(100);
    }

}