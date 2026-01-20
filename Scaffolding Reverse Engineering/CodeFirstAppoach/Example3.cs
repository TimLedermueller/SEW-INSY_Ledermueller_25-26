using Microsoft.EntityFrameworkCore;

namespace CodeFirstAppoach;

[PrimaryKey(nameof(Nr))]
public class Example3
{
    public int Nr {get; set;}
    public string Valeue {get; set;}
}