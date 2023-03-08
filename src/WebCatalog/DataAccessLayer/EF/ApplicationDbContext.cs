namespace DataAccessLayer.EF;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) 
        : base(context)
    {
        Database.EnsureCreated();
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Entities.OperatingSystem> OperatingSystems { get; set; }
    public DbSet<Entities.Type> Types { get; set; }
    public DbSet<SystemRequirement> SystemRequirements { get; set; }
    public DbSet<Program> Programs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Program>(buildAction =>
        {
            buildAction
                .ToTable("Programs");

            buildAction
                .Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("CHAR(100)")
                .IsRequired();

            buildAction
                .Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("TEXT")
                .IsRequired(false);

            buildAction
                .Property(x => x.License)
                .HasColumnName("License")
                .HasColumnType("BIT");

            buildAction
                .Property(x => x.ReleaseDate)
                .HasColumnName("RealeseDate")
                .HasConversion<DateOnlyConverter, DateOnlyComparer>()
                .IsRequired();

            buildAction
                .Property(x => x.Version)
                .HasColumnName("Version")
                .HasColumnType("CHAR(60)")
                .IsRequired();

            buildAction
                .Property(x => x.UsingConnection)
                .HasColumnName("UsingConnection")
                .HasColumnType("BIT");

            buildAction
                .Property(x => x.MultiUserMode)
                .HasColumnName("MultiUserMode")
                .HasColumnType("BIT");

            buildAction
                .Property(x => x.CrossPlatform)
                .HasColumnName("CrossPlatform")
                .HasColumnType("BIT");

            buildAction
                .Property(x => x.Logo)
                .HasColumnName("Logo")
                .HasColumnType("VARBINARY(MAX)");

            buildAction
                .HasOne(x => x.Company)
                .WithMany(x => x.Programs);

            buildAction
                .HasOne(x => x.Type)
                .WithMany(x => x.Programs);

            buildAction
                .HasMany(x => x.Requirements)
                .WithMany(x => x.Programs)
                .UsingEntity(x => x.ToTable("RequirementProgram"));

            buildAction
                .HasMany(x => x.OperatingSystems)
                .WithMany(x => x.Programs)
                .UsingEntity(x => x.ToTable("OperatingSystemProgram"));
        });

        modelBuilder.Entity<Company>(buildAction =>
        {
            buildAction
                .ToTable("Companies");

            buildAction
                .HasKey(x => x.Id)
                .HasName("CompanyId");

            buildAction
                .Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("CHAR(100)")
                .IsRequired();

            buildAction
                .Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("TEXT")
                .IsRequired(false);
        });

        modelBuilder.Entity<Entities.Type>(buildAction =>
        {
            buildAction
                .ToTable("Types");

            buildAction
                .HasKey(x => x.Id)
                .HasName("TypeId");

            buildAction
                .Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("CHAR(100)")
                .IsRequired();
        });

        modelBuilder.Entity<Entities.OperatingSystem>(buildAction =>
        {
            buildAction
                .ToTable("OperationSystems");

            buildAction
                .HasKey(x => x.Id)
                .HasName("OperationSystemId");

            buildAction
                .Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("CHAR(100)")
                .IsRequired();
        });

        modelBuilder.Entity<SystemRequirement>(buildAction =>
        {
            buildAction
                .ToTable("SystemRequirements");

            buildAction
                .HasKey(x => x.Id)
                .HasName("SystemRequirementId");

            buildAction
                .Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("CHAR(100)")
                .IsRequired();
        });
    }
}
