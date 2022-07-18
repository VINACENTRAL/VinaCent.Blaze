
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace VinaCent.Blaze.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class NotifyModel
    {
        // [JsonProperty("type")]
        // [JsonPropertyName("type")]
        public string Type { get; set; }
        
        // [JsonProperty("message")]
        // [JsonPropertyName("message")]
        public string Message { get; set; }
        
        // [JsonProperty("title")]
        // [JsonPropertyName("title")]
        public string Title { get; set; }
        
        // [JsonProperty("options")]
        // [JsonPropertyName("options")]
        public object Options { get; set; }
    }
}