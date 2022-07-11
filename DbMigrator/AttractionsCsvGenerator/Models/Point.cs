using System.Text.Json.Serialization;

namespace AttractionsCsvGenerator.Models;

public record Point
{
    [JsonPropertyName("lon")]
    public double Lon { get; set; }
    
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
}