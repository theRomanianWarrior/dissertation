using CsvDataGenerator.PropertiesGenerator.Models;
using DbPopulator.Migrations.DatabasePopulatingData;

IEnumerable<string> cities = GetDataFromCsv.ReadCitiesFromCsv();

foreach(var city in cities)
{
    PropertyCsvModel propertyCsvModel = new();
    propertyCsvModel.City = city;
}
Console.WriteLine("good");