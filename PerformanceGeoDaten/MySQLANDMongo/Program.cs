using System;
using MySqlConnector;
using MongoDB.Bson;
using MongoDB.Driver;

/*
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
*/

// See https://aka.ms/new-console-template for more information

using System;
using MySql.Data.MySqlClient;
using MongoDB.Bson;
using MongoDB.Driver;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;

Console.WriteLine("Starte Export von MySQL nach MongoDB...");



string mysqlConnString =
    "Server=localhost;Database=geo_niederoesterreich;Uid=root;Pwd=insy;";

using var mysqlConn = new MySqlConnection(mysqlConnString);
mysqlConn.Open();
Console.WriteLine("[MySQL] Verbunden.");



var mongoClient = new MongoClient("mongodb://localhost:27017");
var mongoDb = mongoClient.GetDatabase("geo_niederoesterreich");
var collection = mongoDb.GetCollection<BsonDocument>("tipps");

Console.WriteLine("[MongoDB] Verbunden.");



string sql = "SELECT tippId, text, lat, lon FROM tipps;";
using var cmd = new MySqlCommand(sql, mysqlConn);
using var reader = cmd.ExecuteReader();

int count = 0;


while (reader.Read())
{
    int tippId = reader.GetInt32("tippId");
    string text = reader["text"]?.ToString() ?? "";
    double lat = reader.GetDouble("lat");
    double lon = reader.GetDouble("lon");

    var doc = new BsonDocument
    {
        { "tippId", tippId },
        { "text", text },
        { "lat", lat },
        { "lon", lon },

        // GeoJSON Format (für 2dsphere Index)
        { "location", new BsonDocument
            {
                { "type", "Point" },
                { "coordinates", new BsonArray { lon, lat } }   // Wichtig: GEO = LON, LAT
            }
        }
    };

    collection.InsertOne(doc);
    count++;
}



Console.WriteLine($"Export abgeschlossen: {count} Dokumente erfolgreichübertragen.");