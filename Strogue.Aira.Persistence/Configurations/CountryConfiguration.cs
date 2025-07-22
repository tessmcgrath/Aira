namespace Strogue.Aira.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(e => e.TimeZone)
            .IsRequired()
            .HasMaxLength(75);

        builder.Property(e => e.GmtOffset)
            .IsRequired()
            .HasMaxLength(75);

        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("getdate()");

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);
    }
}