using System.Text.Json.Serialization;

namespace UmbracoBridge.Models.Requests
{

    public class ApiUserResponse
    {
        [JsonPropertyName("id")]
        public required Guid Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("email")]
        public required string Email { get; set; }
    }
}
