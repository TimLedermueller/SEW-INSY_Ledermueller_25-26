using System;
using System.Collections.Generic;

namespace Scaffolding;

public partial class Person
{
    public int Id { get; set; }

    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public override string ToString()
    {
        return Id + " " +Vorname + " " + Nachname;
    }
}
