using Microsoft.EntityFrameworkCore;
using OneTomany;

using var db = new ClassSubjContextx();
await db.Database.MigrateAsync();

var classes = await db.Classes
    .Include(c => c.ClassSubjects)
    .ThenInclude(cs => cs.Subject)
    .ToListAsync();

foreach (var c in classes)
{
    Console.WriteLine($"Class {c.Name}:");
    foreach (var link in c.ClassSubjects)
        Console.WriteLine($"  - {link.Subject.Title} | {link.Content}");
    Console.WriteLine();
}