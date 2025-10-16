using System;
using System.Diagnostics;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        string connStr =
            "Server=127.0.0.1;Port=3307;Database=performancevergleich;User ID=root;Password=insy;AllowPublicKeyRetrieval=True;SslMode=None;ConnectionTimeout=15;";

        using var conn = new MySqlConnection(connStr);
        conn.Open();
        Console.WriteLine("✅ Verbindung hergestellt!");

        string sqlUnion = @"
            SELECT product_id FROM products_a WHERE category = 'Electronics'
            UNION
            SELECT product_id FROM products_b WHERE category = 'Books';";

        string sqlUnionAll = @"
            SELECT product_id FROM products_a WHERE category = 'Electronics'
            UNION ALL
            SELECT product_id FROM products_b WHERE category = 'Books';";

        var runs = 5;
        Console.WriteLine("Starte Benchmark…");

        var tUnion = Benchmark(conn, sqlUnion, runs, "UNION");
        var tUnionAll = Benchmark(conn, sqlUnionAll, runs, "UNION ALL");

        Console.WriteLine($"\nErgebnis:");
        Console.WriteLine($"UNION:     {tUnion:F2} ms");
        Console.WriteLine($"UNION ALL: {tUnionAll:F2} ms");
    }

    static double Benchmark(MySqlConnection conn, string sql, int runs, string label)
    {
        double total = 0;
        for (int i = 1; i <= runs; i++)
        {
            var sw = Stopwatch.StartNew();
            using var cmd = new MySqlCommand(sql, conn);
            using var r = cmd.ExecuteReader();
            while (r.Read()) { }
            sw.Stop();
            Console.WriteLine($"{label} – Lauf {i}: {sw.Elapsed.TotalMilliseconds:F2} ms");
            total += sw.Elapsed.TotalMilliseconds;
        }
        return total / runs;
    }
}