namespace Strogue.Aira.Persistence.Configurations;

public class ContinentConfiguration : IEntityTypeConfiguration<Continent>
{
    public void Configure(EntityTypeBuilder<Continent> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("getdate()");

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);
    }
}