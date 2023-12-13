using System.Text.Json.Serialization;

namespace Kosheidem.Models.External
{
    public class GoogleAuthConfiguration
    {
        [JsonPropertyName("web")] public GoogleAuthConfigurationWeb Web { get; set; }
    }

    public class GoogleAuthConfigurationWeb
    {
        [JsonPropertyName("client_id")] public string ClientId { get; set; }

        [JsonPropertyName("client_secret")] public string ClientSecret { get; set; }
    }
}