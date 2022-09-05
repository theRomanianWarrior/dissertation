using System.Text.Json;
using HttpClients;
using Microsoft.AspNetCore.Mvc;
using VacationPackageWepApp.UiDataStoring.Enums;
using VacationPackageWepApp.UiDataStoring.Preference;
using VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse;
using VacationPackageWepApp.UiDataStoring.Singleton;

namespace VacationPackageWepApp.Controllers;

[Route("[controller]")]
public class FlightController : Controller
{
    private bool disposed = false;
    private GenericRestfulCrudHttpClient<string, string> flightClient =
            new("http://localhost:7071/", "Flight/");
    
    private GenericRestfulCrudHttpClient<PreferencesRequest, PreferencesResponse> preferencesPackageClient =
        new("http://localhost:7071/", "VacationPackage/RequestVacationRecommendation/");
    
    [HttpGet("[action]")]
    public IActionResult GetFlightDepartureCities()
    {
        flightClient.addressSuffix = "Flight/GetFlightDepartureCities";
        var cities = flightClient.GetManyAsync();
        cities.Wait();
        var response = cities.Result;
        return new JsonResult(response.ToList());
    }   
    
    [HttpPost("[action]/{flightDepartureCity}")]
    public IActionResult GetFlightArrivalCities(string flightDepartureCity)
    {
        flightClient.addressSuffix = "Flight/GetFlightArrivalCities/" + flightDepartureCity;
        var cities = flightClient.GetManyAsync();
        cities.Wait();
        var response = cities.Result;
        return new JsonResult(response.ToList());
    }

    [HttpGet("[action]")]
    public async Task SendVacationPackagePreferencesToTheServer()
    {
        PreferencesPayloadSingleton.Instance.CustomerId = new Guid("141fcf23-a053-1d6e-b5df-c0cacbb84b21");

        var preferencesResponse = await preferencesPackageClient.PostAsync<PreferencesResponse>(PreferencesPayloadSingleton.Instance);
        
        ResetPreferencesPayload();
    }

    [HttpPost("[action]/{departureCity}")]
    public void StoreSelectedDepartureCity(string departureCity)
    {
        PreferencesPayloadSingleton.Instance.DepartureCityNavigation = new CityDto()
        {
            Name = departureCity
        };
    }
    
    [HttpPost("[action]/{destinationCity}")]
    public void StoreSelectedDestinationCity(string destinationCity)
    {
        PreferencesPayloadSingleton.Instance.DestinationCityNavigation = new CityDto()
        {
            Name = destinationCity
        };
    }
    
    [HttpPost("[action]/{personsByAge}")]
    public void StorePersonsByAge(string personsByAge)
    {
        var ageList = personsByAge.Split(", ").ToList();

        if (ageList.Count != 3)
        {
            ageList = personsByAge.Split(",").ToList();
        }
        PreferencesPayloadSingleton.Instance.PersonsByAgeNavigation = new AgeCategoryPreferenceDto();
        
        if (ageList[0] != string.Empty && ageList[0] != " ") PreferencesPayloadSingleton.Instance.PersonsByAgeNavigation.Adult = short.Parse(ageList[0]);
        if (ageList[1] != string.Empty && ageList[1] != " ") PreferencesPayloadSingleton.Instance.PersonsByAgeNavigation.Children = short.Parse(ageList[1]);
        if (ageList[2] != string.Empty && ageList[2] != " ") PreferencesPayloadSingleton.Instance.PersonsByAgeNavigation.Infant = short.Parse(ageList[2]);
    }
    
    [HttpPost("[action]/{stopsType}")]
    public void StoreReturnStops(string? stopsType)
    {
        if(stopsType is null or "null")
            return;
        
        var type = stopsType switch
        {
            "Direct" => (short) StopsTypePreferenceId.Direct,
            "OneStop" => (short) StopsTypePreferenceId.OneStop,
            "TwoOrMoreStops" => (short) StopsTypePreferenceId.TwoOrMoreStops,
            _ => (short) StopsTypePreferenceId.Direct
        };

        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation ??= new FlightDirectionPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation ??= new FlightPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation.StopsNavigation =
            new StopsTypePreferenceDto()
            {
                Type = type
            };
    }
    
    [HttpPost("[action]/{classType}")]
    public void StoreDepartureClass(string classType)
    {
        if(classType is null or "null")
            return;
        
        var type = classType switch
        {
            "Economy" => (short) ClassTypeId.Economy,
            "Business" => (short) ClassTypeId.Business,
            "First" => (short) ClassTypeId.First,
            _ => (short) ClassTypeId.Economy
        };

        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation ??= new FlightDirectionPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.DepartureNavigation ??= new FlightPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.DepartureNavigation.Class =
            new FlightClassDto()
            {
                Class = type
            };
    }
    
    [HttpPost("[action]/{classType}")]
    public void StoreReturnClass(string? classType)
    {
        if (classType is null or "null")
            return;
        
        var type = classType switch
        {
            "Economy" => (short) ClassTypeId.Economy,
            "Business" => (short) ClassTypeId.Business,
            "First" => (short) ClassTypeId.First,
            _ => (short) ClassTypeId.Economy
        };

        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation ??= new FlightDirectionPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation ??= new FlightPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation.Class =
            new FlightClassDto()
            {
                Class = type
            };
    }
    
    [HttpPost("[action]/{stopsType}")]
    public void StoreDepartureStops(string? stopsType)
    {
        if(stopsType is null or "null")
            return;

        var type = stopsType switch
        {
            "Direct" => (short) StopsTypePreferenceId.Direct,
            "OneStop" => (short) StopsTypePreferenceId.OneStop,
            "TwoOrMoreStops" => (short) StopsTypePreferenceId.TwoOrMoreStops,
            _ => (short) StopsTypePreferenceId.Direct
        };

        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation ??= new FlightDirectionPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.DepartureNavigation ??= new FlightPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.DepartureNavigation.StopsNavigation =
            new StopsTypePreferenceDto()
            {
                Type = type
            };
    }
    
    [HttpPost("[action]/{days}")]
    public void StoreVacationPeriod(short days)
    {
        PreferencesPayloadSingleton.Instance.HolidaysPeriod = days;
    }
    
    [HttpPost("[action]/{date}")] // date format yyyy-mm-dd
    public void StoreSelectedDepartureDate(string? date)
    {
        PreferencesPayloadSingleton.Instance.DepartureDate = Convert.ToDateTime(date);
    }
    
    [HttpGet("[action]")]
    public void CopyOptionalDepartureFlightPreferencesToReturn()
    {
        if (PreferencesPayloadSingleton.Instance.CustomerFlightNavigation?.DepartureNavigation?.StopsNavigation != null)
        {
            PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation ??= new FlightPreferenceDto();
            PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation.StopsNavigation =
                PreferencesPayloadSingleton.Instance.CustomerFlightNavigation?.DepartureNavigation?.StopsNavigation;

        }
        
        if (PreferencesPayloadSingleton.Instance.CustomerFlightNavigation?.DepartureNavigation?.Class != null)
        {
            PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation ??= new FlightPreferenceDto();
            PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation.Class =
                PreferencesPayloadSingleton.Instance.CustomerFlightNavigation?.DepartureNavigation?.Class;
        }
        
        if (PreferencesPayloadSingleton.Instance.CustomerFlightNavigation?.DepartureNavigation?.DeparturePeriodPreference != null)
        {
            PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation ??= new FlightPreferenceDto();
            PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation.DeparturePeriodPreference =
                PreferencesPayloadSingleton.Instance.CustomerFlightNavigation?.DepartureNavigation?.DeparturePeriodPreference;
        }
        
        if (PreferencesPayloadSingleton.Instance.CustomerFlightNavigation?.DepartureNavigation?.FlightCompaniesNavigationList != null)
        {
            PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation ??= new FlightPreferenceDto();
            PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation.FlightCompaniesNavigationList =
                PreferencesPayloadSingleton.Instance.CustomerFlightNavigation?.DepartureNavigation?.FlightCompaniesNavigationList;
        }

    }
    
    [HttpPost("[action]/{returnFlightCompanies}")]
    public void StoreSelectedReturnFlightCompanies(string? returnFlightCompanies)
    {
        var listOfReturnFlightCompanies = returnFlightCompanies!.Split(", ").ToList();
        listOfReturnFlightCompanies.RemoveAll(e => e.Equals(string.Empty));
        if(listOfReturnFlightCompanies.Count == 0)
            return;
        
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation ??= new FlightDirectionPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation ??= new FlightPreferenceDto();
        var preferredDepartureFlightCompanies = listOfReturnFlightCompanies.Select(flightCompany => new FlightCompaniesPreferenceDto() {Company = new FlightCompanyDto() {Name = flightCompany}}).ToList();

        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation
            .FlightCompaniesNavigationList = preferredDepartureFlightCompanies;
    }
    
    [HttpPost("[action]/{departureFlightCompanies}")]
    public void StoreSelectedDepartureFlightCompanies(string? departureFlightCompanies)
    {
        var listOfDepartureFlightCompanies = departureFlightCompanies!.Split(", ").ToList();
        listOfDepartureFlightCompanies.RemoveAll(e => e.Equals(string.Empty));
        if(listOfDepartureFlightCompanies.Count == 0)
            return;
        
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation ??= new FlightDirectionPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.DepartureNavigation ??= new FlightPreferenceDto();
        var preferredDepartureFlightCompanies = listOfDepartureFlightCompanies.Select(flightCompany => new FlightCompaniesPreferenceDto() {Company = new FlightCompanyDto() {Name = flightCompany}}).ToList();

        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.DepartureNavigation
            .FlightCompaniesNavigationList = preferredDepartureFlightCompanies;
    }

    [HttpPost("[action]/{departurePeriods}")]
    public void StoreSelectedDeparturePeriodsReturnFlight(string? departurePeriods)
    {
        var listOfDeparturePeriods = departurePeriods!.Split(", ").ToList();
        listOfDeparturePeriods.RemoveAll(e => e.Equals(string.Empty));
        if(listOfDeparturePeriods.Count == 0)
            return;
        
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation ??= new FlightDirectionPreferenceDto();

        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation ??= new FlightPreferenceDto();
        
        var departurePeriodPreference = new DeparturePeriodsPreferenceDto()
        {
            EarlyMorning = false,
            Afternoon = false,
            Morning = false,
            Night = false
        };

        foreach (var departurePeriod in listOfDeparturePeriods)
        {
            switch (departurePeriod)
            {
                case "Early Morning" : departurePeriodPreference.EarlyMorning = true;
                    break;
                case "Afternoon" : departurePeriodPreference.Afternoon = true;
                    break;
                case "Morning" : departurePeriodPreference.Morning = true;
                    break;
                case "Night" : departurePeriodPreference.Night = true;
                    break;
            }
        }

        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.ReturnNavigation.DeparturePeriodPreference =
            departurePeriodPreference;
    }
    
    [HttpPost("[action]/{departurePeriods}")]
    public void StoreSelectedDeparturePeriodsDepartureFlight(string? departurePeriods)
    {
        var listOfDeparturePeriods = departurePeriods!.Split(", ").ToList();
        listOfDeparturePeriods.RemoveAll(e => e.Equals(string.Empty));
        if(listOfDeparturePeriods.Count == 0)
            return;
        
        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation ??= new FlightDirectionPreferenceDto();

        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.DepartureNavigation ??= new FlightPreferenceDto();
        
        var departurePeriodPreference = new DeparturePeriodsPreferenceDto()
        {
            EarlyMorning = false,
            Afternoon = false,
            Morning = false,
            Night = false
        };

        foreach (var departurePeriod in listOfDeparturePeriods)
        {
            switch (departurePeriod)
            {
                case "Early Morning" : departurePeriodPreference.EarlyMorning = true;
                    break;
                case "Afternoon" : departurePeriodPreference.Afternoon = true;
                    break;
                case "Morning" : departurePeriodPreference.Morning = true;
                    break;
                case "Night" : departurePeriodPreference.Night = true;
                    break;
            }
        }

        PreferencesPayloadSingleton.Instance.CustomerFlightNavigation.DepartureNavigation.DeparturePeriodPreference =
            departurePeriodPreference;
    }
    
    [HttpGet("[action]")]
    public void ResetPreferencesPayload()
    {
        PreferencesPayloadSingleton.ResetInstance();
    } 
}