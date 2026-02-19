// See https://aka.ms/new-console-template for more information

Blog sew = new Blog {Id = 1, Topic = "SEW"};
Post p = new Post {Id = 1, BlogId = 1, Text = "suppa"};


p.Blog = sew; //Beziehungen pflegen
Console.WriteLine(p.Blog.Topic); //Über das Navigationsproperty

sew.Posts.Add(p);
Console.WriteLine(sew.Posts[0].Text);

//var Blog_of_P = //Liste von Blogs den mit Nummer 1 Suchen

// Principal (parent)
public class Blog
{
    public int Id { get; set; }
    public string Topic { get; set; }
    public List<Post> Posts { get; } = new List<Post>(); // Collection navigation containing dependents
}   // ICollection -List

// Dependent (child)
public class Post
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int BlogId { get; set; } // Optional foreign key property
    
    //Navigationsproperty
    public Blog Blog { get; set; } = null;  // Optional reference navigation to principal
}


