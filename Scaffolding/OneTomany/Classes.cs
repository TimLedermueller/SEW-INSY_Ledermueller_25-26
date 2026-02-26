using OneTomany;

namespace OneTomany;

public class Classes
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    //Navigation property M:N
    public List<ClassSubjectContent> ClassSubjects { get; set; } = [];
}