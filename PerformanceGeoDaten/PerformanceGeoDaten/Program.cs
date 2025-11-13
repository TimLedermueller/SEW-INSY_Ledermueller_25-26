using System;
using System.Diagnostics;
using MySqlConnector;

class Program
{
    static readonly Random rng = new Random();
    static (double lat, double lon) RandStart()
        => (47.0 + rng.NextDouble()*2.0, 12.0 + rng.NextDouble()*3.0); // grob AT

    static double RandRadiusKm() => 10 + rng.NextDouble()*10; // 10–20 km

    static async System.Threading.Tasks.Task Main()
    {
        var cs = "Server=127.0.0.1;Port=3307;Database=perf;User ID=root;Password=insy;AllowPublicKeyRetrieval=True;SslMode=None;ConnectionTimeout=15;";
        int runs = 1000;  // 100 / 1000 / …

        await using var conn = new MySqlConnection(cs);
        await conn.OpenAsync();

        string sql = @"
SELECT tippId, text, ST_Distance_Sphere(coordinates, POINT(@lon, @lat)) AS distance_m
FROM tipps
WHERE ST_Distance_Sphere(coordinates, POINT(@lon, @lat)) <= @r_km * 1000
ORDER BY distance_m";

        var sw = Stopwatch.StartNew();
        for (int i = 0; i < runs; i++)
        {
            var (lat, lon) = RandStart();
            var r = RandRadiusKm();

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@lat", lat);
            cmd.Parameters.AddWithValue("@lon", lon);
            cmd.Parameters.AddWithValue("@r_km", r);

            // optional: nur zählen, um Overhead zu reduzieren
            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync()) { /* noop */ }
        }
        sw.Stop();
        Console.WriteLine($"Runs: {runs}, total: {sw.Elapsed.TotalSeconds:F2}s, avg: {sw.Elapsed.TotalMilliseconds/runs:F2} ms/q");
    }
}