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

        int iteration = 1;
        
        
        
        for (int t = 0; t < iteration; t++)
            {
                Random rnd = new Random();
                StringBuilder sb = new StringBuilder();
                string connectionString = "Server=localhost;Port=3307;Database=threads;User ID=root;Password=insy;";
                for (int i = 0; i < 5; i++)
                {
                    char c = (char)('A' + rnd.Next(0, 26));
                    sb.Append(c);
                }
                sb.Append("%");
                string search = sb.ToString();
                
               // Für "rn" Suche
                // int search = rnd.Next(0, 5);
                
                // string search = "GMCIPQWGXL"
                
                //Thread[] threads = new Thread[iteration];

            
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
            Console.WriteLine($"{sw.Elapsed.TotalMilliseconds} ms, Iterations: {iteration}");
            
    }
}



