using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproach;

public partial class ExampleContext: DbContext
{
    public ExampleContext()
    {
    }

    public ExampleContext(DbContextOptions<ExampleContext> options) :  base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySQL("server=127.0.0.1;uid=root;pwd=insy;database=demo;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Example4>(entity => { entity.HasKey(e => e.Nr).HasName("PRIMARY"); });

        modelBuilder.Entity<Example6>(entity =>
        {
            entity.Ignore(x => x.Value3);
            entity.Property(e => e.Value4)
                .HasMaxLength(20);
            entity.Property(e => e.Value5)
                .HasColumnType("varchar(20)");
            entity.Property(e => e.Value6)
                .HasColumnName("Wert6");
        });
    }


    public virtual DbSet<Example> Examples { get; set; }
    public virtual DbSet<Example2> Examples2 { get; set; }
    public virtual DbSet<Example3> Examples3 { get; set; }
    public virtual DbSet<Example4> Examples4 { get; set; }
    public virtual DbSet<Example5> Examples5 { get; set; }
    public virtual DbSet<Example6> Examples6 { get; set; }
}