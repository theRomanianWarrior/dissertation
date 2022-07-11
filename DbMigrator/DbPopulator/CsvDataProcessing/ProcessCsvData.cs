using System.Globalization;
using CsvHelper;

namespace DbPopulator.CsvDataProcessing
{
    public static class ProcessCsvData<T>
    {
        public static List<T> ReadRecordsFromCsv(string csvPath)
        {
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList();
        }

        public static IEnumerable<string> ReadFieldFromCsv(string fieldName,string csvPath)
        {
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = new List<string>();
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var data = csv.GetField(fieldName);
                records.Add(data);
            }

            return records;
        }
        
        public static void WriteRecordsToCsv(IEnumerable<T> recordsToWrite, string csvPath)
        {
            using var writer = new StreamWriter(csvPath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(recordsToWrite);
        }
    }
}
