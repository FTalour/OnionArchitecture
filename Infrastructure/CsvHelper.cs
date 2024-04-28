using CsvHelper;
using System.Globalization;

namespace Infrastructure
{
    public static class CsvHelper
    {
        public static void Write<T>(IEnumerable<T> people, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(people);
                }
            }
        }

        public static IEnumerable<T> ReadCsv<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<T>().ToList();
                }
            }
        }
    }
}
