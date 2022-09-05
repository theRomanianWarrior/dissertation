using Microsoft.AspNetCore.Mvc;
using VacationPackageWepApp.UiDataStoring.Preference;
using VacationPackageWepApp.UiDataStoring.Singleton;

namespace VacationPackageWepApp.Controllers;

[Route("[controller]")]
public class PropertyController : Controller
{
    [HttpPost("[action]/{amenities}")]
    public void StoreAmenities(string? amenities)
    {
        var listOfAmenities = amenities!.Split(", ").ToList();
        listOfAmenities.RemoveAll(e => e.Equals(string.Empty));
        if (listOfAmenities.Count == 0)
        {
            if (PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation != null)
                PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.AmenitiesNavigation = null;
            return;
        }
        
        PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation ??= new PropertyPreferenceDto();

        var amenitiesPreference = new AmenitiesPreferenceDto()
        {
            WiFi = false,
            AirConditioning = false,
            Dryer = false,
            Heating = false,
            Iron = false,
            Kitchen = false,
            Tv = false,
            Washer = false
        };

        foreach (var amenity in listOfAmenities)
            switch (amenity)
            {
                case "WiFi":
                    amenitiesPreference.WiFi = true;
                    break;
                case "AirConditioning":
                    amenitiesPreference.AirConditioning = true;
                    break;
                case "Dryer":
                    amenitiesPreference.Dryer = true;
                    break;
                case "Heating":
                    amenitiesPreference.Heating = true;
                    break;
                case "Iron":
                    amenitiesPreference.Iron = true;
                    break;
                case "Kitchen":
                    amenitiesPreference.Kitchen = true;
                    break;
                case "Tv":
                    amenitiesPreference.Tv = true;
                    break;
                case "Washer":
                    amenitiesPreference.Washer = true;
                    break;
            }

        PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.AmenitiesNavigation = amenitiesPreference;
    }
    
    [HttpPost("[action]/{placesType}")]
    public void StorePlaceType(string? placesType)
    {
        var listOfPlacesType = placesType!.Split(", ").ToList();
        listOfPlacesType.RemoveAll(e => e.Equals(string.Empty));
        if (listOfPlacesType.Count == 0)
        {
            if (PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation != null)
                PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.PlaceTypeNavigation = null;
            return;
        }
        
        PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation ??= new PropertyPreferenceDto();

        var propertyType = new PlaceTypePreferenceDto()
        {
            EntirePlace = false,
            PrivateRoom = false,
            SharedRoom = false,
        };


        foreach (var propType in listOfPlacesType)
            switch (propType)
            {
                case "Entire Place":
                    propertyType.EntirePlace = true;
                    break;
                case "Private Room":
                    propertyType.PrivateRoom = true;
                    break;
                case "Shared Room":
                    propertyType.SharedRoom = true;
                    break;
            }

        PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.PlaceTypeNavigation = propertyType;
    }

    [HttpPost("[action]/{pets}")]
    public void StorePetsPreference(string pets)
    {
        PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation ??= new PropertyPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.Pets = pets == "true";
    }

    [HttpPost("[action]/{roomsAndBeds}")]
    public void StoreRoomsAndBedsPreference(string roomsAndBeds)
    {
        var roomsAndBedsList = roomsAndBeds.Split(", ").ToList();

        if (roomsAndBedsList.Count != 3)
        {
            roomsAndBedsList = roomsAndBeds.Split(",").ToList();
        }
        
        PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation ??= new PropertyPreferenceDto();
        PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.RoomsAndBedsNavigation =
            new RoomsAndBedsPreferenceDto();
        
        if (roomsAndBedsList[0] != string.Empty && roomsAndBedsList[0] != " ") PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.RoomsAndBedsNavigation.Bathrooms = short.Parse(roomsAndBedsList[0]);
        if (roomsAndBedsList[1] != string.Empty && roomsAndBedsList[1] != " ") PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.RoomsAndBedsNavigation.Beds = short.Parse(roomsAndBedsList[1]);
        if (roomsAndBedsList[2] != string.Empty && roomsAndBedsList[2] != " ") PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.RoomsAndBedsNavigation.Bedrooms = short.Parse(roomsAndBedsList[2]);
    }
    
    [HttpPost("[action]/{propertiesType}")]
    public void StorePropertyType(string? propertiesType)
    {
        var listOfTypeProperties = propertiesType!.Split(", ").ToList();
        listOfTypeProperties.RemoveAll(e => e.Equals(string.Empty));
        if (listOfTypeProperties.Count == 0)
        {
            if (PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation != null)
                PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.PropertyTypeNavigation = null;
            return;
        }
        
        PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation ??= new PropertyPreferenceDto();

        var propertyType = new PropertyTypePreferenceDto
        {
            Apartment = false,
            GuestHouse = false,
            Hotel = false,
            House = false
        };


        foreach (var propType in listOfTypeProperties)
            switch (propType)
            {
                case "House":
                    propertyType.House = true;
                    break;
                case "Apartment":
                    propertyType.Apartment = true;
                    break;
                case "Guest House":
                    propertyType.GuestHouse = true;
                    break;
                case "Hotel":
                    propertyType.Hotel = true;
                    break;
            }

        PreferencesPayloadSingleton.Instance.CustomerPropertyNavigation.PropertyTypeNavigation = propertyType;
    }
}