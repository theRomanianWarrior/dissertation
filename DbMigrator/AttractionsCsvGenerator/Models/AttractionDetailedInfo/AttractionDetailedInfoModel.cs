using System.Text.Json.Serialization;

namespace AttractionsCsvGenerator.Models.AttractionDetailedInfo;

public record AttractionDetailedInfoModel
{
    [JsonPropertyName("xid")]
    public string Xid { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("address")]
    public Address Address { get; set; }

    [JsonPropertyName("rate")]
    public string Rate { get; set; }

    [JsonPropertyName("osm")]
    public string Osm { get; set; }

    [JsonPropertyName("bbox")]
    public Bbox Bbox { get; set; }

    [JsonPropertyName("wikidata")]
    public string Wikidata { get; set; }

    [JsonPropertyName("kinds")]
    public string Kinds { get; set; }
    
    [JsonPropertyName("sources")]
    public Sources Sources { get; set; }

    [JsonPropertyName("otm")]
    public string Otm { get; set; }

    [JsonPropertyName("wikipedia")]
    public string Wikipedia { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    [JsonPropertyName("preview")]
    public Preview Preview { get; set; }

    [JsonPropertyName("wikipedia_extracts")]
    public WikipediaExtracts WikipediaExtracts { get; set; }

    [JsonPropertyName("point")]
    public Point Point { get; set; }
              
}