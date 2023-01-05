using System.Globalization;
using System.Text;
using System.Text.Json;
using AttractionsCsvGenerator.Models;
using AttractionsCsvGenerator.Models.AttractionDetailedInfo;
using CsvHelper;
using DbPopulator.CsvDataProcessing;
using DbPopulator.CsvDataProcessing.CsvForDatabasePopulating;

namespace AttractionsCsvGenerator;

public static class Program
{
    private const string OpenTripMapApiPlacesLink = @"https://api.opentripmap.com/0.1/en/places/";
    private const string SearchRadiusMeters = "15000";
    private const string MinimumRatingPopularity = "3";
    private const string ResponseFormat = "json";
    private const string ApiKey = "5ae2e3f221c38a28845f05b6b2a9f2a0a38b36bc625d8154818d6231";
    private const string MaximumSearchObjects = "10";


    private static void CreateCsvAndStoreCitiesAttractions(IEnumerable<AttractionDetailedInfoModel> citiesAttractions)
    {
        ProcessCsvData<AttractionDetailedInfoModel>.WriteRecordsToCsv(citiesAttractions, CsvLocation.CitiesAttractionsCsvLocation);
        Console.WriteLine("Writing to csv done.");
    }
    
    private static async Task<List<AttractionDetailedInfoModel>> GetCityAttractionsAsync(IEnumerable<CitiesCoordinates> citiesCoordinates)
    {
        var citiesAttractions = new List<AttractionDetailedInfoModel>();
        
        foreach (var city in citiesCoordinates)
        {
            var getClosestAttractionsApiLink = OpenTripMapApiPlacesLink + $"radius?radius={SearchRadiusMeters}&lon={city.Point.Lon}&lat={city.Point.Lat}&rate={MinimumRatingPopularity}&format={ResponseFormat}&limit={MaximumSearchObjects}&apikey={ApiKey}";
            var cityAttractions = await HttpClientCall.GetRequestAsync<List<AttractionApiModel>>(getClosestAttractionsApiLink);
            
            foreach (var attraction in cityAttractions)
            {
                var getAttractionDetailedInfoApiLink = OpenTripMapApiPlacesLink + $"xid/{attraction.Xid}?apikey={ApiKey}";
                var detailedInfo = await HttpClientCall.GetRequestAsync<AttractionDetailedInfoModel>(getAttractionDetailedInfoApiLink);
                detailedInfo.Address.Town = city.City; /* This is for the special characters in the name of cities. */
                citiesAttractions.Add(detailedInfo);
            }
            Console.WriteLine(city.City);
        }

        Console.WriteLine("Cities attractions retrieval done.");
        return citiesAttractions;
    }
    
    private static IEnumerable<CitiesCoordinates> ReadCitiesCoordinatesFromCsv()
    {
        var citiesCoordinates =
            ProcessCsvData<CitiesCoordinates>.ReadRecordsFromCsv(CsvLocation.CitiesCoordinatesCsvLocation);
        Console.WriteLine("Reading cities coordinates done.");
        return ProcessCsvData<CitiesCoordinates>.ReadRecordsFromCsv(CsvLocation.CitiesCoordinatesCsvLocation);
    }
    
    private static async Task Main()
    {
        var citiesCoordinate = ReadCitiesCoordinatesFromCsv();
        var citiesAttractions = await GetCityAttractionsAsync(citiesCoordinate);
        CreateCsvAndStoreCitiesAttractions(citiesAttractions);
    }
}
