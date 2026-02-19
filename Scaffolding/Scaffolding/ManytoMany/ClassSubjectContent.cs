namespace ManytoMany;

public class ClassSubjectContent
{
    public int ClassId { get; set; }
    public int SubjectId { get; set; }

    public string Content { get; set; } = string.Empty;

    public Classes? Class { get; set; }
    public Subject? Subject { get; set; }
}