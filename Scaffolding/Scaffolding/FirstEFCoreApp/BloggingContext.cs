using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public BloggingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=127.0.0.1;uid=root;pwd=insy;database=demo;");

        optionsBuilder.UseSeeding((context, _) =>
        {
            context.Set<Blog>().RemoveRange(context.Set<Blog>());
            
            context.Set<Blog>().Add(new Blog {Url = "http://test.at"});
            context.Set<Blog>().Add(new Blog {Url = "http://test.de"});
            context.Set<Blog>().Add(new Blog {Url = "http://test.cz"});
            
            var com = new Blog {Url = "http://test.com"};
            context.Set<Blog>().Add(com);
            
            com.Posts.Add(new Post {Content = "Oachkatzl", Title = "asdf"});
            
            context.SaveChanges();
            
        });
    } 
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; } // Foreign Key
    public Blog Blog { get; set; }  // Navigation Property
}