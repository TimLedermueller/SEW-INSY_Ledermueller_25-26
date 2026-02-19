using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstApproach;

public class Example5
{
    public int Id { get; set; }
    public string Value1 { get; set; }
    public string? Value2 { get; set; }
    [NotMapped]
    public string Value3 { get; set; }
    [Column(TypeName = "varchar(20)")]
    public string Value4 { get; set; }
    [MaxLength(25)]
    public string Value5 { get; set; }
    [Column("Wert6")]
    public string? Value6 { get; set; }
    public string Value7 { get; set; }
    public string Value8 { get; set; }
    public string Value9 { get; set; }
}