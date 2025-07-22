namespace Strogue.Aira.Application.Contracts;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    EntityEntry Entry(object entity);

    #region DbSets

    public DbSet<AuditLog> AuditLog { get; set; }
    public DbSet<Continent> Continent { get; set; }
    public DbSet<Country> Country { get; set; }


    #endregion DbSets
}