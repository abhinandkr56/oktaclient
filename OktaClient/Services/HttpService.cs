using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OktaClient.Services;

public class HttpService
{
    public async Task<HttpClient> GetClient()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", GetAccessToken().Result);        
        return client;
    }

    private async Task<string> GetAccessToken()
    {
        var url = "https://dev-01068725.okta.com/oauth2/default/v1/token";

        var tokenClient = new HttpClient();
        var formData = new Dictionary<string, string>()
        {
            {"client_id", "0oacvydxvvhtUTZge5d7"},
            {"client_secret","9OmGwADwEaQJEXSjP3_y6flYSHAKW4_sJd82V0WUfDMH8mg-oVPdbrgFniYxUbMu"},
            {"grant_type", "client_credentials"},
            {"scope", "api"},
        };
        var content = new FormUrlEncodedContent(formData);
        var token = await tokenClient.PostAsync(url, content);
        token.EnsureSuccessStatusCode();

        var response = JsonSerializer.Deserialize<TokenResponse>(await token.Content.ReadAsStringAsync());
        return response.AccessToken;
    }
    
    internal class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
        
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}