using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace core.Models
{
  public class AdTokenResponse
  {
     [JsonPropertyName("access_token")]
     public string AccessToken { get; set; }
     [JsonPropertyName("token_type")]
     public string TokenType { get; set; }
     [JsonPropertyName("expires_in")]
     public string ExpiresIn { get; set; }
     [JsonPropertyName("expires_on")]
     public string ExpiresOn { get; set; }
     [JsonPropertyName("resource")]
     public string Resource { get; set; }
  }
}

