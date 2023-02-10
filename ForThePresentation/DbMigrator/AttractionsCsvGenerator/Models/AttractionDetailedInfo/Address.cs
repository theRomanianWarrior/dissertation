using System.Text.Json.Serialization;

namespace AttractionsCsvGenerator.Models.AttractionDetailedInfo;

public record Address
{
    [JsonPropertyName("town")]
    public string Town { get; set; }
    
    [JsonPropertyName("state")]
    public string State { get; set; }
    
    [JsonPropertyName("county")]
    public string County { get; set; }
    
    [JsonPropertyName("suburb")]
    public string Suburb { get; set; }
    
    [JsonPropertyName("country")]
    public string Country { get; set; }
    
    [JsonPropertyName("postcode")]
    public string Postcode { get; set; }
    
    [JsonPropertyName("pedestrian")]
    public string Pedestrian { get; set; }
    
    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }
    
    [JsonPropertyName("neighbourhood")]
    public string Neighbourhood { get; set; }
}