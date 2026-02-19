namespace ManytoMany;

public class Classes
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public List<ClassSubjectContent> ClassSubjects { get; set; } = [];
}