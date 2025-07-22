namespace Strogue.Aira.Persistence.Contexts
{
    public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ILoggedInUserService loggedInUserService,
    IConfiguration configuration)
    : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
    {
        private readonly ILoggedInUserService _loggedInUserService = loggedInUserService;
        private readonly IConfiguration _configuration = configuration;


        #region DbSets
        public DbSet<AuditLog> AuditLog { get; set; }
        public DbSet<Continent> Continent { get; set; }
        public DbSet<Country> Country { get; set; }

        #endregion DbSets



        #region Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Debugger.IsAttached)
            {
                optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
            }

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                .ToList();

            foreach (var modifiedEntity in modifiedEntities)
            {
                var auditLog = new AuditLog
                {
                    UserId = _loggedInUserService.UserId,
                    TenantId = _loggedInUserService.TenantId,
                    EntityName = modifiedEntity.Entity.GetType().Name,
                    Action = modifiedEntity.State.ToString(),
                    Timestamp = DateTime.Now,
                    Changes = modifiedEntity.State == EntityState.Deleted
                        ? "Deleted entity"
                        : string.Join(", ", modifiedEntity.Properties
                            .Where(p => p.IsModified)
                            .Select(p => $"{p.Metadata.Name}: {p.OriginalValue} -> {p.CurrentValue}"))
                };

                // Add the audit log to the AuditLog DbSet
                AuditLog.Add(auditLog);
            }

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _loggedInUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    case EntityState.Deleted:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion Overrides
    }
}
