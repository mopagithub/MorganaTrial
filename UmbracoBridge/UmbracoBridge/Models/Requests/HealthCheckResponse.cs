namespace UmbracoBridge.Models.Requests
{
    public class HealthCheckResponse
    {
        public int total { get; set; }
        public List<Item>? Items { get; set; }
    }

    public class Item
    {
        public string? name { get; set; }
    }
}