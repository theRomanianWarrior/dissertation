using System.Text.Json.Serialization;

namespace AttractionsCsvGenerator.Models.AttractionDetailedInfo;

public record Bbox
{
    [JsonPropertyName("lon_min")]
    public double LonMin { get; set; }
    
    [JsonPropertyName("lon_max")]
    public double LonMax { get; set; }
    
    [JsonPropertyName("lat_min")]
    public double LatMin { get; set; }
    
    [JsonPropertyName("lat_max")]
    public double LatMax { get; set; }
    
}