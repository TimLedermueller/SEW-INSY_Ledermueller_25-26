using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproach;

[PrimaryKey(nameof(Nr))]
public class Example3
{
    public int Nr { get; set; }
    public string Value {get;set;}
}