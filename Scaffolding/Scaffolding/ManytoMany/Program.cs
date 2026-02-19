// See https://aka.ms/new-console-template for more information

using ManytoMany;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Starting...");

using var db = new ClassSubjContextx();
await db.Database.MigrateAsync();

Console.WriteLine("Database migrated.");
