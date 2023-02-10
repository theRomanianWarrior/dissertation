using System.Text.Json.Serialization;

namespace AttractionsCsvGenerator.Models;

public record AttractionApiModel
{
    [JsonPropertyName("xid")]
    public string Xid { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("dist")]
    public double Dist { get; set; }
    
    [JsonPropertyName("rate")]
    public int Rate { get; set; }
    
    [JsonPropertyName("osm")]
    public string Osm { get; set; }
    
    [JsonPropertyName("wikidata")]
    public string Wikidata { get; set; }
    
    [JsonPropertyName("kinds")]
    public string Kinds { get; set; }
    
    [JsonPropertyName("point")]
    public Point Point { get; set; }
}