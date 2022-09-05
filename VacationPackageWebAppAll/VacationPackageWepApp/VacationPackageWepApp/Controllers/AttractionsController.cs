using Microsoft.AspNetCore.Mvc;
using VacationPackageWepApp.UiDataStoring.Preference;
using VacationPackageWepApp.UiDataStoring.Singleton;

namespace VacationPackageWepApp.Controllers;

[Route("[controller]")]
public class AttractionsController : Controller
{
    [HttpPost("[action]/{attractionsPreferences}")]
    public void StoreAttractionsPreferences(string attractionsPreferences)
    {
        var attractionPref = new AttractionPreferenceDto();

        var attractionsPreferencesList = attractionsPreferences.Split(", ").ToList();

        attractionPref.Natural = attractionsPreferencesList[0].Equals("true");
        attractionPref.Cultural = attractionsPreferencesList[1].Equals("true");
        attractionPref.Historical = attractionsPreferencesList[2].Equals("true");
        attractionPref.Religion = attractionsPreferencesList[3].Equals("true");
        attractionPref.Architecture = attractionsPreferencesList[4].Equals("true");
        attractionPref.IndustrialFacilities = attractionsPreferencesList[5].Equals("true");
        attractionPref.Other = attractionsPreferencesList[6].Equals("true");

        PreferencesPayloadSingleton.Instance.CustomerAttractionNavigation = attractionPref;
    }
}