using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

public class Movie
{
    [BsonId]
    [BsonElement("Id")]
    [JsonPropertyName("Id")]
    public string Id { get; set; } = "";
    [BsonElement("Title")]
    [JsonPropertyName("Title")]
    public string Title { get; set; } = "";
    [BsonElement("Year")]
    [JsonPropertyName("Year")]
    public int Year { get; set; }
    [BsonElement("Summary")]
    [JsonPropertyName("Summary")]
    public string Summary { get; set; } = "";
    [BsonElement("Actors")]
    [JsonPropertyName("Actors")]
    public string[] Actors { get; set; } = Array.Empty<string>();
}