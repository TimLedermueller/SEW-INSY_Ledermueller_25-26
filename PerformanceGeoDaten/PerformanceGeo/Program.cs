// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;

class GeoPerformanceCountOnly
{
    static void Main()
    {
        var random = new Random();
        const int RUNS = 100;

        string connStr = "server=localhost;userid=root;password=MeinSicheresPasswort;database=geographic_data";

        // ================================================================
        // UNIVERSAL MYSQL BENCHMARK
        // ================================================================
        void RunMySQLTest(string label, string forceIndex = "")
        {
            Console.WriteLine($"\n=== MySQL Geo-Test ({label}) ===");

            try
            {
                using var conn = new MySqlConnection(connStr);
                conn.Open();

                Stopwatch sw = Stopwatch.StartNew();
                long total = 0;

                for (int i = 0; i < RUNS; i++)
                {
                    int radiusKm = 15;
                    double radiusMeters = radiusKm * 1000;

                    double lon = 47.0;
                    double lat = 14.0;

                    string sql = $@"
                        SELECT COUNT(*) 
                        FROM tipps {forceIndex}
                        WHERE ST_Distance_Sphere(
                            coordinates,
                            ST_GeomFromText(CONCAT('POINT(', @lon, ' ', @lat, ')'), 4326)
                        ) <= @radiusMeters;
                    ";

                    using var cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@lat", lat);
                    cmd.Parameters.AddWithValue("@lon", lon);
                    cmd.Parameters.AddWithValue("@radiusMeters", radiusMeters);

                    total += Convert.ToInt64(cmd.ExecuteScalar());
                }

                sw.Stop();

                Console.WriteLine($"{label}: Gesamtanzahl der gefundenen Objekte: {total}");
                Console.WriteLine($"{label}: Dauer: {sw.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim MySQL-Test ({label}): {ex.Message}");
            }
        }

        // ================================================================
        // 3 GETRENNTE MYSQL TESTFÄLLE
        // ================================================================
        RunMySQLTest("Mit Index (wenn MySQL ihn nützen kann)", "USE INDEX(g)");
        RunMySQLTest("Ohne Index (MySQL entscheidet selbst)", "");
        RunMySQLTest("Force Index (MySQL wird gezwungen)", "FORCE INDEX(g)");

        // ================================================================
        // MongoDB
        // ================================================================
        Console.WriteLine("\n=== MongoDB Geo-Test: Anzahl Objekte in der Nähe ===");
        var client = new MongoClient("mongodb://root:insy@localhost:27017/?authSource=admin");
        var database = client.GetDatabase("geo_data");
        var collection = database.GetCollection<BsonDocument>("tipps");

        // 2dsphere-Index sicherstellen
        collection.Indexes.CreateOne(
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Geo2DSphere("coordinates")
            )
        );

        Stopwatch sw2 = Stopwatch.StartNew();
        long totalMongo = 0;

        for (int i = 0; i < RUNS; i++)
        {
            int radiusKm = random.Next(10, 21);
            double lat = 47.0 + random.NextDouble() * 2;
            double lon = 14.0 + random.NextDouble() * 3;
            double radiusRadians = radiusKm / 6378.1; // Erdradius ~6378 km

            var filter = Builders<BsonDocument>.Filter.GeoWithinCenterSphere(
                "coordinates", lon, lat, radiusRadians
            );

            totalMongo += collection.CountDocuments(filter);
        }

        sw2.Stop();
        Console.WriteLine($"MongoDB: Gesamtanzahl der gefundenen Objekte: {totalMongo}");
        Console.WriteLine($"MongoDB: {RUNS} Läufe in {sw2.ElapsedMilliseconds} ms");

        Console.WriteLine("\nVergleich abgeschlossen!"); 
    }
}