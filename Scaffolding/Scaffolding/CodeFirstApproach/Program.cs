// See https://aka.ms/new-console-template for more information

using ManytoMany;
using Microsoft.EntityFrameworkCore;

using ManytoMany;
using Microsoft.EntityFrameworkCore;

using var schoolContext = new ClassSubjContextx();

Console.WriteLine("subjects (without include)");
foreach (var subject in schoolContext.Subjects)
{
    // Ohne Include ist ClassSubjects nicht geladen -> Count ist meist 0 (oder LazyLoading wäre nötig)
    Console.WriteLine($"{subject.Id}, {subject.Title}");
}

Console.WriteLine("subjects with count of classes (with include)");
var subjectsWithClasses = schoolContext.Subjects
    .Include(s => s.ClassSubjects)
    .ThenInclude(cs => cs.Class);

foreach (var subject in subjectsWithClasses)
{
    var classCount = subject.ClassSubjects.Count;
    Console.WriteLine($"{subject.Id}, {subject.Title}, {classCount}");
}