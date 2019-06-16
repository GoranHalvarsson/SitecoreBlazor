using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Foundation.BlazorExtensions.Services
{
    public class RestService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<T> ExecuteRestMethod<T>(string url) where T : class
        {
            return await _httpClient.GetJsonAsync<T>(url);
        }

        public async Task<string> ExecuteRestMethod(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }

        public async Task<T> ExecuteRestMethodWithJsonSerializerOptions<T>(string url, JsonSerializerOptions options = null)
        {
            string rawResultData = await ExecuteRestMethod(url);

            return JsonSerializer.Parse<T>(rawResultData, options == null ? _jsonSerializerOptions : options);
        }

    }
}
