using System.Text.Json.Serialization;

namespace AttractionsCsvGenerator.Models.AttractionDetailedInfo;

public record Sources
{
    [JsonPropertyName("geometry")]
    public string Geometry { get; set; }
    
    [JsonPropertyName("attributes")]
    public List<string> Attributes { get; set; }
}