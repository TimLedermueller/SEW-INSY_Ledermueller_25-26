namespace BlogPost;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; } // Foreign Key
    public Blog Blog { get; set; }  // Navigation Property
}