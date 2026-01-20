using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstAppoach;

public class Example6
{
    public int Id {get; set;} //required by default
    public string? Valeue1 {get; set;} //optional because of ?
    public string Valeue2 {get; set;}
    public string Valeue3 {get; set;}
    public string Valeue4 {get; set;}
    public string Valeue5 {get; set;}
    public string? Valeue6 {get; set;}
    public string Valeue7 { get; set;}
    public string Valeue8 { get; set;}
    public string Valeue9 { get; set;}
}