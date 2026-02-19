using Scaffolding;

using (var context = new DemoContext())
{
    foreach (var item in context.Persons)
    {
        Console.WriteLine(item);
    }
}