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
