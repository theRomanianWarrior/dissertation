#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestWebAPI.BusinessLogic;
using TestWebAPI.CustomerServicesEvaluation;
using TestWebAPI.Enums;
using TestWebAPI.PreferencesPackageRequest;
using TestWebAPI.PreferencesPackageResponse;
using VacationPackageWebApi.Infrastructure.Repositories.Models;

namespace TestWebAPI;

internal static class Program
{
    private const string BaseUrl = "http://localhost:7071/";
    private const string RequestVacationRecommendationUri = BaseUrl + "VacationPackage/RequestVacationRecommendation";
    private const string SaveEvaluationsUri = BaseUrl + "VacationPackage/SaveEvaluations";
    private const string GetFlightCompaniesForDepartureDestinationCity = BaseUrl + "Flight/GetFlightCompaniesForDepartureDestinationCity/";
    private const string GetFlightDepartureCities = BaseUrl + "Flight/GetFlightDepartureCities";
    private const string GetFlightArrivalCities = BaseUrl + "Flight/GetFlightArrivalCities/";

    private static readonly Random Random = new();

    private static async Task Main()
    {
        using var client = new HttpClient();

        for (var i = 1; i <= 1000; i++)
        {
            /* 1. Get all departure city flights */
            var responseOfGetFlightDepartureCities = await client.GetAsync(GetFlightDepartureCities);

            var resultOfGetDepartureCities = await responseOfGetFlightDepartureCities.Content.ReadAsStringAsync();
            var jsonGetDepartureCitiesDeserialize =
                JsonConvert.DeserializeObject<List<string>>(resultOfGetDepartureCities);

            /* 2. Get random city for departure */
            var indexOfFlightDepartureCities = Random.Next(jsonGetDepartureCitiesDeserialize!.Count);
            var randomDepartureCityCountry = jsonGetDepartureCitiesDeserialize[indexOfFlightDepartureCities];
            var randomDepartureCity = randomDepartureCityCountry.Split(", ")[0];

            /* 3. Get arrival cities based on departure */
            var responseOfGetArrivalCities = await client.GetAsync(GetFlightArrivalCities + randomDepartureCity);

            var resultOfGetArrivalCities = await responseOfGetArrivalCities.Content.ReadAsStringAsync();
            var jsonGetArrivalCitiesDeserialize =
                JsonConvert.DeserializeObject<List<string>>(resultOfGetArrivalCities);

            /* 4. Get random city for arrival */
            var indexOfFlightArrivalCities = Random.Next(jsonGetArrivalCitiesDeserialize!.Count);
            var randomArrivalCityCountry = jsonGetArrivalCitiesDeserialize[indexOfFlightArrivalCities];
            var randomArrivalCity = randomArrivalCityCountry.Split(", ")[0];

            /* 5. Get flight companies that have flights departure-destination cities */
            var payloadForFlightCompanies = $"{randomDepartureCity}, {randomArrivalCity}";
            var responseOfGetFlightCompaniesForDepartureDestinationCity =
                await client.GetAsync(GetFlightCompaniesForDepartureDestinationCity + payloadForFlightCompanies);

            var resultOfGetFlightCompaniesForDepartureDestinationCity =
                await responseOfGetFlightCompaniesForDepartureDestinationCity.Content.ReadAsStringAsync();
            var jsonGetFlightCompaniesForDepartureDestinationCity =
                JsonConvert.DeserializeObject<List<string>>(resultOfGetFlightCompaniesForDepartureDestinationCity);

            /* 6. Get flight companies that have flights for return */
            var payloadForReturnFlightCompanies = $"{randomArrivalCity}, {randomDepartureCity}";
            var responseOfGetFlightCompaniesForReturn =
                await client.GetAsync(GetFlightCompaniesForDepartureDestinationCity + payloadForReturnFlightCompanies);

            var resultOfGetFlightCompaniesForReturn =
                await responseOfGetFlightCompaniesForReturn.Content.ReadAsStringAsync();
            var jsonGetFlightCompaniesForReturn =
                JsonConvert.DeserializeObject<List<string>>(resultOfGetFlightCompaniesForReturn);

            /* 7. Get departure random flight companies */
            var indexOfRandomFlightCompanyDepart =
                Random.Next(jsonGetFlightCompaniesForDepartureDestinationCity!.Count);
            var randomFlightCompanyDepart = jsonGetFlightCompaniesForDepartureDestinationCity[indexOfRandomFlightCompanyDepart];

            var indexOfRandomFlightCompanyDepart1 =
                Random.Next(jsonGetFlightCompaniesForDepartureDestinationCity.Count);
            var randomFlightCompanyDepart1 = jsonGetFlightCompaniesForDepartureDestinationCity[indexOfRandomFlightCompanyDepart1];

            /* 8. Get return random flight companies */
            var indexOfRandomFlightCompanyReturn = Random.Next(jsonGetFlightCompaniesForReturn!.Count);
            var randomFlightCompanyReturn = jsonGetFlightCompaniesForReturn[indexOfRandomFlightCompanyReturn];

            var indexOfRandomFlightCompanyReturn1 = Random.Next(jsonGetFlightCompaniesForReturn.Count);
            var randomFlightCompanyReturn1 = jsonGetFlightCompaniesForReturn[indexOfRandomFlightCompanyReturn1];

            /* 9. Fulfill preferences */
            PreferencesRequest preferencesRequest = new()
            {
                DepartureDate = DateTime.UtcNow,
                HolidaysPeriod = (short)Random.Next(3, 8), // random period from 3 to 7 days
                CustomerId = new Guid("4a77f1d2-1175-4065-860a-67ee52d5ea1e"),
                DepartureCityNavigation = new()
                {
                    Name = randomDepartureCity
                },
                DestinationCityNavigation = new()
                {
                    Name = randomArrivalCity
                },
                PersonsByAgeNavigation = new()
                {
                    Adult = 2,
                    Children = 1,
                    Infant = 2
                },
                CustomerAttractionNavigation = new()
                {
                    Architecture = GetRandomBool(),
                    Cultural = GetRandomBool(),
                    Historical = GetRandomBool(),
                    IndustrialFacilities = GetRandomBool(),
                    Natural = GetRandomBool(),
                    Religion = GetRandomBool(),
                    Other = GetRandomBool()
                },
                CustomerPropertyNavigation = new()
                {
                    PlaceTypeNavigation = new PlaceTypePreferenceDto()
                    {
                        EntirePlace = GetRandomBool(),
                        PrivateRoom = GetRandomBool(),
                        SharedRoom = GetRandomBool()
                    },
                    AmenitiesNavigation = new()
                    {
                        AirConditioning = GetRandomBool(),
                        Dryer = GetRandomBool(),
                        Heating = GetRandomBool(),
                        Iron = GetRandomBool(),
                        Kitchen = GetRandomBool(),
                        Tv = GetRandomBool(),
                        Washer = GetRandomBool(),
                        WiFi = GetRandomBool()
                    },

                    RoomsAndBedsNavigation = new()
                    {
                        // Generate random numbers from 1 to 3
                        Bathrooms = (short)Random.Next(1, 4),
                        Bedrooms = (short)Random.Next(1, 4),
                        Beds = (short)Random.Next(1, 4)
                    },

                    Pets = GetRandomBool(),
                    PropertyTypeNavigation = new()
                    {
                        Apartment = GetRandomBool(),
                        GuestHouse = GetRandomBool(),
                        Hotel = GetRandomBool(),
                        House = GetRandomBool()
                    }
                },
                CustomerFlightNavigation = new FlightDirectionPreferenceDto()
                {
                    DepartureNavigation = new FlightPreferenceDto()
                    {
                        Class = new FlightClassDto()
                        {
                            Class = (short) Random.Next(1, 4)
                        },
                        DeparturePeriodPreference = new DeparturePeriodsPreferenceDto()
                        {
                            Afternoon = GetRandomBool(),
                            EarlyMorning = GetRandomBool(),
                            Morning = GetRandomBool(),
                            Night = GetRandomBool()
                        },
                        FlightCompaniesNavigationList = new List<FlightCompaniesPreferenceDto>()
                        {
                            new()
                            {
                                Company = new FlightCompanyDto
                                {
                                    Name = randomFlightCompanyDepart
                                }
                            },
                            new()
                            {
                                Company = new FlightCompanyDto
                                {
                                    Name = randomFlightCompanyDepart1
                                }
                            }
                        },
                        StopsNavigation = new StopsTypePreferenceDto
                        {
                            Type = (short) Random.Next(1, 4)
                        }
                    },
                    ReturnNavigation = new FlightPreferenceDto
                    {
                        Class = new FlightClassDto
                        {
                            Class = (short)Random.Next(1, 4)
                        },
                        DeparturePeriodPreference = new DeparturePeriodsPreferenceDto
                        {
                            Afternoon = GetRandomBool(),
                            EarlyMorning = GetRandomBool(),
                            Morning = GetRandomBool(),
                            Night = GetRandomBool(),
                        },
                        FlightCompaniesNavigationList = new List<FlightCompaniesPreferenceDto>
                        {
                            new()
                            {
                                Company = new FlightCompanyDto
                                {
                                    Name = randomFlightCompanyReturn
                                }
                            },
                            new()
                            {
                                Company = new FlightCompanyDto
                                {
                                    Name = randomFlightCompanyReturn1
                                }
                            }
                        },
                        StopsNavigation = new StopsTypePreferenceDto
                        {
                            Type = (short) Random.Next(1, 4)
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(preferencesRequest, Formatting.Indented);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //DateTime T = System.DateTime.UtcNow;
            var response = await client.PostAsync(RequestVacationRecommendationUri, data);
            //TimeSpan TT = System.DateTime.UtcNow - T; //--> Note the Time Difference

           // Console.WriteLine(TT.Milliseconds);
            var result = await response.Content.ReadFromJsonAsync<PreferencesResponse?>();

            /* 10. Fulfill evaluation of recommendation */
            await FormTheRecommendation(result, client);
            
            var departureFlightMatchingRate = FlightRecommendationLogic.CalculateFlightSimilarityRate(preferencesRequest,
                                                                                   result!.FlightRecommendationResponse, "Departure");
            
            var returnFlightMatchingRate = FlightRecommendationLogic.CalculateFlightSimilarityRate(preferencesRequest,
                result.FlightRecommendationResponse, "Return");

            var propertyMatchingRate = PropertyRecommendationLogic.CalculatePropertySimilarityRate(preferencesRequest, result.PropertyPreferencesResponse);

            var attractionsMatchingRate = AttractionsRecommendationLogic.CalculateAttractionsSimilarityRate(preferencesRequest, result.AttractionsRecommendationResponse!.AttractionRecommendationList);

            var mean = (departureFlightMatchingRate + returnFlightMatchingRate + propertyMatchingRate +
                       attractionsMatchingRate) / 4;
            
            Console.WriteLine($"{i} {departureFlightMatchingRate} {returnFlightMatchingRate} {propertyMatchingRate} {attractionsMatchingRate} {mean}");
        }
    }

    private static async Task FormTheRecommendation(PreferencesResponse? result, HttpClient client)
    {
        var attractionEvaluationsList = new List<AttractionEvaluationDto>();

        var attractionRecommendationList =
            result!.AttractionsRecommendationResponse!.AttractionRecommendationList.ToList();
        foreach (var attractionRecommendation in attractionRecommendationList)
        {
            var attractionEvaluation = new AttractionEvaluationDto
            {
                AttractionId = attractionRecommendation.Attraction.Xid,
                AttractionName = attractionRecommendation.Attraction.Name,
                Rate = GetRandomBool()
            };
            attractionEvaluationsList.Add(attractionEvaluation);
        }

        var serviceEvaluation = new ServiceEvaluationDto
        {
            ClientRequestId = (Guid) result.ClientRequestId!,
            AttractionEvaluation = new AllAttractionEvaluationPointDto
            {
                AttractionEvaluations = attractionEvaluationsList
            },
            FlightEvaluation = new FlightDirectionEvaluationDto
            {
                DepartureNavigation = new FlightEvaluationDto
                {
                    Class = GetRandomBool(),
                    Company = GetRandomBool(),
                    FlightDate = GetRandomBool(),
                    FlightTime = GetRandomBool(),
                    Price = GetRandomBool()
                },
                ReturnNavigation = new FlightEvaluationDto
                {
                    Class = GetRandomBool(),
                    Company = GetRandomBool(),
                    FlightDate = GetRandomBool(),
                    FlightTime = GetRandomBool(),
                    Price = GetRandomBool()
                }
            },
            PropertyEvaluation = new PropertyEvaluationDto
            {
                Amenities = GetRandomBool(),
                PlaceType = GetRandomBool(),
                PropertyType = GetRandomBool(),
                RoomsAndBeds = GetRandomBool()
            }
        };

        var json1 = JsonConvert.SerializeObject(serviceEvaluation, Formatting.Indented);
        var data1 = new StringContent(json1, Encoding.UTF8, "application/json");

        await client.PostAsync(SaveEvaluationsUri, data1);
    }

    private static bool GetRandomBool()
    {
        return Random.Next(2) == 0;
    }
}