using OneTomany;

namespace OneTomany;

public class Subject
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public List<ClassSubjectContent> ClassSubjects { get; set; } = [];
}