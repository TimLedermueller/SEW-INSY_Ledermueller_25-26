using Microsoft.EntityFrameworkCore;

namespace CodeFirstAppoach;

public partial class ExampleContent : DbContext
{
    public ExampleContent()
    {

    }

    public ExampleContent(DbContextOptions<ExampleContent> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Port=3307;Database=car_db;User=root;Password=insy;");

    public virtual DbSet<Example> Examples { get; set; }
    public virtual DbSet<Example2> Examples2 { get; set; }
    public virtual DbSet<Example3> Examples3 { get; set; }
    public virtual DbSet<Example4> Examples4 { get; set; }
    public virtual DbSet<Example5> Examples5 { get; set; }
    public virtual DbSet<Example6> Examples6 { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Example4>(entity =>
        {
            //FLUENT API configurations for Database Models
            
            entity.HasKey(e => e.Nr).HasName("PRIMARY");
           
            
        });

        modelBuilder.Entity<Example6>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Ignore(x => x.Valeue3);
            
            entity.Property(x => x.Valeue4)
                .HasColumnType("varchar(20)");
            
            entity.Property(x => x.Valeue5)
                .HasMaxLength(25);

            entity.Property(x => x.Valeue6)
                .HasColumnName("Wert6");
        });
        
        modelBuilder.Entity<Example5>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Valeue6)
                .HasColumnName("Kathegorie")
                .HasMaxLength(255)       
                .IsRequired();             
        });
    }
   
   
}
