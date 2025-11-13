using System;
using System.Diagnostics;
using System.Globalization;
using MySql.Data.MySqlClient;


string connStr = "Server=localhost;Database=geo_niederoesterreich;User ID=root;Password=insy;";


Random rnd = new Random();
int runs = 100;

Stopwatch sw = new Stopwatch();
int totalHits = 0;


CultureInfo ci = CultureInfo.InvariantCulture;

using (var conn = new MySqlConnection(connStr))
{
    conn.Open();
    
    string sql = @"
        SELECT tippID,
               ST_Distance_Sphere(
                   coordinates,
                   POINT(@lon, @lat)
               ) AS distance_m
        FROM tipps
        WHERE ST_Distance_Sphere(
                  coordinates,
                  POINT(@lon, @lat)
              ) <= @radiusMeters;
    ";

    using (var cmd = new MySqlCommand(sql, conn))
    {
       
        cmd.Parameters.Add("@lon", MySqlDbType.Double);
        cmd.Parameters.Add("@lat", MySqlDbType.Double);
        cmd.Parameters.Add("@radiusMeters", MySqlDbType.Double);

        sw.Start();

        for (int i = 0; i < runs; i++)
        {
            int radiusKm = rnd.Next(10, 21);
            double radiusMeters = radiusKm * 1000;

            double lat = 47.0 + rnd.NextDouble() * 2.0;  // 47–49°
            double lon = 14.0 + rnd.NextDouble() * 3.0;  // 14–17°

          
            cmd.Parameters["@lon"].Value = lon;
            cmd.Parameters["@lat"].Value = lat;
            cmd.Parameters["@radiusMeters"].Value = radiusMeters;

            using (var r = cmd.ExecuteReader())
            {
                int hitCountThisRun = 0;

                while (r.Read())
                {
                    // Wenn du Details brauchst, hier auslesen:
                    // int id = r.GetInt32("tippID");
                    // double dist = r.GetDouble("distance_m");
                    hitCountThisRun++;
                }

                totalHits += hitCountThisRun;
                // Für Performance lieber im Loop nix schreiben
                // Console.WriteLine($"Run {i+1}: {hitCountThisRun} Treffer");
            }
        }

        sw.Stop();
    }
}

Console.WriteLine($"Runs:        {runs}");
Console.WriteLine($"Total hits:  {totalHits}");
Console.WriteLine($"Total time:  {sw.ElapsedMilliseconds} ms");
Console.WriteLine($"Avg/query:   {sw.ElapsedMilliseconds / (double)runs:F2} ms");
