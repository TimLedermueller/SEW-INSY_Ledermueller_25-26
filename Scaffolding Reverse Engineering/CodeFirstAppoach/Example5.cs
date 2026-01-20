using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstAppoach;

public class Example5
{
    public int Id {get; set;} //required by default
    public string? Valeue1 {get; set;} //optional because of ?

    public string Valeue2 {get; set;}
    [NotMapped]
    public string Valeue3 {get; set;}
    [Column(TypeName = "varchar(20)")]
    public string Valeue4 {get; set;}
    [MaxLength(25)]
    public string Valeue5 {get; set;}
    [Column("Kathegorie")]
    public string Valeue6 {get; set;}
    public string Valeue7 { get; set;}
    public string Valeue8 { get; set;}
    public string Valeue9 { get; set;}
}