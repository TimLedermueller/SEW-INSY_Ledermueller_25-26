using Microsoft.EntityFrameworkCore;

namespace ManytoMany;

public class Program
{
    public static async Task Main()
    {
        Console.WriteLine("Starting...");

        using var db = new ClassSubjContextx();
        await db.Database.MigrateAsync();

        Console.WriteLine("Database migrated.");

        var subjects = await db.Subjects
            .Include(s => s.ClassSubjects)
            .ThenInclude(cs => cs.Class)   // wenn dein Property anders heißt: cs.Classes
            .ToListAsync();

        foreach (var s in subjects)
            Console.WriteLine($"{s.Id} {s.Title} (classes: {s.ClassSubjects.Count})");
    }
}