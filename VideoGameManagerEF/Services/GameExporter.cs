using System.Globalization; 
using CsvHelper;
using CsvHelper.Configuration;
using System;

namespace VideoGameManagerEF.Services;

public static class GameExporter
{
    public const string DefPath = "./wwwroot/data/games.csv";
    public static void ExportToCsv()
    {
    }
    
    public static void ImportFromCsv()
    {
    }
    
    public static List<T> Read<T>()
        {
            if (!File.Exists(DefPath))
            {
                throw new FileNotFoundException("File not found");
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", 
                PrepareHeaderForMatch = args => args.Header.ToLower() 
            };

            using var reader = new StreamReader(DefPath);
            using var csv = new CsvReader(reader, config);

            return csv.GetRecords<T>().ToList();
        }

        public static void Write<T>(List<T> records)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true
            };

            using var writer = new StreamWriter(DefPath);
            using var csv = new CsvWriter(writer, config);

            csv.WriteRecords(records);
        }

        public static void Append<T>(List<T> records)
        {

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            using var stream = File.Open(DefPath, FileMode.Append); 
            using var writer = new StreamWriter(stream);
            using var csv = new CsvWriter(writer, config);

            if (!File.Exists(DefPath) || new FileInfo(DefPath).Length == 0)
            {
                csv.WriteHeader<T>(); 
                csv.NextRecord(); 
            }

            csv.WriteRecords(records); 
        }

        public static void Append<T>(T record)
        {
            bool fileExists = File.Exists(DefPath);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = !fileExists
            };

            using var stream = File.Open(DefPath, FileMode.Append);
            using var writer = new StreamWriter(stream);
            using var csv = new CsvWriter(writer, config);

            csv.WriteRecord(record);
            csv.NextRecord();
        }
}