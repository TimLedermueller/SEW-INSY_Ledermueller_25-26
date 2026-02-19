// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
// See https://aka.ms/new-console-template for more information
//Blog sew = new Blog {Id = 1, Topic = "SEW"};
//Post p = new Post {Id = 1, BlogId = 1, Text = "suppa"};

Building bulding1 = new Building{Id = 1, Name = "Bulding1"}; 
Room room1 = new Room{Id = 1, Name = "Room1"};

room1.Bulding = bulding1; //Beziehungen pflegen
Console.WriteLine(room1.Bulding.Name); //Über das Navigationsproperty

//bulding1.Rooms(room1);
bulding1.Rooms.Add(room1);
Console.WriteLine(bulding1.Rooms[0].Name);

//var Blog_of_P = //Liste von Blogs den mit Nummer 1 Suchen

// Principal (parent)
public class Building
{
    public int Id { get; set; }
    public string Name{ get; set; }
    public List<Room> Rooms{ get; } = new List<Room>(); // Collection navigation containing dependents
}   // ICollection -List

// Dependent (child)
public class Room
{
    public int Id { get; set; }
    public string Name{ get; set; }
    public int Roum_id { get; set; } // Optional foreign key property
    
    //Navigationsproperty
    // Optional reference navigation to principal
    public Building Bulding { get; set; } = null; 
}