using OneTomany;

namespace OneTomany;



using Microsoft.EntityFrameworkCore;

public class ClassSubjContextx : DbContext
{
    public DbSet<Classes> Classes { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<ClassSubjectContent> ClassSubjectContent { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=127.0.0.1;port=3306;uid=root;pwd=insy;database=schoolmanagement;",
            new MySqlServerVersion(new Version(8, 0, 36))
        );
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
        
        //DataSeeding
        
        
        modelBuilder.Entity<Classes>().HasData(
            new Classes { Id = 1, Name = "4C" },
            new Classes { Id = 2, Name = "1C" },
            new Classes { Id = 3, Name = "2C" }
        );

        modelBuilder.Entity<Subject>().HasData(
            new Subject { Id = 1, Title = "German" },
            new Subject { Id = 2, Title = "Mathematics" },
            new Subject { Id = 3, Title = "English" },
            new Subject { Id = 4, Title = "Physics" },
            new Subject { Id = 5, Title = "Programming" }
        );
        
        modelBuilder.Entity<ClassSubjectContent>().HasData(
            // 4C
            new ClassSubjectContent { ClassId = 1, SubjectId = 1, Content = "Meinungsrede" },
            new ClassSubjectContent { ClassId = 1, SubjectId = 5, Content = "C#, OOP, APIs" },
            new ClassSubjectContent { ClassId = 1, SubjectId = 2, Content = "Vectors, integrals" },

            // 1C
            new ClassSubjectContent { ClassId = 2, SubjectId = 5, Content = "Basics, algorithms" },
            new ClassSubjectContent { ClassId = 2, SubjectId = 3, Content = "Grammar + vocabulary" },

            // 2C
            new ClassSubjectContent { ClassId = 3, SubjectId = 2, Content = "Equations + functions" },
            new ClassSubjectContent { ClassId = 3, SubjectId = 4, Content = "Mechanics basics" }
        );
    }
}