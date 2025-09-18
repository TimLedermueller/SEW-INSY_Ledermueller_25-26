
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        int threadCount = 1;
        string connectionString = "Server=localhost;Database=threads;User ID=root;Password=insy;";
        
        Random rnd = new Random();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 5; i++)
        {
            char c = (char)('A' + rnd.Next(0, 26));
            sb.Append(c);
        }
        sb.Append('%');
        string search = sb.ToString();
        
        // Für "rn" Suche
        // int search = rnd.Next(0, 5);
        
        // string search = "GMCIPQWGXL"

        Thread[] threads = new Thread[threadCount];

        for (int t = 0; t < threadCount; t++)
        {
            //threads[t] = new Thread(() =>
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // string sql = "SELECT Id, RandomString, rn FROM Codes WHERE rn LIKE  @search";
                    // string sql = "SELECT Id, RandomString, rn FROM Codes WHERE RandomString =  @search";
                    string sql = "SELECT Id, RandomString, rn FROM Codes WHERE RandomString LIKE  @search";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", search);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("Id");
                                string rs = reader.GetString("RandomString");
                                int rn = reader.GetInt32("rn");
                                Console.WriteLine($"FOUND ID: {id} - {rs}");
                            }
                        }
                    }
                }
                //});
            }
            //threads[t].Start();
                //threads[t].Join();
        }

            //foreach (var th in threads) th.Join();
            sw.Stop();
            Console.WriteLine($"{sw.Elapsed.TotalMilliseconds} ms, Threads: {threadCount}");
    }
}


/*

using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        const int anzahl = 1_000_000;   
        const int laenge = 12;          
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var rnd = new Random();

        string pfad = "random_strings.txt";

        using var writer = new StreamWriter(pfad, false, Encoding.UTF8);

        for (int i = 1; i <= anzahl; i++)
        {
            string zufall = GenerateRandomString(rnd, alphabet, laenge);
            writer.WriteLine($"{i};{zufall}");
        }

        Console.WriteLine($"Fertig! {anzahl:N0} Zeilen in {Path.GetFullPath(pfad)} gespeichert.");
    }

    static string GenerateRandomString(Random rnd, string chars, int length)
    {
        var sb = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            char c = chars[rnd.Next(chars.Length)];
            sb.Append(c);
        }
        return sb.ToString();
    }
}
*/


using System;
using System.IO;
using System.Text;

namespace CsvGenerator
{
    internal static class Program
    {
        static void Main()
        {
            const int anzahl = 1_000_000;
            const int laenge = 12;
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var rnd = new Random();

            // schreibt ohne BOM:
            var pfad = Path.Combine(AppContext.BaseDirectory, "random_strings.txt");
            using var writer = new StreamWriter(pfad, false, new UTF8Encoding(false));

            for (int i = 1; i <= anzahl; i++)
            {
                writer.WriteLine($"{i};{GenerateRandomString(rnd, alphabet, laenge)}");
            }

            Console.WriteLine($"Fertig! {anzahl:N0} Zeilen in {Path.GetFullPath(pfad)} gespeichert.");
        }

        private static string GenerateRandomString(Random rnd, string chars, int length)
        {
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                sb.Append(chars[rnd.Next(chars.Length)]);
            return sb.ToString();
        }
    }
}


