namespace ManytoMany;


using Microsoft.EntityFrameworkCore;

public class ClassSubjContextx : DbContext
{
    public DbSet<Classes> Classes { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<ClassSubjectContent> ClassSubjectContents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=127.0.0.1;uid=root;pwd=insy;database=schoolmanagement;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite Primary Key
        modelBuilder.Entity<ClassSubjectContent>()
            .HasKey(x => new { x.ClassId, x.SubjectId });

        // Relationships
        modelBuilder.Entity<ClassSubjectContent>()
            .HasOne(x => x.Class)
            .WithMany(c => c.ClassSubjects)
            .HasForeignKey(x => x.ClassId);

        modelBuilder.Entity<ClassSubjectContent>()
            .HasOne(x => x.Subject)
            .WithMany(s => s.ClassSubjects)
            .HasForeignKey(x => x.SubjectId);
    }
    
    
}