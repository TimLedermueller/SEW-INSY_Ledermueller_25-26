using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using var db = new BloggingContext();

var post = db.Posts.Include(x => x.Blog).FirstOrDefault();
Console.WriteLine($"Post: {post?.Title}:{post?.Content}/ from {post.Blog.Url}");

var post2 = db.Posts.FirstOrDefault();
Console.WriteLine($"Post: {post2?.Title}:{post2?.Content}/ from {post2.Blog.Url}");