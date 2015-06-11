using System;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchemalessCsvReader
{
    public class Program
    {
        static IList<ICollection<object>> ReadText(string dsv, string delimiter)
        {
            using(var sr = new StringReader(dsv))
            {
                var csv = new CsvReader(sr);
                csv.Configuration.Delimiter = delimiter;
                csv.Configuration.HasHeaderRecord = false;
                return csv.GetRecords<dynamic>().Select(x => ((IDictionary<string, object>) x).Values).ToList();
            }
        }

        static IList<ICollection<object>> ReadFile(string dsv, string delimiter)
        {
            var path = Path.GetTempFileName();
            File.WriteAllText(path, dsv);
            using(var sr = new StringReader(dsv))
            {
                var csv = new CsvReader(sr);
                csv.Configuration.Delimiter = delimiter;
                csv.Configuration.HasHeaderRecord = false;
                return csv.GetRecords<dynamic>().Select(x => ((IDictionary<string, object>) x).Values).ToList();
            }
        }

        static void Main()
        {
            foreach (var line in ReadText("Apple\tRed\t200\r\nBanana\tYellow\t100", "\t"))
            {
                Console.WriteLine(string.Join("\t", line));
            };

            foreach(var line in ReadFile("Apple\tRed\t200\r\nBanana\tYellow\t100", "\t"))
            {
                Console.WriteLine(string.Join("\t", line));
            };
        }
    }
}
