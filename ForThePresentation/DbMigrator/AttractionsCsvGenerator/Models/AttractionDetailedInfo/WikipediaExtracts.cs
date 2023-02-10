using System.Text.Json.Serialization;

namespace AttractionsCsvGenerator.Models.AttractionDetailedInfo;

public record WikipediaExtracts
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("text")]
    public string Text { get; set; }
    
    [JsonPropertyName("html")]
    public string Html { get; set; }
}