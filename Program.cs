using System;
using System.Diagnostics;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        string connStr =
            "Server=127.0.0.1;Port=3307;Database=perf;User ID=root;Password=insy;AllowPublicKeyRetrieval=True;SslMode=None;ConnectionTimeout=15;";


        using var conn = new MySqlConnection(connStr);
        conn.Open();
        Console.WriteLine(" Verbindung hergestellt!");

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
/*
Starte Benchmark.
    UNION - Lauf 1: 2917,20 ms
UNION - Lauf 2: 2285,90 ms
UNION - Lauf 3: 2386,61 ms
UNION - Lauf 4: 2518,70 ms
UNION - Lauf 5: 2681,39 ms
    UNION ALL - Lauf 1: 1469,44 ms
    UNION ALL - Lauf 2: 1527,74 ms
    UNION ALL - Lauf 3: 1548,68 ms
    UNION ALL - Lauf 4: 1644,91 ms
    UNION ALL - Lauf 5: 1558,06 ms

Ergebnis:
UNION:     2557,96 ms
    UNION ALL: 1549,76 ms

CREATE SCHEMA IF NOT EXISTS perf;

CREATE DATABASE IF NOT EXISTS perf
  DEFAULT CHARACTER SET utf8mb4
  DEFAULT COLLATE utf8mb4_0900_ai_ci;
USE perf;

-- 2) Sauberer Neuaufbau
DROP TABLE IF EXISTS products_a;
DROP TABLE IF EXISTS products_b;


CREATE TABLE products_a (
  id BIGINT NOT NULL AUTO_INCREMENT,
  product_id INT NOT NULL,
  category VARCHAR(32) NOT NULL,
  name VARCHAR(64) NOT NULL,
  PRIMARY KEY (id),
  KEY idx_products_a_category (category),
  KEY idx_products_a_product_id (product_id)
) ENGINE=InnoDB;

CREATE TABLE products_b (
  id BIGINT NOT NULL AUTO_INCREMENT,
  product_id INT NOT NULL,
  category VARCHAR(32) NOT NULL,
  name VARCHAR(64) NOT NULL,
  PRIMARY KEY (id),
  KEY idx_products_b_category (category),
  KEY idx_products_b_product_id (product_id)
) ENGINE=InnoDB;

DROP TABLE IF EXISTS digits;
CREATE TABLE digits (d TINYINT PRIMARY KEY) ENGINE=InnoDB;

INSERT INTO digits (d) VALUES (0),(1),(2),(3),(4),(5),(6),(7),(8),(9);



INSERT + NO_HASH_JOIN  INTO products_a (product_id, category, name)
SELECT
  viele Duplikate über Modulo auf 700k 
  ((n % 700000) + 1) AS product_id,
  CASE
    WHEN n % 3 = 0 THEN 'Books'
    WHEN n % 3 = 1 THEN 'Electronics'
    ELSE 'Toys'
  END AS category,
  CONCAT('ProdA-', n) AS name
FROM (
  SELECT (d1.d
        + d2.d*10
        + d3.d*100
        + d4.d*1000
        + d5.d*10000
        + d6.d*100000) + 1 AS n
  FROM digits d1
  CROSS JOIN digits d2
  CROSS JOIN digits d3
  CROSS JOIN digits d4
  CROSS JOIN digits d5
  CROSS JOIN digits d6
) AS seq;


INSERT + NO_HASH_JOIN  INTO products_b (product_id, category, name)
SELECT
  ähnliche, aber leicht verschobene Duplikatverteilung 
  ((n % 720000) + 1) AS product_id,
  CASE
    WHEN n % 2 = 0 THEN 'Books'
    ELSE 'Electronics'
  END AS category,
  CONCAT('ProdB-', n) AS name
FROM (
  SELECT (d1.d
        + d2.d*10
        + d3.d*100
        + d4.d*1000
        + d5.d*10000
        + d6.d*100000) + 1 AS n
  FROM digits d1
  CROSS JOIN digits d2
  CROSS JOIN digits d3
  CROSS JOIN digits d4
  CROSS JOIN digits d5
  CROSS JOIN digits d6
) AS seq;


ANALYZE TABLE products_a, products_b;

SELECT 'products_a' AS tbl, COUNT(*) AS row_count FROM products_a
UNION ALL
SELECT 'products_b' AS tbl, COUNT(*) AS row_count FROM products_b;


SELECT 'A Electronics' AS src, COUNT(*) AS row_count FROM products_a WHERE category='Electronics'
UNION ALL
SELECT 'B Books'       AS src, COUNT(*) AS row_count FROM products_b WHERE category='Books';
-- Zeilen pro Kategorie (Prüfung)


-- Duplikate prüfen
SELECT
  COUNT(*) AS total_rows,
  COUNT(DISTINCT product_id) AS distinct_rows,
  COUNT(*) - COUNT(DISTINCT product_id) AS duplicates
FROM (
  SELECT product_id FROM products_a WHERE category='Electronics'
  UNION ALL
  SELECT product_id FROM products_b WHERE category='Books'
) all_rows;




-- UNION (entfernt Duplikate)
EXPLAIN ANALYZE
SELECT product_id
FROM products_a WHERE category = 'Electronics'
UNION
SELECT product_id
FROM products_b WHERE category = 'Books';

-- UNION ALL (behält Duplikate)
EXPLAIN ANALYZE
SELECT product_id
FROM products_a WHERE category = 'Electronics'
UNION ALL
SELECT product_id
FROM products_b WHERE category = 'Books';

-- DISTINCT über UNION ALL (optional)
EXPLAIN ANALYZE
SELECT DISTINCT product_id
FROM (
  SELECT product_id FROM products_a WHERE category='Electronics'
  UNION ALL
  SELECT product_id FROM products_b WHERE category='Books'
) t;

*/

