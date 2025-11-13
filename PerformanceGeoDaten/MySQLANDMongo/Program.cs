using System;
using MySqlConnector;
using MongoDB.Bson;
using MongoDB.Driver;

class Program
{
    static void Main(string[] args)
    {
        string mysqlConn =
            "Server=127.0.0.1;Port=3307;Database=tipps;User ID=root;Password=insy;AllowPublicKeyRetrieval=True;SslMode=None;";
        // MongoDB Verbindung
        var mongoClient = new MongoClient("mongodb://localhost:27017/");
        var mongoDb = mongoClient.GetDatabase("freizeit");
        var mongoCollection = mongoDb.GetCollection<BsonDocument>("tipp");

        Console.WriteLine("Export startet …");

        using (var con = new MySqlConnection(mysqlConn))
        {
            con.Open();

            string sql = @"
                SELECT 
                    tippId,
                    text,
                    Lat,
                    Lon
                FROM tipps;
            ";

            using (var cmd = new MySqlCommand(sql, con))
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    int id = rdr.GetInt32("tippId");
                    string text = rdr.GetString("text");
                    double lat = rdr.GetDouble("Lat");
                    double lon = rdr.GetDouble("Lon");

                    // GeoJSON location
                    var geoPoint = new BsonDocument
                    {
                        { "type", "Point" },
                        { "coordinates", new BsonArray { lon, lat } }
                    };

                    // Dokument für MongoDB
                    var doc = new BsonDocument
                    {
                        { "tippId", id },
                        { "text", text },
                        { "location", geoPoint },
                        { "exportedAt", DateTime.UtcNow }
                    };

                    mongoCollection.InsertOne(doc);
                }
            }
        }

        Console.WriteLine("Export erfolgreich abgeschlossen!");
    }
}
