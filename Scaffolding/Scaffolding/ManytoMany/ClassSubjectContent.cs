namespace ManytoMany;

using Microsoft.EntityFrameworkCore;

public class ClassSubjContextx : DbContext
{
    public DbSet<Classes> Classes { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=127.0.0.1;uid=root;pwd=insy;database=;");
        
        
    }
}